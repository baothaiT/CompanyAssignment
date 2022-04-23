using CompanyCore.Models;
using CompanyCore.Entity;
using Microsoft.AspNetCore.Mvc;


namespace ConpanyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {

            return Ok();
        }

        [HttpPost]
        public IActionResult Create( CompanyModels companyModels)
        {

            return Ok();
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
