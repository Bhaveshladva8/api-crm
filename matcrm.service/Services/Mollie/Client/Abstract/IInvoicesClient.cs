using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.Customer;
using matcrm.data.Models.MollieModel.Invoice;
using matcrm.data.Models.MollieModel.List;

using matcrm.data.Models.MollieModel.Url;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface IInvoicesClient {
        Task<InvoiceResponse> GetInvoiceAsync(string invoiceId, bool includeLines = false, bool includeSettlements = false);
        Task<InvoiceResponse> GetInvoiceAsync(UrlObjectLink<InvoiceResponse> url);
        Task<ListResponse<InvoiceResponse>> GetInvoiceListAsync(string reference = null, int? year = null, string from = null, int? limit = null, bool includeLines = false, bool includeSettlements = false);
    }
}