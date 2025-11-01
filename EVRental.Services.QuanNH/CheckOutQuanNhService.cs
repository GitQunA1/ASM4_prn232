using EVRental.Repositories.QuanNH;
using EVRental.Repositories.QuanNH.ModelExtensions;
using EVRental.Repositories.QuanNH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Services.QuanNH
{
    public class CheckOutQuanNhService : ICheckOutQuanNhService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CheckOutQuanNhService() => _unitOfWork ??= new UnitOfWork();

        public async Task<int> CreateAsync(CheckOutQuanNh entity)
        {
            try
            {
                return await _unitOfWork.CheckOutQuanNhRepository.CreateAsync(entity);
            }
            catch(Exception e) { }

            return 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var item = await _unitOfWork.CheckOutQuanNhRepository.GetByIdAsync(id);

                if(item != null)
                {
                    return await _unitOfWork.CheckOutQuanNhRepository.RemoveAsync(item);
                }
            }
            catch (Exception e) { }

            return false;
        }

        public async Task<List<CheckOutQuanNh>> GetAllAsync()
        {
            try
            {
                return await _unitOfWork.CheckOutQuanNhRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            //return new List<CheckOutQuanNh>();
        }

        public async Task<CheckOutQuanNh> GetByIdAsync(int id)
        {
            try
            {
                return await _unitOfWork.CheckOutQuanNhRepository.GetByIdAsync(id);
            }
            catch (Exception ex) { }

            return new CheckOutQuanNh();
        }

        public async Task<List<CheckOutQuanNh>> SearchAsync(string note, decimal cost, string name)
        {
            try
            {
                return await _unitOfWork.CheckOutQuanNhRepository.SearchAsync(note, cost, name);
            }
            catch (Exception ex) { }

            return new List<CheckOutQuanNh>();
        }

        public async Task<PaginationResult<List<CheckOutQuanNh>>> SearchWithPaginationAsync(CheckOutQuanNhSearchRequest searchRequest)
        {
            try
            {
                return await _unitOfWork.CheckOutQuanNhRepository.SearchWithPagingAsync(searchRequest);
            }
            catch (Exception ex) { }

            return new PaginationResult<List<CheckOutQuanNh>>();
        }

        public async Task<int> UpdateAsync(CheckOutQuanNh entity)
        {
            try
            {
                return await _unitOfWork.CheckOutQuanNhRepository.UpdateAsync(entity);
            }
            catch (Exception ex) { }

            return 0;
        }
    }
}
