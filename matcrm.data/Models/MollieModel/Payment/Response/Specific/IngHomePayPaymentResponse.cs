namespace matcrm.data.Models.MollieModel.Payment.Response.Specific {
    public class IngHomePayPaymentResponse : PaymentResponse {
        /// <summary>
        /// An object with payment details.
        /// </summary>
        public IngHomePayPaymentResponseDetails Details { get; set; }
    }

    public class IngHomePayPaymentResponseDetails {
        /// <summary>
        /// Only available one banking day after the payment has been completed – The consumer’s name.
        /// </summary>
        public string ConsumerName { get; set; }

        /// <summary>
        /// Only available one banking day after the payment has been completed – The consumer’s IBAN.
        /// </summary>
        public string ConsumerAccount { get; set; }

        /// <summary>
        /// Only available one banking day after the payment has been completed – BBRUBEBB.
        /// </summary>
        public string ConsumerBic { get; set; }
    }
}