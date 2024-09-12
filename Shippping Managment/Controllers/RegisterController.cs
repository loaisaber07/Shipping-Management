using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost("AddEmployee")]   
        public ActionResult AddEmployee()
        {
            return NoContent();
        
        }
    }
}
