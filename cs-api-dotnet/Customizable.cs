using System.Collections.Generic;

namespace cs_api_dotnet
{
    /// <summary>
    /// Classes derived from this class have customizable attributes in the api
    /// </summary>
    public class Customizable
    {
        /// <summary>
        /// Dictionary of custom fields created for object.
        /// </summary>
        public Dictionary<string, string> custom_fields { get; set; }
    }
}