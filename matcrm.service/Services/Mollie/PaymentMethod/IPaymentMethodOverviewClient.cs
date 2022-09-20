using System.Threading.Tasks;
using matcrm.data.Models.Dto.Mollie;
using matcrm.data.Models.MollieModel.PaymentMethod;

namespace matcrm.service.Services.Mollie.PaymentMethod {
    public interface IPaymentMethodOverviewClient {
        Task<OverviewModel<PaymentMethodResponse>> GetList();
    }
}