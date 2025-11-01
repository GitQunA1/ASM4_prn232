using EVRental.Repositories.QuanNH.ModelExtensions;
using EVRental.Repositories.QuanNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Services.QuanNH
{
    public interface ICheckOutQuanNhService
    {
        Task<List<CheckOutQuanNh>> GetAllAsync();
        Task<CheckOutQuanNh> GetByIdAsync(int id);
        Task<List<CheckOutQuanNh>> SearchAsync(string note, decimal cost, string name);
        Task<PaginationResult<List<CheckOutQuanNh>>> SearchWithPaginationAsync(CheckOutQuanNhSearchRequest searchRequest);
        Task<int> CreateAsync(CheckOutQuanNh entity);
        Task<int> UpdateAsync(CheckOutQuanNh entity);
        Task<bool> DeleteAsync(int id);
    }
}
