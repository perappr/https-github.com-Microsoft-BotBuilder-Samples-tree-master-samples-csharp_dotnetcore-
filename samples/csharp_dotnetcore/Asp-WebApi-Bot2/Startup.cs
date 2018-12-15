using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Asp_WebApi_Bot2
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            this.HostingEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
                builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Hosting Environment information
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// Gets the configuration that represents a set of key/value application configuration properties.
        /// </summary>
        /// <value>
        /// The <see cref="IConfiguration"/> that represents a set of key/value application configuration properties.
        /// </value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            AddBotFrameworkAdapter(services);
        }

        private void AddBotFrameworkAdapter(IServiceCollection services)
        {
            var botFileSecret = Configuration.GetSection("botFileSecret")?.Value;
            var botFilePath = Configuration.GetSection("botFilePath")?.Value;

            if (!Path.IsPathRooted(botFilePath))
                botFilePath = Path.Combine(this.HostingEnvironment.ContentRootPath, botFilePath);

            BotConfiguration botConfig;
            try
            {
                // Loads .bot configuration file and adds a singleton that your Bot can access through dependency injection.
                botConfig = BotConfiguration.Load(botFilePath, botFileSecret);

                // register configuration file for anyone who wants to get it via DI
                services.AddSingleton(sp => botConfig ?? throw new InvalidOperationException($"The .bot configuration file could not be loaded. botFilePath: {botFilePath}"));
            }
            catch
            {
                var msg = @"Error reading bot file. Please ensure you have valid botFilePath and botFileSecret set for your environment.
    - You can find the botFilePath and botFileSecret in the Azure App Service application settings.
    - If you are running this bot locally, consider adding a appsettings.json file with botFilePath and botFileSecret.
    - See https://aka.ms/about-bot-file to learn more about .bot file its use and bot configuration.
    ";
                throw new InvalidOperationException(msg);
            }

            // Get active endpoint 
            EndpointService endpoint = botConfig.FindServiceByNameOrId(Configuration["botEndpoint"]) as EndpointService;

            // Memory Storage is for local bot debugging only. When the bot
            // is restarted, everything stored in memory will be gone.
            IStorage dataStore = new MemoryStorage();

            // For production bots use the Azure Blob or
            // Azure CosmosDB storage providers. For the Azure
            // based storage providers, add the Microsoft.Bot.Builder.Azure
            // Nuget package to your solution. That package is found at:
            // https://www.nuget.org/packages/Microsoft.Bot.Builder.Azure/
            // Un-comment the following lines to use Azure Blob Storage
            // // Storage configuration name or ID from the .bot file.
            // const string StorageConfigurationId = "<STORAGE-NAME-OR-ID-FROM-BOT-FILE>";
            // var blobConfig = botConfig.FindServiceByNameOrId(StorageConfigurationId);
            // if (!(blobConfig is BlobStorageService blobStorageConfig))
            // {
            //    throw new InvalidOperationException($"The .bot file does not contain an blob storage with name '{StorageConfigurationId}'.");
            // }
            // // Default container name.
            // const string DefaultBotContainer = "<DEFAULT-CONTAINER>";
            // var storageContainer = string.IsNullOrWhiteSpace(blobStorageConfig.Container) ? DefaultBotContainer : blobStorageConfig.Container;
            // IStorage dataStore = new Microsoft.Bot.Builder.Azure.AzureBlobStorage(blobStorageConfig.ConnectionString, storageContainer);

            // Create and add conversation state around dataStore
            var conversationState = new ConversationState(dataStore);

            // Create and add user state around dataStore
            var userState = new UserState(dataStore);

            // create Bot Framework Adapter with credentials for the endpoint 
            var adapter = new BotFrameworkAdapter(new SimpleCredentialProvider(endpoint.AppId, endpoint.AppPassword))
                .Use(new AutoSaveStateMiddleware(conversationState, userState));

            // setup up what to do when our logic completely fails (tell user something)
            adapter.OnTurnError = async (context, exception) => await context.SendActivityAsync("Sorry, it looks like something went wrong.");

            // register it so our controller can pick it up and use it
            services.AddSingleton<BotFrameworkAdapter>(adapter);

            // add bot as a singleton 
            services.AddSingleton<IBot>(new EchoBot());
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDefaultFiles()
               .UseStaticFiles()
               .UseHttpsRedirection()
               .UseMvc();
        }
    }
}
