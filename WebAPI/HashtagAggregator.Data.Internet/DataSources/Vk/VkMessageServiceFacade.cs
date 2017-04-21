﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using HashtagAggregator.Core.Contracts.Interface.DataSources;
using HashtagAggregator.Core.Entities.VkEntities;
using HashtagAggregator.Core.Models.Commands;
using HashtagAggregator.Core.Models.Interface.Cqrs.Command;
using HashtagAggregator.Core.Models.Results.Query.Message;
using HashtagAggregator.Data.Internet.Assemblers;
using HashtagAggregator.Shared.Common.Helpers;
using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregator.Shared.Common.Settings;
using HashtagAggregator.Shared.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HashtagAggregator.Data.Internet.DataSources.Vk
{
    public class VkMessageServiceFacade : IVkMessageFacade
    {
        private readonly IOptions<VkSettings> settings;
        private readonly ILogger<VkMessageServiceFacade> logger;

        public VkMessageServiceFacade(IOptions<VkSettings> settings, ILogger<VkMessageServiceFacade> logger)
        {
            this.settings = settings;
            this.logger = logger;
        }

        public async Task<MessagesQueryResult> GetAllAsync(HashTagWord hashtag)
        {
            using (WebRequestWrapper request = new WebRequestWrapper())
            {
                VkMessageQuery query =
                    new VkMessageQuery(settings.Value.MessagesApiUrl,
                    settings.Value.ApiVersion)
                    {
                        Query = hashtag.ToString()
                    };

                var json = await request.LoadJsonAsync(HttpMethod.Get, query.ToString());
                var jObject = JObject.Parse(json).SelectToken("response").ToString();

                if (String.IsNullOrEmpty(jObject))
                {
                    logger.LogError(
                        LoggingEvents.EXCEPTION_GET_TWITTER_MESSAGE,
                        "Failed to get messages by {hashtag} with {error}",
                        hashtag,
                        jObject);
                    throw new InvalidDataException(jObject);
                }

                VkNewsFeed feed = JsonConvert.DeserializeObject<VkNewsFeed>(jObject);
                VkMessageResultMapper mapper = new VkMessageResultMapper();
                return mapper.MapSingle(feed, hashtag);
            }
        }

        public async Task<ICommandResult> Save(IEnumerable<MessageCreateCommand> filtered)
        {
            //todo: refactor
            throw new NotImplementedException();
            //try
            //{
            //    var seconds = 1;
            //    foreach (var command in filtered)
            //    {
            //        BackgroundJob.Schedule<IVkBackgroundJob<MessageCreateCommand>>(
            //            x => x.Publish(command),
            //            TimeSpan.FromSeconds(seconds));
            //        seconds += settings.Value.VkMessagePublishDelay;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
            //return new CommandResult { Success = true };
        }

        public async Task<MessagesQueryResult> GetNumberAsync(int number, string hashtag)
        {
            throw new NotImplementedException();
        }

        public async Task<MessagesQueryResult> GetSinceLastIdAsync(long lastId, string hashtag)
        {
            throw new NotImplementedException();
        }
    }
}
