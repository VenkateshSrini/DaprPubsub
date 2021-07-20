using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr;
using CloudNative.CloudEvents.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Daprsub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly ILogger<SubscribeController> _logger;
        public SubscribeController(ILogger<SubscribeController> logger) => _logger = logger;
        [Topic(Constant.PUBSUB_NAME, Constant.QUEUE_NAME)]
        [HttpPost("/OnMessageArrived")]
        public IActionResult OnMessageArrived(string message)
        {
            _logger.LogInformation($"Message from Queue {message}");
            return Ok();
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Running successfully");
        }
    }
}
