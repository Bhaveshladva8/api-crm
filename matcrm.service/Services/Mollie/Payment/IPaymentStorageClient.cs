using System.Threading.Tasks;
using matcrm.data.Models.Dto.Mollie;
using matcrm.data.Models.MollieModel.Payment.Response;

namespace matcrm.service.Services.Mollie.Payment
{
    public interface IPaymentStorageClient
    {
        Task<PaymentResponse> Create(CreatePaymentModel model);

        Task<PaymentResponse> GetPayment(string paymentId);
    }
}