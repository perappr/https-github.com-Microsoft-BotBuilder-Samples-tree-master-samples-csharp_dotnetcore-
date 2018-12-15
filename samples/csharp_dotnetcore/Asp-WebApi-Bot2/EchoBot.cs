// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace Asp_WebApi_Bot2
{

    /// <summary>
    /// Represents a bot that echo's back the original input from the user.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// </summary>
    /// <remarks>A <see cref="BotAdapter"/> passes incoming activities from the user's
    /// channel to the bot's <see cref="OnTurnAsync(ITurnContext, CancellationToken)"/> method.
    /// In this case, the Activity messages originate from console input, which flows through
    /// sample's <see cref="ConsoleAdapter"/> and the Bot Framework pipeline which calls this
    /// object.
    /// </remarks>
    /// <seealso cref="IMiddleware"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.ibot?view=botbuilder-dotnet-preview"/>
    public class EchoBot : IBot, IActivityEvents<ITurnContext, InvokeResponse>
    {
        public Task<InvokeResponse> OnActivity(ITurnContext context, IActivity activity, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnContactRelationUpdate(ITurnContext context, IContactRelationUpdateActivity contactRelationUpdate, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnConversationUpdate(ITurnContext context, IConversationUpdateActivity conversationUpdate, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnEndOfConversation(ITurnContext context, IEndOfConversationActivity endOfConversation, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnEvent(ITurnContext context, IEventActivity @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnHandoff(ITurnContext context, IHandoffActivity handoff, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnInstallationUpdate(ITurnContext context, IInstallationUpdateActivity installationUpdate, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnInvoke(ITurnContext context, IInvokeActivity invoke, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnMessage(ITurnContext context, IMessageActivity message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnMessageDelete(ITurnContext context, IMessageDeleteActivity messageDelete, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnMessageReaction(ITurnContext context, IMessageReactionActivity messageReaction, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnMessageUpdate(ITurnContext context, IMessageUpdateActivity messageUpdate, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnSuggestion(ITurnContext context, ISuggestionActivity suggestion, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnTrace(ITurnContext context, ITraceActivity trace, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<DialogTurnResult> OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task<InvokeResponse> OnTyping(ITurnContext context, ITypingActivity typing, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MyDialog : ActivityDialog
    {
        public MyDialog(string dialogId =null) : base(dialogId)
        {

        }

        public override Task<DialogTurnResult> OnMessage(DialogContext context, IMessageActivity message, CancellationToken cancellationToken)
        {
            if (message.Text == "foo")
            {

            }
            return base.OnMessage(context, message, cancellationToken);
        }

        public override Task<DialogTurnResult> OnTyping(DialogContext dialogContext, ITypingActivity typing, CancellationToken cancellationToken)
        {
            dialogContext.Context.SendActivityAsync(Activity.CreateMessageActivity());
            return base.OnTyping(dialogContext, typing, cancellationToken);
        }
    }


    public interface ITurnActivityRouter : IActivityEvents<ITurnContext, InvokeResponse>
    {

    }

    public interface IDialogActivityRouter : IActivityEvents<DialogContext, DialogTurnResult>
    {

    }

    public static class DialogActivityRouter
    {
        private static Task<DialogTurnResult> Route(Activity activity, DialogContext context, IDialogActivityRouter activityRoutes, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ActivityRouter<DialogContext,DialogTurnResult>.Route(activity, context, activityRoutes, cancellationToken);
        }
    }


    public class ActivityDialog : Dialog, IActivityEvents<DialogContext, DialogTurnResult>
    {
        public ActivityDialog(string dialogId = null) : base(dialogId)
        {
        }

        public override Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ActivityRouter<DialogContext, DialogTurnResult>.Route(dc.Context.Activity, dc, this, cancellationToken);
        }

        public override Task<DialogTurnResult> ContinueDialogAsync(DialogContext dc, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ActivityRouter<DialogContext, DialogTurnResult>.Route(dc.Context.Activity, dc, this, cancellationToken);
        }

        // ---------------------- ACTIVITY EVENT HANDLERS

        public virtual Task<DialogTurnResult> OnActivity(DialogContext context, IActivity activity, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnContactRelationUpdate(DialogContext context, IContactRelationUpdateActivity contactRelationUpdate, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnConversationUpdate(DialogContext context, IConversationUpdateActivity conversationUpdate, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnEndOfConversation(DialogContext context, IEndOfConversationActivity endOfConversation, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnEvent(DialogContext context, IEventActivity @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnHandoff(DialogContext context, IHandoffActivity handoff, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnInstallationUpdate(DialogContext context, IInstallationUpdateActivity installationUpdate, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnInvoke(DialogContext context, IInvokeActivity invoke, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Waiting));
        }

        public virtual Task<DialogTurnResult> OnMessage(DialogContext context, IMessageActivity message, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Waiting));
        }

        public virtual Task<DialogTurnResult> OnMessageDelete(DialogContext context, IMessageDeleteActivity messageDelete, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Waiting));
        }

        public virtual Task<DialogTurnResult> OnMessageReaction(DialogContext context, IMessageReactionActivity messageReaction, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnMessageUpdate(DialogContext context, IMessageUpdateActivity messageUpdate, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnSuggestion(DialogContext context, ISuggestionActivity suggestion, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnTrace(DialogContext context, ITraceActivity trace, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }

        public virtual Task<DialogTurnResult> OnTyping(DialogContext context, ITypingActivity typing, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DialogTurnResult(DialogTurnStatus.Complete));
        }
    }
}
