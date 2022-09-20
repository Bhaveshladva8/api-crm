using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Refund;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface IRefundClient {
        Task CancelRefundAsync(string paymentId, string refundId, bool testmode = false);
        Task<RefundResponse> CreateRefundAsync(string paymentId, RefundRequest refundRequest);
        Task<RefundResponse> GetRefundAsync(string paymentId, string refundId, bool testmode = false);
        Task<ListResponse<RefundResponse>> GetRefundListAsync(string paymentId, string from = null, int? limit = null, bool testmode = false);
        Task<RefundResponse> GetRefundAsync(UrlObjectLink<RefundResponse> url);
    }
}