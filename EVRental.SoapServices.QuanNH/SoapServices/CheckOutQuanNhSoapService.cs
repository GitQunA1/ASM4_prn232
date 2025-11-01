using EVRental.Services.QuanNH;
using EVRental.SoapServices.QuanNH.SoapModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

using RepositoryCheckOutQuanNh = EVRental.Repositories.QuanNH.Models.CheckOutQuanNh;
using RepositoryReturnCondition = EVRental.Repositories.QuanNH.Models.ReturnCondition;

namespace EVRental.SoapServices.QuanNH.SoapServices
{
    [ServiceContract]
    public interface ICheckOutQuanNhSoapService
    {
        // Queries
        [OperationContract]
        Task<List<CheckOutQuanNh>> GetAllAsync();

        [OperationContract]
        Task<CheckOutQuanNh> GetByIdAsync(int id);

    [OperationContract]
    Task<List<ReturnCondition>> GetReturnConditionsAsync();

        // Mutation
        [OperationContract]
        Task<int> CreateAsync(CheckOutQuanNh checkOutQuanNh);

        [OperationContract]
        Task<int> UpdateAsync(CheckOutQuanNh checkOutQuanNh);

        [OperationContract]
        Task<int> DeleteAsync(int id);
    }
    public class CheckOutQuanNhSoapService : ICheckOutQuanNhSoapService
    {
        private readonly IServiceProviders _serviceProviders;
        public CheckOutQuanNhSoapService(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        public async Task<List<CheckOutQuanNh>> GetAllAsync()
        {
            try
            {
                var entities = await _serviceProviders.ICheckOutQuanNhService.GetAllAsync();
                return entities?.Select(MapToSoapModel).Where(item => item != null).ToList() ?? new List<CheckOutQuanNh>();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Unable to retrieve checkout records. {ex.Message}");
            }
        }

        public async Task<CheckOutQuanNh> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _serviceProviders.ICheckOutQuanNhService.GetByIdAsync(id);
                return entity == null ? null : MapToSoapModel(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException($"Unable to retrieve checkout record with id {id}. {ex.Message}");
            }
        }

        public async Task<List<ReturnCondition>> GetReturnConditionsAsync()
        {
            try
            {
                var items = await _serviceProviders.ReturnConditionService.GetAllAsync();
                return items?.Select(MapToSoapModel).Where(item => item != null).ToList() ?? new List<ReturnCondition>();
            }
            catch (Exception ex)
            {
                throw new FaultException($"Unable to retrieve return conditions. {ex.Message}");
            }
        }

        public async Task<int> CreateAsync(CheckOutQuanNh checkOutQuanNh)
        {
            if (checkOutQuanNh == null)
            {
                throw new FaultException("Payload is null.");
            }

            try
            {
                var entity = MapToRepositoryModel(checkOutQuanNh);
                return await _serviceProviders.ICheckOutQuanNhService.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException($"Unable to create checkout record. {ex.Message}");
            }
        }

        public async Task<int> UpdateAsync(CheckOutQuanNh checkOutQuanNh)
        {
            if (checkOutQuanNh == null)
            {
                throw new FaultException("Payload is null.");
            }

            try
            {
                var entity = MapToRepositoryModel(checkOutQuanNh);
                return await _serviceProviders.ICheckOutQuanNhService.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new FaultException($"Unable to update checkout record {checkOutQuanNh.CheckOutQuanNhid}. {ex.Message}");
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _serviceProviders.ICheckOutQuanNhService.DeleteAsync(id);
                return deleted ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw new FaultException($"Unable to delete checkout record {id}. {ex.Message}");
            }
        }

        private static CheckOutQuanNh MapToSoapModel(RepositoryCheckOutQuanNh entity)
        {
            if (entity == null || entity.CheckOutQuanNhid == 0)
            {
                return null;
            }

            return new CheckOutQuanNh
            {
                CheckOutQuanNhid = entity.CheckOutQuanNhid,
                CheckOutTime = entity.CheckOutTime,
                ReturnDate = entity.ReturnDate.HasValue ? entity.ReturnDate.Value.ToDateTime(TimeOnly.MinValue) : null,
                ExtraCost = entity.ExtraCost,
                TotalCost = entity.TotalCost,
                LateFee = entity.LateFee,
                IsPaid = entity.IsPaid,
                IsDamageReported = entity.IsDamageReported,
                Notes = entity.Notes,
                CustomerFeedback = entity.CustomerFeedback,
                PaymentMethod = entity.PaymentMethod,
                StaffSignature = entity.StaffSignature,
                CustomerSignature = entity.CustomerSignature,
                ReturnConditionId = entity.ReturnConditionId,
                ReturnCondition = MapToSoapModel(entity.ReturnCondition)
            };
        }

        private static ReturnCondition MapToSoapModel(RepositoryReturnCondition entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ReturnCondition
            {
                ReturnConditionId = entity.ReturnConditionId,
                Name = entity.Name,
                RepairCost = entity.RepairCost,
                SeverityLevel = entity.SeverityLevel,
                IsResolved = entity.IsResolved
            };
        }

        private static RepositoryCheckOutQuanNh MapToRepositoryModel(CheckOutQuanNh model)
        {
            return new RepositoryCheckOutQuanNh
            {
                CheckOutQuanNhid = model.CheckOutQuanNhid,
                CheckOutTime = model.CheckOutTime,
                ReturnDate = model.ReturnDate.HasValue ? DateOnly.FromDateTime(model.ReturnDate.Value) : null,
                ExtraCost = model.ExtraCost,
                TotalCost = model.TotalCost,
                LateFee = model.LateFee,
                IsPaid = model.IsPaid,
                IsDamageReported = model.IsDamageReported,
                Notes = model.Notes,
                CustomerFeedback = model.CustomerFeedback,
                PaymentMethod = model.PaymentMethod,
                StaffSignature = model.StaffSignature,
                CustomerSignature = model.CustomerSignature,
                ReturnConditionId = model.ReturnConditionId
            };
        }
    }
}
