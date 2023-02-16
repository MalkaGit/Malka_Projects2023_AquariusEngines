using IotRestApi.Model;
using IotRestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IotRestApi.Controllers
{
    //https://www.tutorialsteacher.com/webapi/implement-post-method-in-web-api
    [ApiController]
    [Route("[controller]")]
    public class IotAlertController : ControllerBase
    {
        private readonly IotAlertService _service;
        private readonly ILogger<IotAlertController> _logger;

        public IotAlertController(IotAlertService service, ILogger<IotAlertController> logger)
        {
            _service    = service;
            _logger     = logger;
        }

        [HttpGet(Name = "GetIotAlert")]
        public IEnumerable<IotAlert> Get(int pageNumber = 1, int pageSize = 100, string orderBy = "AlertId", bool asc = true)
        {
            return _service.GetIotAlerts(pageNumber,  pageSize,  orderBy,  asc);

        }


        [HttpPost(Name = "PostIotAlert")]
        public int Post (IotAlert iotAlert)
        {
            return _service.CreateIotAlert(iotAlert);

        }
    }
}

