using System.Collections.Generic;
using matcrm.data.Models.MollieModel.Refund;

namespace matcrm.data.Models.MollieModel.Order {
    public class OrderRefundResponse : RefundResponse {
        /// <summary>
        /// The unique identifier of the order this refund was created for. For example: ord_stTC2WHAuS.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// An array of order line objects as described in Get order. Only available if the refund was created via the
        /// Create Order Refund API.
        /// </summary>
        public IEnumerable<OrderLineResponse> Lines { get; set; }
    }
}
