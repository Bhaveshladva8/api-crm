using System.Threading.Tasks;
using matcrm.data.Models.Dto.Mollie;
using matcrm.data.Models.MollieModel.Subscription;

namespace matcrm.service.Services.Mollie.Subscription {
    public interface ISubscriptionStorageClient {
        Task<SubscriptionResponse> Create(CreateSubscriptionModel model);
        Task Cancel(string customerId, string subscriptionId);
    }
}