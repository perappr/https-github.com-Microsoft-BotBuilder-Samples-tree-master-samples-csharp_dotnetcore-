using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;

namespace Asp_WebApi_Bot2
{
    public class EchoBot : IBot
    {
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            var botFrameworkAdapter = turnContext.Adapter as BotFrameworkAdapter;

            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text($"Echo xxxx '{turnContext.Activity.Text}' from bot 1."));
            }
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (turnContext.Activity.MembersAdded.Any())
                {
                    foreach (var member in turnContext.Activity.MembersAdded)
                    {
                        if (member.Id != turnContext.Activity.Recipient.Id)
                        {
                            await turnContext.SendActivityAsync(MessageFactory.Text($"Welcome {member.Name} to ASP MVC Bot 2."), cancellationToken);
                        }
                    }
                }
            }
        }
    }
}
