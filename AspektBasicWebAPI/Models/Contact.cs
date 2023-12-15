using System.ComponentModel.DataAnnotations;

namespace AspektBasicWebAPI.Models
{
    /// <summary>
    /// The Contact Model.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Unique key to the Contact.
        /// </summary>
        [Key]
        public int ContactId { get; set; }
        /// <summary>
        /// Name of the Contact.
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Foreign key to the Contact.
        /// </summary>
        //Foreign Key Property
        public int CompanyId { get; set; }
        /// <summary>
        /// Foreign key to the Contact.
        /// </summary>
        public int CountryId { get; set; }


        /// <summary>
        /// One-To-Many relationships with Company.
        /// </summary>
        public Company Company { get; set; }
        /// <summary>
        /// One-To-Many relationships with Country.
        /// </summary>
        public Country Country { get; set; }


    }
}
