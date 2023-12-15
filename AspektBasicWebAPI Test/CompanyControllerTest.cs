using AspektBasicWebAPI.Controllers;
using AspektBasicWebAPI.Models;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;


namespace AspektBasicWebAPI_Test.Controllers
{
    public class CompanyControllerTest
    {

        [Test]
        public void GetCompaniesReturnListOfCompanies()
        {
            
            // Arrange
            var options = new DbContextOptionsBuilder<StructureContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            /*
            using (var context = new StructureContext(options))
            {
                var companies = new List<Company>
                {
                    new Company { CompanyId = 1, CompanyName = "Company1" },
                    new Company { CompanyId = 2, CompanyName = "Company2" }
                };

                context.Companies.AddRange(companies);
                context.SaveChanges();
            }
            */

            using (var context = new StructureContext(options))
            {
                var controller = new CompanyController(context);

                // Act
                var result = controller.GetCompanies();

                // Assert
                NUnit.Framework.Assert.That(result, Is.Not.Null);
                NUnit.Framework.Assert.That(result, Is.TypeOf<List<Company>>());
                NUnit.Framework.Assert.That(result.Count, Is.EqualTo(2));
                NUnit.Framework.Assert.That(result[0].CompanyId, Is.EqualTo(1));
                NUnit.Framework.Assert.That(result[0].CompanyName, Is.EqualTo("Company1"));
                NUnit.Framework.Assert.That(result[1].CompanyId, Is.EqualTo(2));
                NUnit.Framework.Assert.That(result[1].CompanyName, Is.EqualTo("Company2"));
            }
            
        }

    }
        
}

