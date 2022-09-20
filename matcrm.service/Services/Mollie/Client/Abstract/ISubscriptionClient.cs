using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Payment.Response;
using matcrm.data.Models.MollieModel.Subscription;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface ISubscriptionClient {
        Task CancelSubscriptionAsync(string customerId, string subscriptionId);
        Task<SubscriptionResponse> CreateSubscriptionAsync(string customerId, SubscriptionRequest request);
        Task<SubscriptionResponse> GetSubscriptionAsync(string customerId, string subscriptionId);
        Task<ListResponse<SubscriptionResponse>> GetSubscriptionListAsync(string customerId, string from = null, int? limit = null);
        Task<ListResponse<SubscriptionResponse>> GetSubscriptionListAsync(UrlObjectLink<ListResponse<SubscriptionResponse>> url);
        Task<ListResponse<SubscriptionResponse>> GetAllSubscriptionList(string from = null, int? limit = null);
        Task<SubscriptionResponse> GetSubscriptionAsync(UrlObjectLink<SubscriptionResponse> url);
        Task<SubscriptionResponse> UpdateSubscriptionAsync(string customerId, string subscriptionId, SubscriptionUpdateRequest request);
        Task<ListResponse<PaymentResponse>> GetSubscriptionPaymentListAsync(string customerId, string subscriptionId, string from = null, int? limit = null);
    }
}