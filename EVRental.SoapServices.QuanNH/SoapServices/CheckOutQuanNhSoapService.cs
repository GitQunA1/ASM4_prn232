using EVRental.Services.QuanNH;
using EVRental.SoapServices.QuanNH.SoapModels;
using System.ServiceModel;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public Task<int> CreateAsync(CheckOutQuanNh checkOutQuanNh)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CheckOutQuanNh>> GetAllAsync()
        {
            try
            {
                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

                var checkOutQuanNhs = await _serviceProviders.ICheckOutQuanNhService.GetAllAsync();

                var checkoutQuanNhJson = JsonSerializer.Serialize(checkOutQuanNhs, opt);

                var result = JsonSerializer.Deserialize<List<CheckOutQuanNh>>(checkoutQuanNhJson, opt);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<CheckOutQuanNh> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(CheckOutQuanNh checkOutQuanNh)
        {
            throw new NotImplementedException();
        }
    }
}
