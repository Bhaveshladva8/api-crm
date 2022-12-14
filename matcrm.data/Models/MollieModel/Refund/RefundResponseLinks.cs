using matcrm.data.Models.MollieModel.Payment.Response;
using matcrm.data.Models.MollieModel.Settlement;
using matcrm.data.Models.MollieModel.Url;

namespace matcrm.data.Models.MollieModel.Refund {
    public class RefundResponseLinks {
        /// <summary>
        /// The API resource URL of the refund itself.
        /// </summary>
        public UrlObjectLink<RefundResponse> Self { get; set; }

        /// <summary>
        /// The API resource URL of the payment the refund belongs to.
        /// </summary>
        public UrlObjectLink<PaymentResponse> Payment { get; set; }

        /// <summary>
        /// The API resource URL of the settlement this payment has been settled with. Not present if not yet settled.
        /// </summary>
        public UrlObjectLink<SettlementResponse> Settlement { get; set; }

        /// <summary>
        /// The URL to the refund retrieval endpoint documentation.
        /// </summary>
        public UrlLink Documentation { get; set; }
    }
}