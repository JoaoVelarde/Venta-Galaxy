using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using VentaGalaxy.DataAccess;
using VentaGalaxy.Entities;
using VentaGalaxy.Repositories.Interfaces;
using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Shared;
using VentaGalaxy.Shared.Configuracion;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Implementaciones;

public class UserService : IUserService
{
    private readonly AppSettings _configuration;
    private readonly UserManager<ClienteIdentityUser> _userManager;
    private readonly ILogger<UserService> _logger;
    private readonly IClienteRepository _empleadoRepository;
    private readonly IEmailService _emailService;

    public UserService(UserManager<ClienteIdentityUser> userManager,
        ILogger<UserService> logger, IClienteRepository empleadoRepository, IEmailService emailService, IOptions<AppSettings> options)
    {
        _configuration = options.Value;
        _userManager = userManager;
        _logger = logger;
        _empleadoRepository = empleadoRepository;
        _emailService = emailService;

    }
    public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
    {
        var response = new LoginDtoResponse();

        try
        {
            var identity = await _userManager.FindByNameAsync(request.Usuario);

            if (identity is null)
                throw new SecurityException("Usuario no existe");

            if (await _userManager.IsLockedOutAsync(identity))
            {
                throw new SecurityException($"Demasiados intentos fallidos para el usuario {request.Usuario}");
            }

            if (!await _userManager.CheckPasswordAsync(identity, request.Password))
            {
                response.ErrorMessage = "Usuario o clave incorrecta";
                _logger.LogWarning($"Intento fallido de Login para el usuario {identity.UserName}");

                await _userManager.AccessFailedAsync(identity); // Esto aumenta el contador de bloqueo

                return response;
            }

            var roles = await _userManager.GetRolesAsync(identity);
            var fechaExpiracion = DateTime.Now.AddHours(1);

            // Vamos a devolver los Claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, identity.NombreCompleto),
                new Claim(ClaimTypes.Email, identity.Email!),
                new Claim(ClaimTypes.Expiration, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss")),
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            response.Roles = roles.ToList();

            var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Jwt.SecretKey));
            var credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credenciales);

            var payload = new JwtPayload(
                _configuration.Jwt.Issuer,
                _configuration.Jwt.Audience,
                claims,
                DateTime.Now,
                fechaExpiracion
            );


            var jwtToken = new JwtSecurityToken(header, payload);

            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            response.NombreCompleto = identity.NombreCompleto;
            response.Success = true;

            _logger.LogInformation("Se creó el JWT de forma satisfactoria");
        }
        catch (SecurityException ex)
        {
            response.ErrorMessage = ex.Message;
            _logger.LogError(ex, "Error de seguridad {Message}", ex.Message);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error inesperado";
            _logger.LogError(ex, "Error al autenticar {Message}", ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request)
    {
        var response = new BaseResponse();

        try

        {
            var identity = new ClienteIdentityUser()
            {
                NombreCompleto = request.NombresCompleto,
                UserName = request.Usuario,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identity, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(identity, Constantes.RolCliente);

                var cliente = new Cliente()
                {
                    NombreCompleto = request.NombresCompleto,
                    Correo = request.Email,
                    NroDocumento = request.NroDocumento,
                    FechaCrea = DateTime.Today,
                    Telefono = request.Telefono
                };

                await _empleadoRepository.AddAsync(cliente);

                // Enviar un email
                await _emailService.SendEmailAsync(request.Email, "Venta Galaxy - Registro",
                    $@"<strong><p>Felicidades {request.NombresCompleto}</p></strong>
                     <p>Su cuenta ha sido creada satisfactoriamente</p>");
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var identityError in result.Errors)
                {
                    sb.AppendFormat("{0} ", identityError.Description);
                }

                response.ErrorMessage = sb.ToString();
                sb.Clear();
            }

            response.Success = result.Succeeded;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al registrar";
            _logger.LogWarning(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> SendTokenToResetPasswordAsync(GenerateTokenToResetDtoRequest request)
    {
        var response = new BaseResponseGeneric<string>();
        try
        {
            ClienteIdentityUser? user = null;

            if (!string.IsNullOrEmpty(request.Usuario))
            {
                user = await _userManager.FindByNameAsync(request.Usuario);
                if (user is null) throw new SecurityException("Usuario no existe");
            }
            else if (!string.IsNullOrEmpty(request.Email))
            {
                user = await _userManager.FindByEmailAsync(request.Email);
                if (user is null) throw new SecurityException("Usuario no existe");
            }

            if (user is null)
            {
                response.ErrorMessage = "Usuario no pudo ser validado";
                return response;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // codificamos el token
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            await _emailService.SendEmailAsync(user.Email!, "Venta Galaxy - Solicitud de Reseteo de Clave",
                @$"Por favor use el siguiente token para resetear su contraseña, haga click aqui:
                    <p><a href=""{_configuration.UrlFrontend}/reset-password?email={request.Email}&token={token}"">Recuperar clave</a></p>");

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al generar el token para restablecer la contraseña";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> ResetPasswordAsync(ResetPasswordDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new SecurityException("Usuario no existe");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Clave);
            if (result.Succeeded)
            {
                response.Success = true;
                _logger.LogInformation("Contraseña restablecida con éxito");

                // TODO: Enviar un correo con el mensaje.
                await _emailService.SendEmailAsync(request.Email, "Reset Password Confirmation",
                    "Tu contrseña fue cambiado correctamente.");
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var identityError in result.Errors)
                {
                    sb.AppendFormat("{0} ", identityError.Description);
                }

                response.ErrorMessage = sb.ToString();
                sb.Clear();
                _logger.LogError("Error al restablecer la contraseña");
            }

            response.Success = result.Succeeded;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al restablecer la contraseña";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
}