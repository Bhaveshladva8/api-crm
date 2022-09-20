using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Organization;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface IOrganizationsClient
    {
        Task<OrganizationResponse> GetCurrentOrganizationAsync();
        Task<OrganizationResponse> GetOrganizationAsync(string organizationId);
        Task<ListResponse<OrganizationResponse>> GetOrganizationsListAsync(string from = null, int? limit = null);
        Task<OrganizationResponse> GetOrganizationAsync(UrlObjectLink<OrganizationResponse> url);
    }
}