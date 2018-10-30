This sample shows how to create a web page with customized [Web Chat](https://github.com/Microsoft/BotFramework-WebChat/) component.

![Screenshot of styled Web Chat](https://raw.githubusercontent.com/Microsoft/BotBuilder-Samples/v4/samples/javascript_es6/70.styling-webchat/screenshot.png)

> You will need to obtain bot secret from a bot hosted on Azure Bot Services. You can follow this [article](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-channel-connect-webchat?view=azure-bot-service-3.0#step-1) to get the bot secret key.

- Clone this repository
   ```sh
   git clone https://github.com/Microsoft/BotBuilder-Samples.git
   ```
- In a terminal,
   ```sh
   cd samples/javascript_es6/70.styling-webchat
   ```
- In [`index.html`](https://github.com/Microsoft/BotBuilder-Samples/tree/v4/samples/javascript_es6/70.styling-webchat), put your bot secret key by replacing `YOUR_BOT_SECRET_FROM_AZURE` with the key
- Host it using [`serve`](https://npmjs.com/package/serve)
   ```sh
   npx serve
   ```
- Navigate to the page at [http://localhost:5000](http://localhost:5000/)

# Further Reading

- [Bot Framework Documentation](https://docs.botframework.com/)
- [Web Chat GitHub repository](https://github.com/Microsoft/BotFramework-WebChat/tree/v4)
- [Connect a bot to Web Chat](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-channel-connect-webchat?view=azure-bot-service-3.0#step-1)
- [Customizing Web Chat](https://github.com/Microsoft/BotFramework-WebChat/blob/v4/SAMPLES.md)
