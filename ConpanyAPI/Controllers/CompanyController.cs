using CompanyCore;
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

        //[HttpPut]
        //public IActionResult Update()
        //{

        //    return Ok();
        //}

        //[HttpDelete]
        //public IActionResult Delete()
        //{

        //    return Ok();
        //}
    }
}
