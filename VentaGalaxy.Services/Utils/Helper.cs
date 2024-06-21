using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Services.Utils
{
    public static class Helper
    {
        public static int GetTotalPages(int totalRows, int rowsPerPage)
        {
            if (totalRows == 0) return 0;
            int totalPages = totalRows / rowsPerPage;
            if (totalRows % rowsPerPage > 0)
            {
                totalPages++;
            }

            return totalPages;
        }
    }
}
