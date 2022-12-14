using matcrm.data.Models.MollieModel.Url;

namespace matcrm.data.Models.MollieModel.Profile.Response {
    public class EnableGiftCardIssuerResponseLinks {
        /// <summary>
        /// The API resource URL of the gift card issuer itself.
        /// </summary>
        public UrlLink Self { get; set; }

        /// <summary>
        /// The URL to the gift card issuer retrieval endpoint documentation.
        /// </summary>
        public UrlLink Documentation { get; set; }
    }
}
