using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DaprPub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<PublishController> _logger;
        public PublishController(ILogger<PublishController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }
        [HttpGet]
        [Route("Health")]
        public IActionResult Get()
        {
            return new OkObjectResult("Publish controller running successfully");
        }
        [HttpGet]
        [Route("PutInQueue")]
        public async Task<IActionResult> GetDapr()
        {
            try
            {
                string message = $"Hello World {DateTime.Now.ToString()}_{DateTime.Now.Ticks} ";
                await _daprClient.PublishEventAsync
                    (Constant.PUBSUB_NAME, Constant.QUEUE_NAME, message);
                _logger.LogInformation("Successfully published message in queue");
                return new OkResult();
            }
            catch(Exception ex)
            {
                _logger.LogInformation("message publishing failed");
                throw;

            }
        }
    }
}
