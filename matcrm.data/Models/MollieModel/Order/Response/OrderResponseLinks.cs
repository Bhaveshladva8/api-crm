using matcrm.data.Models.MollieModel.Subscription;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.data.Models.MollieModel.Order {
    public class OrderResponseLinks {
        /// <summary>
        /// The API resource URL of the order itself.
        /// </summary>
        public UrlObjectLink<OrderResponse> Self { get; set; }

        /// <summary>
        /// The URL your customer should visit to make the payment for the order.
        /// This is where you should redirect the customer to after creating the order.
        /// </summary>
        public UrlLink Checkout { get; set; }

        /// <summary>
        /// The URL to the order retrieval endpoint documentation.
        /// </summary>
        public UrlLink Documentation { get; set; }
    }
}