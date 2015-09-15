using System.Collections.Generic;

namespace cs_api_dotnet
{
    /// <summary>
    /// CustomFields Object
    /// </summary>
    public class CustomFields
    {
        /// <summary>
        /// ID of custom fields
        /// </summary>
        public string _id { get; set; }
        /// <summary>
        /// Type of object these fields are for
        /// </summary>
        public string parent { get; set; }
        /// <summary>
        /// List of all custom fields
        /// </summary>
        public List<Field> fields { get; set; }
    }

    public class Field
    {
        /// <summary>
        /// ID of field
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Datatype of field
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Label of field
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Default value of field
        /// </summary>
        public string @default { get; set; }
        /// <summary>
        /// If this field was deleted 
        /// </summary>
        public bool deleted { get; set; }
    }


}
