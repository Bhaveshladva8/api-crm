using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.Customer;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Payment.Request;
using matcrm.data.Models.MollieModel.Payment.Response;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface ICustomerClient {
        Task<CustomerResponse> CreateCustomerAsync(CustomerRequest request);
        Task<CustomerResponse> UpdateCustomerAsync(string customerId, CustomerRequest request);
        Task DeleteCustomerAsync(string customerId);
        Task<CustomerResponse> GetCustomerAsync(string customerId);
        Task<CustomerResponse> GetCustomerAsync(UrlObjectLink<CustomerResponse> url);
        Task<ListResponse<CustomerResponse>> GetCustomerListAsync(UrlObjectLink<ListResponse<CustomerResponse>> url);
        Task<ListResponse<CustomerResponse>> GetCustomerListAsync(string from = null, int? limit = null);
        Task<ListResponse<PaymentResponse>> GetCustomerPaymentListAsync(string customerId, string from = null, int? limit = null);
        Task<PaymentResponse> CreateCustomerPayment(string customerId, PaymentRequest paymentRequest);
    }
}