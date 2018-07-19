﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder.ConnectorEx
{
    using Microsoft.Bot.Connector;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// The Bot Connector REST API allows your bot to send and receive messages
    /// to channels configured in the
    /// [Bot Framework Developer Portal](https://dev.botframework.com). The
    /// Connector service uses industry-standard REST
    /// and JSON over HTTPS.
    ///
    /// Client libraries for this REST API are available. See below for a list.
    ///
    /// Many bots will use both the Bot Connector REST API and the associated
    /// [Bot State REST API](/en-us/restapi/state). The
    /// Bot State REST API allows a bot to store and retrieve state associated
    /// with users and conversations.
    ///
    /// Authentication for both the Bot Connector and Bot State REST APIs is
    /// accomplished with JWT Bearer tokens, and is
    /// described in detail in the [Connector
    /// Authentication](/en-us/restapi/authentication) document.
    ///
    /// # Client Libraries for the Bot Connector REST API
    ///
    /// * [Bot Builder for C#](/en-us/csharp/builder/sdkreference/)
    /// * [Bot Builder for Node.js](/en-us/node/builder/overview/)
    /// * Generate your own from the [Connector API Swagger
    /// file](https://raw.githubusercontent.com/Microsoft/BotBuilder/master/CSharp/Library/Microsoft.Bot.Connector.Shared/Swagger/ConnectorAPI.json)
    ///
    /// © 2016 Microsoft
    /// </summary>
    public class ConnectorClientWebSockets : IConnectorClient, System.IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        public System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        public JsonSerializerSettings SerializationSettings { get; private set; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        public JsonSerializerSettings DeserializationSettings { get; private set; }

        /// <summary>
        /// Subscription credentials which uniquely identify client subscription.
        /// </summary>
        public ServiceClientCredentials Credentials { get; private set; }

        /// <summary>
        /// Gets the IAttachments.
        /// </summary>
        public virtual IAttachments Attachments { get; private set; }

        /// <summary>
        /// Gets the IConversations.
        /// </summary>
        public virtual IConversations Conversations { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ConnectorClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Subscription credentials which uniquely identify client subscription.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public ConnectorClientWebSockets(System.Uri baseUri, ServiceClientCredentials credentials)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            BaseUri = baseUri;
            Credentials = credentials;
            Conversations = new ConversationOverWebSocket();
        }

        /// <summary>
        /// Initializes client properties.
        /// </summary>
        private void Initialize()
        {
            Attachments = null; // TODO new Attachments(this);
            //Conversations = new ConversationOverWebSocket();
            BaseUri = new System.Uri("https://api.botframework.com");
            SerializationSettings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            DeserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
        }

        public void Dispose()
        {

        }
    }
}
