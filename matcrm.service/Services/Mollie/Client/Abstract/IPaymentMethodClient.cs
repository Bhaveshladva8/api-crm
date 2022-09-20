using System.Threading.Tasks;
using matcrm.data.Models.MollieModel;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Payment;
using matcrm.data.Models.MollieModel.PaymentMethod;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface IPaymentMethodClient {
		Task<PaymentMethodResponse> GetPaymentMethodAsync(string paymentMethod, bool includeIssuers = false, string locale = null, bool includePricing = false, string profileId = null, bool testmode = false, string currency = null);
        Task<ListResponse<PaymentMethodResponse>> GetAllPaymentMethodListAsync(string locale = null, Amount amount = null, bool includeIssuers = false, bool includePricing = false);
        Task<ListResponse<PaymentMethodResponse>> GetPaymentMethodListAsync(string sequenceType = null, string locale = null, Amount amount = null, bool includeIssuers = false, bool includePricing = false, string profileId = null, bool testmode = false, Resource? resource = null, string billingCountry = null);
        Task<PaymentMethodResponse> GetPaymentMethodAsync(UrlObjectLink<PaymentMethodResponse> url);
    }
}