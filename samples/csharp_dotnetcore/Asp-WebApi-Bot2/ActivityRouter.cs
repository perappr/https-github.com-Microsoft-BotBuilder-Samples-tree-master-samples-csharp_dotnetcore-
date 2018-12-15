using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Schema;

namespace Asp_WebApi_Bot2
{
    public class ActivityRouter<ContextT, ReturnT>
    {
        public static Task<ReturnT> Route(Activity activity, ContextT context, IActivityEvents<ContextT, ReturnT> activityRouter, CancellationToken cancellationToken = default(CancellationToken))
        {
            switch (activity.Type)
            {
                case ActivityTypes.ContactRelationUpdate:
                    return activityRouter.OnContactRelationUpdate(context, activity.AsContactRelationUpdateActivity(), cancellationToken);

                case ActivityTypes.ConversationUpdate:
                    return activityRouter.OnConversationUpdate(context, activity.AsConversationUpdateActivity(), cancellationToken);

                case ActivityTypes.EndOfConversation:
                    return activityRouter.OnEndOfConversation(context, activity.AsEndOfConversationActivity(), cancellationToken);

                case ActivityTypes.Event:
                    return activityRouter.OnEvent(context, activity.AsEventActivity(), cancellationToken);

                case ActivityTypes.Handoff:
                    return activityRouter.OnHandoff(context, activity.AsHandoffActivity(), cancellationToken);

                case ActivityTypes.InstallationUpdate:
                    return activityRouter.OnInstallationUpdate(context, activity.AsInstallationUpdateActivity(), cancellationToken);

                case ActivityTypes.Invoke:
                    return activityRouter.OnInvoke(context, activity.AsInvokeActivity(), cancellationToken);

                case ActivityTypes.Message:
                    return activityRouter.OnMessage(context, activity.AsMessageActivity(), cancellationToken);

                case ActivityTypes.MessageDelete:
                    return activityRouter.OnMessageDelete(context, activity.AsMessageDeleteActivity(), cancellationToken);

                case ActivityTypes.MessageReaction:
                    return activityRouter.OnMessageReaction(context, activity.AsMessageReactionActivity(), cancellationToken);

                case ActivityTypes.MessageUpdate:
                    return activityRouter.OnMessageUpdate(context, activity.AsMessageUpdateActivity(), cancellationToken);

                case ActivityTypes.Suggestion:
                    return activityRouter.OnSuggestion(context, activity.AsSuggestionActivity(), cancellationToken);

                case ActivityTypes.Trace:
                    return activityRouter.OnTrace(context, activity.AsTraceActivity(), cancellationToken);

                case ActivityTypes.Typing:
                    return activityRouter.OnTyping(context, activity.AsTypingActivity(), cancellationToken);

                default:
                    return activityRouter.OnActivity(context, activity, cancellationToken);
            }
        }

    }
}
