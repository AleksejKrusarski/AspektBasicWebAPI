using AspektBasicWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AspektBasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly StructureContext structureContext;

        public CountryController(StructureContext structureContext)
        {
            this.structureContext = structureContext;
        }

        /// <summary>
        /// Retrieves a List of all Countries.
        /// </summary>
        /// <returns>Returns a List of all Countries in the database.</returns>
        //Get()
        [HttpGet]
        [Route("GetAllCountries")]
        [SwaggerOperation("GetAllCountries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public List<Country> GetCountry()
        {
            return structureContext.Countries.ToList();
        }
        
        /// <summary>
        /// Retrieves a specific Country by ID.
        /// </summary>
        /// <param name="id">The ID of the Country to retrieve.</param>
        /// <returns>Returns the country with the specified ID.</returns>
        //Get()
        [HttpGet]
        [Route("GetCountryById")]
        [SwaggerOperation("GetCountryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public Country GetCountry(int id)
        {
            return structureContext.Countries.Where(x => x.CountryId == id).FirstOrDefault();
        }

        /// <summary>
        /// Add Country to the database.
        /// </summary>
        /// <param name="country">The Country to add.</param>
        /// <returns>Returns a message indicating the success of the add operation.</returns>
        //Create(Country)
        [HttpPost]
        [Route("AddCountry")]
        [SwaggerOperation("AddCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public string AddCountry (Country country)
        {
            string response = string.Empty;
            structureContext.Countries.Add(country);
            structureContext.SaveChanges();
            return "Country Added";
        }

        /// <summary>
        /// Update an existing Country to the database.
        /// </summary>
        /// <param name="country">The updated Country information.</param>
        /// <returns>Returns a message indicating the success of the update operation.</returns>
        //Update(Company)
        [HttpPut]
        [Route("UpdateCountry")]
        [SwaggerOperation("UpdateCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public string UpdateCountry(Country country)
        {
            structureContext.Entry(country).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            structureContext.SaveChanges ();

            return "Country Updated";
        }

        /// <summary>
        /// Deletes a Country from the database.
        /// </summary>
        /// <param name="id">The ID of the Country to delete.</param>
        /// <returns>Returns a response indicating the success of the delete operation.</returns>
        //Delete(id)
        [HttpDelete]
        [Route("DeleteCountry")]
        [SwaggerOperation("DeleteCountry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public string DeleteCountry(int id)
        {
            Country country = structureContext.Countries.Where(x => x.CountryId == id).FirstOrDefault();
            if (country != null)
            {
                structureContext.Countries.Remove(country);
                structureContext.SaveChanges();
                return "Country Deleted";
            }
            else
            {
                return "No Country Found";
            }
        }

    }
}
