using System.Threading.Tasks;
using matcrm.data.Models.Dto.Mollie;
using matcrm.data.Models.MollieModel.Payment.Response;

namespace matcrm.service.Services.Mollie.Payment {
    public interface IPaymentOverviewClient {
        Task<OverviewModel<PaymentResponse>> GetList();
        Task<OverviewModel<PaymentResponse>> GetListByUrl(string url);
    }
}