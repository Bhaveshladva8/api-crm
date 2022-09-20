using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Shipment;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface IShipmentClient {
        Task<ShipmentResponse> CreateShipmentAsync(string orderId, ShipmentRequest shipmentRequest);
        Task<ShipmentResponse> GetShipmentAsync(string orderId, string shipmentId);
        Task<ListResponse<ShipmentResponse>> GetShipmentsListAsync(string orderId);
        Task<ShipmentResponse> UpdateOrderAsync(string orderId, string shipmentId, ShipmentUpdateRequest shipmentUpdateRequest);
    }
}