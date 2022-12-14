namespace matcrm.data.Models.MollieModel.Payment.Request {
    public class SepaDirectDebitRequest : PaymentRequest {
        public SepaDirectDebitRequest() {
            this.Method = PaymentMethod.DirectDebit;
        }

        /// <summary>
        /// Optional - Beneficiary name of the account holder.
        /// </summary>
        public string ConsumerName { get; set; }

        /// <summary>
        /// Optional - IBAN of the account holder.
        /// </summary>
        public string ConsumerAccount { get; set; }
    }
}