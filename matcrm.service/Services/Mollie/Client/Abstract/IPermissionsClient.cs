using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Permission;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface IPermissionsClient {
        Task<PermissionResponse> GetPermissionAsync(string permissionId);
        Task<PermissionResponse> GetPermissionAsync(UrlObjectLink<PermissionResponse> url);
        Task<ListResponse<PermissionResponse>> GetPermissionListAsync();
    }
}