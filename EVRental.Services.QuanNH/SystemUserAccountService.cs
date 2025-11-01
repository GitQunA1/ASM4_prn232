using EVRental.Repositories.QuanNH;
using EVRental.Repositories.QuanNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Services.QuanNH
{
    public class SystemUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SystemUserAccountService() => _unitOfWork ??= new UnitOfWork();

        public async Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            try
            {
                return await _unitOfWork.SystemUserAccountRepository.GetUserAccount(username, password);
            } catch (Exception ex) { }

            return null;
        }
    }
}
