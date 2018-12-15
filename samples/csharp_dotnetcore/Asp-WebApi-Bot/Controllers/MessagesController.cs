using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;

namespace Asp_WebApi_Bot2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private BotFrameworkAdapter botframeworkAdapter;
        private IBot bot;

        public MessagesController(BotFrameworkAdapter adapter, IBot bot)
        {
            this.botframeworkAdapter = adapter;
            this.bot = bot;
        }

        // Bot framework protocol does a POST to api/messages with Activity object
        [HttpPost]
        public async Task<object> Post([FromBody] Activity activity)
        {
            return await botframeworkAdapter.ProcessActivityAsync(Request.Headers["Authorization"], activity, bot.OnTurnAsync, default(CancellationToken));
        }
    }
}
