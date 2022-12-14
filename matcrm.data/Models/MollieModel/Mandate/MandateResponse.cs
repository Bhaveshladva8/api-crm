using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace matcrm.data.Models.MollieModel.Mandate {
    public class MandateResponse : IResponseObject {
        /// <summary>
        /// Indicates the response contains a mandate object. Will always contain mandate for this endpoint.
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Unique identifier of you mandate.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Current status of mandate - See the matcrm.data.Models.MollieModel.Mandate.MandateStatus class for a full 
        /// list of known values.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Payment method of the mandate - See the matcrm.data.Models.MollieModel.Payment.PaymentMethod class for a full list of known values.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Mandate details that are different per payment method. Available fields depend on that payment method.
        /// </summary>
        public MandateDetails Details { get; set; }

        /// <summary>
        /// The mandate’s custom reference, if this was provided when creating the mandate.
        /// </summary>
        public string MandateReference { get; set; }

        /// <summary>
        /// The signature date of the mandate in YYYY-MM-DD format.
        /// </summary>
        public string SignatureDate { get; set; }

        /// <summary>
        /// DateTime when mandate was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// An object with several URL objects relevant to the mandate. Every URL object will contain an href and a type field.
        /// </summary>
        [JsonProperty("_links")]
        public MandateResponseLinks Links { get; set; }
    }

    public class MandateDetails {
        /// <summary>
        /// The direct debit account holder's name.
        /// </summary>
        public string ConsumerName { get; set; }

        /// <summary>
        /// The direct debit account IBAN.
        /// </summary>
        public string ConsumerAccount { get; set; }

        /// <summary>
        ///  The direct debit account BIC.
        /// </summary>
        public string ConsumerBic { get; set; }

        /// <summary>
        /// The credit card holder's name.
        /// </summary>
        public string CardHolder { get; set; }

        /// <summary>
        /// The last four digits of the credit card number.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// The credit card's label. Note that not all labels can be acquired through Mollie.
        /// </summary>
        public string CardLabel { get; set; }

        /// <summary>
        /// Unique alphanumeric representation of credit card, usable for identifying returning customers.
        /// </summary>
        public string CardFingerprint { get; set; }

        /// <summary>
        /// Expiry date of the credit card card in YYYY-MM-DD format.
        /// </summary>
        public string CardExpiryDate { get; set; }
    }
}