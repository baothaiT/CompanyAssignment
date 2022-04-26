using CompanyCore.Dto;
using CompanyCore.Entity;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System;

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

            var records = new List<Company>();
            var session = this._driver.AsyncSession();
            try
            {
                return await session.ReadTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync("MATCH (p:Person) RETURN p.name as companyName ,Id(p) as companyId");

                    var resultRecord = await result.ToListAsync();
                    foreach (var item in resultRecord)
                    {
                        records.Add(
                            new Company
                            {
                                companyName = ValueExtensions.As<string>(item["companyName"]),
                                companyId = ValueExtensions.As<string>(item["companyId"])
                            });
                    }

                    //List<string> peopleList = await result.ToListAsync(r => r[0].As<string>());
                    return Ok(records);
                });
            }
            finally
            {
                await session.CloseAsync();
            }

        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {

            var session = this._driver.AsyncSession();
            try
            {
                return await session.ReadTransactionAsync(async tx =>
                {
                    Console.WriteLine("MATCH (p: Person) WHERE id(p)= {0} RETURN p.name", id);

                    var result = await tx.RunAsync("MATCH (p: Person) WHERE id(p)= {0} RETURN p.name", id);

                    List<string> peopleList = await result.ToListAsync(r => r[0].As<string>());
                    return Ok(peopleList);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
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
