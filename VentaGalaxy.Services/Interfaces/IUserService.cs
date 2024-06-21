using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Interfaces;

public interface IUserService
{
    Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);

    Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request);

    Task<BaseResponse> SendTokenToResetPasswordAsync(GenerateTokenToResetDtoRequest request);

    Task<BaseResponse> ResetPasswordAsync(ResetPasswordDtoRequest request);
}