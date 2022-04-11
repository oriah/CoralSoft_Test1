using Microsoft.AspNetCore.Mvc;
using TestProject1.Web.Helpers;
using TestProject1.Web.Models;

namespace TestProject1.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {


        [HttpGet]
        [Route("page")]
        public IActionResult Page(int from = 0, int itemsPerPage = 20)
        {
            var x = LoadAPIItems(from, itemsPerPage);
            return Ok(x);
        }

        private object LoadAPIItems(int @from, int itemsPerPage)
        {
            string url = $@"https://jsonplaceholder.typicode.com/photos";

            var x = WebAPIClient.WebApiGet<List<DataItem>>(url);
            var x0 = x.Skip(from).Take(itemsPerPage);
            return x0;
        }







        //[HttpGet]
        //[Route("test")]
        //public IActionResult Test()
        //{
        //    return Ok("API All OK !!");
        //}

        //[HttpPost]
        //[Route("test2")]
        //public IActionResult Test2()
        //{
        //    return Ok("API All OK !!");
        //}


    }

}
