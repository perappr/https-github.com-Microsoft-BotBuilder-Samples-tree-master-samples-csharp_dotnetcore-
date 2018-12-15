using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Asp_WebApi_Bot2
{
    public interface IActivityEvents<ContextT, ReturnT>
    {
        Task<ReturnT> OnActivity(ContextT context, IActivity activity, CancellationToken cancellationToken);
        Task<ReturnT> OnContactRelationUpdate(ContextT context, IContactRelationUpdateActivity contactRelationUpdate, CancellationToken cancellationToken);
        Task<ReturnT> OnConversationUpdate(ContextT context, IConversationUpdateActivity conversationUpdate, CancellationToken cancellationToken);
        Task<ReturnT> OnEndOfConversation(ContextT context, IEndOfConversationActivity endOfConversation, CancellationToken cancellationToken);
        Task<ReturnT> OnEvent(ContextT context, IEventActivity @event, CancellationToken cancellationToken);
        Task<ReturnT> OnHandoff(ContextT context, IHandoffActivity handoff, CancellationToken cancellationToken);
        Task<ReturnT> OnInstallationUpdate(ContextT context, IInstallationUpdateActivity installationUpdate, CancellationToken cancellationToken);
        Task<ReturnT> OnInvoke(ContextT context, IInvokeActivity invoke, CancellationToken cancellationToken);
        Task<ReturnT> OnMessage(ContextT context, IMessageActivity message, CancellationToken cancellationToken);
        Task<ReturnT> OnMessageDelete(ContextT context, IMessageDeleteActivity messageDelete, CancellationToken cancellationToken);
        Task<ReturnT> OnMessageReaction(ContextT context, IMessageReactionActivity messageReaction, CancellationToken cancellationToken);
        Task<ReturnT> OnMessageUpdate(ContextT context, IMessageUpdateActivity messageUpdate, CancellationToken cancellationToken);
        Task<ReturnT> OnSuggestion(ContextT context, ISuggestionActivity suggestion, CancellationToken cancellationToken);
        Task<ReturnT> OnTrace(ContextT context, ITraceActivity trace, CancellationToken cancellationToken);
        Task<ReturnT> OnTyping(ContextT context, ITypingActivity typing, CancellationToken cancellationToken);
    }
}
