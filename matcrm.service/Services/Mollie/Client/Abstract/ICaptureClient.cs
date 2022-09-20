using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Capture;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface ICaptureClient {
        Task<CaptureResponse> GetCaptureAsync(string paymentId, string captureId);
        Task<ListResponse<CaptureResponse>> GetCapturesListAsync(string paymentId);
    }
}