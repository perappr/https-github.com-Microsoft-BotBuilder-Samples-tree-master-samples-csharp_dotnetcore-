using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Asp_WebApi_Bot2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        /// <summary>
        /// This is the adapter which hosts the bot with a middleware pipeline and uses the ConnectorClient
        /// to implement the BotFramework HTTP protocol.
        /// </summary>
        private BotFrameworkAdapter botframeworkAdapter;

        /// <summary>
        /// This it the bot which is agnostic to adapter
        /// </summary>
        private IBot bot;

        public MessagesController(BotFrameworkAdapter adapter, IBot bot)
        {
            // use singleton IBot from DI as my bot
            this.bot = bot;

            // use singleton BotFramworkAdapter from DI as the adapter with middleware pipeline and credentials for my bot
            this.botframeworkAdapter = adapter;
        }

        // Bot framework protocol does a POST to api/messages with Activity object
        [HttpPost]
        public async Task<object> Post([FromBody] Activity activity)
        {
            // run adapter pipeline and pass activity to bot
            return await botframeworkAdapter.ProcessActivityAsync(Request.Headers["Authorization"], activity, bot.OnTurnAsync, default(CancellationToken));
        }
    }
}
