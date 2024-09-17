using Data_Access_Layer.DTO.WeightDTO;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        private readonly IWeight weightRepo;

        public WeightController(IWeight weightRepo)
        {
            this.weightRepo = weightRepo;
        }

        //[HttpPost]
        //public async Task<ActionResult> AddWeightSettings(AddWeightSettingsDTO settingsDTO)
        //{


        //}
    }
}
