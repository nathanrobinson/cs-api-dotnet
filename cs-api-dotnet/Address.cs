using System.Collections.Generic;

namespace cs_api_dotnet
{
    /// <summary>
    /// Address Object
    /// </summary>
    public partial class Address
    {
        /// <summary>
        /// Address Line 1 (Street)
        /// </summary>
        public string line1 { get; set; }
        /// <summary>
        /// Address Line 2 (PO Box, APT, Suite)
        /// </summary>
        public string line2 { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// State/Province
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// Zip Code
        /// </summary>
        public string zip { get; set; }
        /// <summary>
        /// 2 Letter country code
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// Geo Location: [lon, lat]
        /// </summary>
        public List<double> loc { get; set; }
        /// <summary>
        /// Primary contact name
        /// </summary>
        public string contact { get; set; }
        /// <summary>
        /// Office phone
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// Office Fax
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// Primary contact email
        /// </summary>
        public string email { get; set; }
    }
}
