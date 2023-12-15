using System.ComponentModel.DataAnnotations;

namespace AspektBasicWebAPI.Models
{
    /// <summary>
    /// The Country Model
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Foreign key to the Country.
        /// </summary>
        [Key]
        public int CountryId { get; set; }
        /// <summary>
        /// Name of the Country.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Navigation property for one-to-many relationship.
        /// </summary>
        //Navigation property for one-to-many relationship
        public List<Contact> Contacts { get; set; }
    }
}
