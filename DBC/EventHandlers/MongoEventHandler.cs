using System;
using System.Linq;
using DBC.App_Start;
using DBC.Models.MongoModels;
using MongoDB.Bson;
using MongoDB.Driver;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using Umbraco.Web.PublishedContentModels;

namespace DBC.EventHandlers
{
    public class MongoEventHandler : ApplicationEventHandler
    {
        private readonly MongoContext _dbContext;
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentService.Published += AddToMongo;
        }

        public void AddToMongo(IPublishingStrategy sender, PublishEventArgs<IContent> args)
        {
            foreach (var article in args.PublishedEntities.Where(x => x.ContentType.Alias == Blogpost.ModelTypeAlias))
            {
                var document = _dbContext._database.GetCollection<BsonDocument>("article");

                var id = article.Id;
                var name = article.Name;
                var publicationDate = article.ReleaseDate ?? new DateTime();
                var url = "";

                var articleModel = new Article
                {
                    Id = id,
                    Name = name,
                    PublicationDate = publicationDate,
                    Url = url
                };

                //var count = document.Count(new FilterDefinitionBuilder<>());

                var result = document.InsertOne(articleModel.ToBsonDocument());
            }
        }
    }
}