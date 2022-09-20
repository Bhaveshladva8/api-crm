using System.Threading.Tasks;
using matcrm.data.Models.Dto.Mollie;
using matcrm.data.Models.MollieModel.Customer;

namespace matcrm.service.Services.Mollie.Customer {
    public interface ICustomerOverviewClient {
        Task<OverviewModel<CustomerResponse>> GetList();
        Task<OverviewModel<CustomerResponse>> GetListByUrl(string url);
    }
}