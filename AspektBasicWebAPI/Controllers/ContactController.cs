using AspektBasicWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AspektBasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly StructureContext structureContext;

        public ContactController(StructureContext structureContext)
        {
            this.structureContext = structureContext;
        }

        /// <summary>
        /// Retrieves a List of all Contacts.
        /// </summary>
        /// <returns>Returns a List of all Contacts in the Database.</returns>
        //Get()
        [HttpGet]
        [Route("GetAllContacts")]
        [SwaggerOperation("GetAllContacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public List<Contact> GetContacts()
        {
            return structureContext.Contacts.ToList();
        }

        /// <summary>
        /// Retrieves a specific Contact by ID.
        /// </summary>
        /// <param name="id">The ID of the Contact to retrieve.</param>
        /// <returns>Return the Contact with the ID.</returns>
        //Get()
        [HttpGet]
        [Route("GetContactById")]
        [SwaggerOperation("GetContactById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public Contact GetContact(int id)
        {
            return structureContext.Contacts.Where(x => x.ContactId == id).FirstOrDefault();
        }

        /*
        //Create(Contact) = Add()
        [HttpPost]
        [Route("AddContact")]
        public string AddContact(Contact contact)
        {
            string response = string.Empty;
            structureContext.Contacts.Add(contact);
            structureContext.SaveChanges();
            return "Contact Added";
        }
        */
        //Create(Contact) = Add()

        /// <summary>
        /// Add a Contact to the database.
        /// </summary>
        /// <param name="contact">The Contact to add.</param>
        /// <returns>Returns a message indicating the success of the add operation</returns>
        [HttpPost]
        [Route("AddContact")]
        [SwaggerOperation("AddContact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public string AddContact(Contact contact)
        {
            string response = string.Empty;

            bool contactExists = structureContext.Contacts.Any(c =>
                c.ContactName == contact.ContactName
                //c.ContactId == contact.ContactId
                );

            if (!contactExists)
            {
                structureContext.Contacts.Add(contact);
                structureContext.SaveChanges();
                response = "Contact Added";
            }
            else
            {
                response = "Contact Already Exists";
            }
            return response;
        }

        /// <summary>
        /// Update an existing Contact to the database.
        /// </summary>
        /// <param name="contact">The updated Contact information.</param>
        /// <returns>Returns a message indicating the success of the update operation.</returns>
        //Update(Contact)
        [HttpPut]
        [Route("UpdateContact")]
        [SwaggerOperation("UpdateContact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public string UpdateContact(Contact contact)
        {
            structureContext.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            structureContext.SaveChanges();

            return "User Updated";
        }

        /// <summary>
        /// Deletes a Contact from the database.
        /// </summary>
        /// <param name="id">The ID from Contact to delete.</param>
        /// <returns>Returns a response indicating the success of the delete operation.</returns>
        //Delete()
        [HttpDelete]
        [Route("DeleteContact")]
        [SwaggerOperation("DeleteContact")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public string DeleteContact(int id)
        {
            Contact contact = structureContext.Contacts.Where(x => x.ContactId == id).FirstOrDefault();
            if (contact != null)
            {
                structureContext.Contacts.Remove(contact);
                structureContext.SaveChanges();
                return "User Deleted";
            }
            else
            {
                return "No User Found";
            }
        }

        /// <summary>
        /// Get Contacts with Company and Country information.
        /// </summary>
        /// <returns>Returns a List of Contacts with Company and Country information</returns>
        //GetContactsWithCompanyAndCountry()
        [HttpGet]
        [Route("GetContactsWithCompanyAndCountry")]
        [SwaggerOperation("GetContactsWithCompanyAndCountry information")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult GetContactsWithCompanyAndCountry()
        {
            var results = structureContext.Contacts
                .Include(c => c.Company)
                .Select(c => new
                {
                    Contact = c.ContactName,
                    Company = c.Company.CompanyName,
                    Country = c.Country.CountryName
                })
                .ToList();
            return Ok(results);
        }

        /// <summary>
        /// Filter Contact by CountryId and CompanyId.
        /// </summary>
        /// <param name="countryId">The identifier of the Country to filter Contacts by.</param>
        /// <param name="companyId">The identifier of the Company to filter Contacts by.</param>
        /// <returns>Returns a list of contacts that match the specified filters.</returns>
        //FilterContacts(int countryId, int companyId) List<Contact>
        [HttpGet]
        [Route("FilterContact")]
        [SwaggerOperation("Filter Contact based on Country and/or Company filters.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public ActionResult<List<Contact>> FilterContact(int? countryId = null, int? companyId = null)
        {
            
            
            if (!countryId.HasValue && !companyId.HasValue) {
                return BadRequest("At least one filter parameter required!");
            }
            
            if (countryId.HasValue && countryId <= 0)
            {
                return BadRequest("Invalid countryId. It should be positive integer.");
            }
            if (companyId.HasValue && companyId <= 0)
            {
                return BadRequest("Invalid companyId. It should be positive integer.");
            }

            
            var filteredContacts = structureContext.Contacts
                .Where(c => 
                    (!countryId.HasValue || c.CountryId == countryId) &&
                    (!companyId.HasValue || c.CompanyId == companyId)
                 )
                .ToList();
            
            return filteredContacts;

        }
        
        


        /*
        //FilterContacts(int countryId, int companyId) List<Contact>
        [HttpGet]
        [Route("FilterContact")]
        public ActionResult<List<Contact>> FilterContact(int countryId, int companyId)
        {

            var filteredContacts = structureContext.Contacts
                .Where(c =>
                    (countryId == 0 || c.CountryId == countryId) && 
                    (companyId == 0 || c.CompanyId == companyId)
                 )
                .ToList();

            return filteredContacts;
        }
        */
        

    }
}
