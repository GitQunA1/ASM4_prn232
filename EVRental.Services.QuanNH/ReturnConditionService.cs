using EVRental.Repositories.QuanNH;
using EVRental.Repositories.QuanNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Services.QuanNH
{
    public class ReturnConditionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReturnConditionService() => _unitOfWork ??= new UnitOfWork();
        public async Task<List<ReturnCondition>> GetAllAsync()
        {
            try
            {
                return await _unitOfWork.ReturnConditionRepository.GetAllAsync();
            }
            catch (Exception ex) { }

            return new List<ReturnCondition>();
        }
    }
}
