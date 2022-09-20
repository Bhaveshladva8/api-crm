using System.Threading.Tasks;
using matcrm.data.Models.Dto.Mollie;
using matcrm.data.Models.MollieModel.Subscription;

namespace matcrm.service.Services.Mollie.Subscription {
    public interface ISubscriptionOverviewClient {
        Task<OverviewModel<SubscriptionResponse>> GetList(string customerId);
        Task<OverviewModel<SubscriptionResponse>> GetListByUrl(string url);
    }
}