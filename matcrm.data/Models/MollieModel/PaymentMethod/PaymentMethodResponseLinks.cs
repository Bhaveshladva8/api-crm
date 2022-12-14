using matcrm.data.Models.MollieModel.Url;

namespace matcrm.data.Models.MollieModel.PaymentMethod {
    public class PaymentMethodResponseLinks {
        /// <summary>
        /// The API resource URL of the payment method itself.
        /// </summary>
        public UrlObjectLink<PaymentMethodResponse> Self { get; set; }

        /// <summary>
        /// The URL to the payment method retrieval endpoint documentation.
        /// </summary>
        public UrlLink Documentation { get; set; }
    }
}