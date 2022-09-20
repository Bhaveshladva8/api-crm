using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.Chargeback;
using matcrm.data.Models.MollieModel.List;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface IChargebacksClient {
        Task<ChargebackResponse> GetChargebackAsync(string paymentId, string chargebackId);
        Task<ListResponse<ChargebackResponse>> GetChargebacksListAsync(string paymentId, string from = null, int? limit = null);
        Task<ListResponse<ChargebackResponse>> GetChargebacksListAsync(string profileId = null, bool? testmode = null);
    }
}