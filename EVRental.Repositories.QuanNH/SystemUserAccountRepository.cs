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
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository() { }

        public SystemUserAccountRepository(FA25_PRN232_SE1717_G6_EVRentalContext context) => _context = context;

        public async Task<SystemUserAccount> GetUserAccount(string userName, string password)
        {
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password && u.IsActive == true);

            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Phone == userName && u.Password == password && u.IsActive == true);

            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == userName && u.Password == password && u.IsActive == true);

            return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive == true);
        }
    }
}
