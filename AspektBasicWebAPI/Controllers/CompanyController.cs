using AspektBasicWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AspektBasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly StructureContext structureContext;

        public CompanyController(StructureContext structureContext)
        {
            this.structureContext = structureContext;
        }

        /// <summary>
        /// Retrieves a List of all Companies.
        /// </summary>
        /// <returns>Retrieves a List of all Companies in the database. </returns>
        //Get()
        [HttpGet]
        [Route("GetAllCompanies")]
        [SwaggerOperation("GetAllCompanies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public List<Company> GetCompanies()
        {
            return structureContext.Companies.ToList();
        }

        /// <summary>
        /// Retrieves a specific Company by ID.
        /// </summary>
        /// <param name="id">The ID of the Company to retrieve.</param>
        /// <returns>Returns the Company with the ID.</returns>
        //Get()
        [HttpGet]
        [Route("GetCompanyById")]
        [SwaggerOperation("GetCompanyById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public Company GetCompany(int id)
        {
            return structureContext.Companies.Where(x => x.CompanyId == id).FirstOrDefault();
        }

        /// <summary>
        /// Add a new Company to the database.
        /// </summary>
        /// <param name="company">The Company to add.</param>
        /// <returns>Returns a message indicating the success of the add operation.</returns>
        //Create(Company)
        [HttpPost]
        [Route("AddCompany")]
        [SwaggerOperation("AddCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public string AddCompany(Company company)
        {
            string response = string.Empty;
            structureContext.Companies.Add(company);
            structureContext.SaveChanges();
            return "Company Added";
        }

        /// <summary>
        /// Update an existing Company to the database.
        /// </summary>
        /// <param name="company">The updated Company information.</param>
        /// <returns>Returns a message indicating the success of the updating operation.</returns>
        //Update(Company)
        [HttpPut]
        [Route("UpdateCompany")]
        [SwaggerOperation("UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public string UpdateCompany(Company company)
        {
            structureContext.Entry(company).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            structureContext.SaveChanges();
            return "Compant Updated";
        }

        /// <summary>
        /// Deletes a Company from the database.
        /// </summary>
        /// <param name="id">The ID from the Company to delete.</param>
        /// <returns>Returns a response indicating the success of the delete operation</returns>
        //Delete()
        [HttpDelete]
        [Route("DeleteCompany")]
        [SwaggerOperation("DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public string DeleteCompany(int id)
        {
            Company company = structureContext.Companies.Where(x => x.CompanyId == id).FirstOrDefault();
            if (company != null)
            {
                structureContext.Companies.Remove(company);
                structureContext.SaveChanges();
                return "Company Deleted";
            }
            else
            {
                return "No Company Found";
            }

        }
    }
}
