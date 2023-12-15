using System.ComponentModel.DataAnnotations;

namespace AspektBasicWebAPI.Models
{
    /// <summary>
    /// The Company Model.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Foreign key to the Company.
        /// </summary>
        [Key]
        public int CompanyId { get; set; }

        /// <summary>
        /// Name of the Company.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Navigation property for one-to-many relationships.
        /// </summary>
        //Navigation property for one-to-many relationship
        public List<Contact> Contacts { get; set; }
    }
}
