namespace matcrm.data.Models.MollieModel {
    public class AddressObject {
        /// <summary>
        /// The card holder’s street and street number.
        /// </summary>
        public string StreetAndNumber { get; set; }

        /// <summary>
        /// The card holder’s postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// The card holder’s city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The card holder’s region.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The card holder’s country in ISO 3166-1 alpha-2 format.
        /// </summary>
        public string Country { get; set; }
    }
}