using EVRental.Repositories.QuanNH.Basic;
using EVRental.Repositories.QuanNH.DBContext;
using EVRental.Repositories.QuanNH.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Repositories.QuanNH
{
    public class ReturnConditionRepository : GenericRepository<ReturnCondition>
    {
        public ReturnConditionRepository() { }
        public ReturnConditionRepository(FA25_PRN232_SE1717_G6_EVRentalContext context) => _context = context;

        public async Task<List<ReturnCondition>> GetAllAsync()
        {
            return await _context.ReturnConditions.ToListAsync();
        }
    }
}
