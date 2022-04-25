using CompanyCore.Dto;
using CompanyCore.Entity;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ConpanyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly IDriver _driver;

        public CompanyController()
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "1"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            var statementText = new StringBuilder();
            statementText.Append("MATCH (n) RETURN n");

            var session = this._driver.AsyncSession();
            var result = await session.ReadTransactionAsync(tx => tx.RunAsync(statementText.ToString()));

            var companyList = result.Select(x => new Company()
            {
                companyName = x.CompanyName,
            });
            return StatusCode(201, "Node has been created in the database");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {

            return Ok();
        }

        [HttpPost]
        public IActionResult Create(CompanyModels companyModels)
        {

            return Ok();
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> CreateNode(string name)
        {
            var statementText = new StringBuilder();
            statementText.Append("CREATE (person:Person {name: $name})");
            var statementParameters = new Dictionary<string, object>
            {
                {"name", name }
            };

            var session = this._driver.AsyncSession();
            var result = await session.WriteTransactionAsync(tx => tx.RunAsync(statementText.ToString(), statementParameters));
            return StatusCode(201, "Node has been created in the database");
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id)
        {

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            return Ok();
        }
    }
}
