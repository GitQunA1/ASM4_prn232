using EVRental.Repositories.QuanNH.Basic;
using EVRental.Repositories.QuanNH.DBContext;
using EVRental.Repositories.QuanNH.ModelExtensions;
using EVRental.Repositories.QuanNH.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Repositories.QuanNH
{
    public class CheckOutQuanNhRepository : GenericRepository<CheckOutQuanNh>
    {
        public CheckOutQuanNhRepository() { }

        public CheckOutQuanNhRepository(FA25_PRN232_SE1717_G6_EVRentalContext context) => _context = context;

        public async Task<List<CheckOutQuanNh>> GetAllAsync()
        {
            var items = await _context.CheckOutQuanNhs.Include(c => c.ReturnCondition).ToListAsync();

            return items ?? new List<CheckOutQuanNh>();
        }

        public async Task<CheckOutQuanNh> GetByIdAsync(int code)
        {
            var item = await _context.CheckOutQuanNhs
                .Include(c => c.ReturnCondition)
                .FirstOrDefaultAsync(c => c.CheckOutQuanNhid == code);
            return item ?? new CheckOutQuanNh();
        }

        public async Task<List<CheckOutQuanNh>> SearchAsync(string note, decimal cost, string name)
        {
            var query = _context.CheckOutQuanNhs
                .Include(c => c.ReturnCondition)
                .AsQueryable();

            // Tìm kiếm theo note (nếu có)
            if (!string.IsNullOrEmpty(note))
            {
                query = query.Where(c => c.Notes.Contains(note));
            }

            // Tìm kiếm theo cost (nếu có và khác 0)
            if (cost > 0)
            {
                query = query.Where(c => c.TotalCost == cost);
            }

            // Tìm kiếm theo name của ReturnCondition (nếu có)
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.ReturnCondition != null && c.ReturnCondition.Name.Contains(name));
            }

            var items = await query
                .OrderByDescending(c => c.CheckOutTime)
                .ToListAsync();

            return items ?? new List<CheckOutQuanNh>();
        }

        public async Task<PaginationResult<List<CheckOutQuanNh>>> SearchWithPagingAsync(CheckOutQuanNhSearchRequest searchRequest)
        {
            var items = await this.SearchAsync(searchRequest.note, searchRequest.cost.Value, searchRequest.name);

            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / searchRequest.PageSize.Value);

            items = items.Skip((searchRequest.CurrentPage.Value - 1) * searchRequest.PageSize.Value).Take(searchRequest.PageSize.Value).ToList();

            var result = new PaginationResult<List<CheckOutQuanNh>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = searchRequest.CurrentPage.Value,
                PageSize = searchRequest.PageSize.Value,
                Items = items
            };

            return result ?? new PaginationResult<List<CheckOutQuanNh>>();
        }
    }
}
