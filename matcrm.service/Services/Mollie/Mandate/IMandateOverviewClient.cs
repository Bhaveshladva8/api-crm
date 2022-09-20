using System.Threading.Tasks;
using matcrm.data.Models.Dto.Mollie;
using matcrm.data.Models.MollieModel.Mandate;

namespace matcrm.service.Services.Mollie.Mandate {
    public interface IMandateOverviewClient {
        Task<OverviewModel<MandateResponse>> GetList(string customerId);
        Task<OverviewModel<MandateResponse>> GetListByUrl(string url);
    }
}