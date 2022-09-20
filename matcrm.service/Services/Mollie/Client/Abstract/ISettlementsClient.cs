using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.Capture;
using matcrm.data.Models.MollieModel.Chargeback;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Payment.Response;
using matcrm.data.Models.MollieModel.Refund;
using matcrm.data.Models.MollieModel.Settlement;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface ISettlementsClient {
        Task<SettlementResponse> GetSettlementAsync(string settlementId);
        Task<SettlementResponse> GetNextSettlement();
        Task<SettlementResponse> GetOpenSettlement();
        Task<ListResponse<SettlementResponse>> GetSettlementsListAsync(string reference = null, string from = null, int? limit = null);
        Task<ListResponse<PaymentResponse>> GetSettlementPaymentsListAsync(string settlementId, string from = null, int? limit = null);
        Task<ListResponse<RefundResponse>> GetSettlementRefundsListAsync(string settlementId, string from = null, int? limit = null);
        Task<ListResponse<ChargebackResponse>> GetSettlementChargebacksListAsync(string settlementId, string from = null, int? limit = null);
        Task<SettlementResponse> GetSettlementAsync(UrlObjectLink<SettlementResponse> url);
        Task<ListResponse<CaptureResponse>> ListSettlementCapturesAsync(string settlementId);
    }
}