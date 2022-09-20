using System.Threading.Tasks;
using matcrm.data.Models.MollieModel.List;
using matcrm.data.Models.MollieModel.Order;
using matcrm.data.Models.MollieModel.Payment.Response;
using matcrm.data.Models.MollieModel.Refund;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.service.Services.Mollie.Client.Abstract {
    public interface IOrderClient {
        Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest);
        Task<OrderResponse> GetOrderAsync(string orderId, bool embedPayments = false, bool embedRefunds = false, bool embedShipments = false);
        Task<OrderResponse> UpdateOrderAsync(string orderId, OrderUpdateRequest orderUpdateRequest);
        Task<OrderResponse> UpdateOrderLinesAsync(string orderId, string orderLineId, OrderLineUpdateRequest orderLineUpdateRequest);
        Task CancelOrderAsync(string orderId);
        Task<ListResponse<OrderResponse>> GetOrderListAsync(string from = null, int? limit = null);
        Task<ListResponse<OrderResponse>> GetOrderListAsync(UrlObjectLink<ListResponse<OrderResponse>> url);
        Task CancelOrderLinesAsync(string orderId, OrderLineCancellationRequest cancelationRequest);
        Task<PaymentResponse> CreateOrderPaymentAsync(string orderId, OrderPaymentRequest createOrderPaymentRequest);
        Task<OrderRefundResponse> CreateOrderRefundAsync(string orderId, OrderRefundRequest createOrderRefundRequest);
        Task<ListResponse<RefundResponse>> GetOrderRefundListAsync(string orderId, string from = null, int? limit = null);
    }
}