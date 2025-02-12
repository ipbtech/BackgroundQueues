using BackgroundQueues.Models;
using BackgroundQueues.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundQueues.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController(OperationQueue operationQueue) : ControllerBase
    {
        [HttpPost("{name}")]
        public async Task<ActionResult> Start([FromRoute] string name, CancellationToken cancellationToken)
        {
            var workItem = new OperationItem(name);
            await operationQueue.QueueWorkItemAsync(workItem, cancellationToken);

            return Ok($"Operation \'{name}\' was added to the queue");
        }
    }
}
