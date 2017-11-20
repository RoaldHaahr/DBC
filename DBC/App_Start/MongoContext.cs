using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace DBC.App_Start
{
    public class MongoContext
    {
        readonly MongoClient _client;
        public IMongoDatabase _database;
        public MongoContext()        //constructor   
        {
            // Reading credentials from Web.config file   
            var mongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"]; //CarDatabase  
            var mongoUsername = ConfigurationManager.AppSettings["MongoUsername"]; //demouser  
            var mongoPassword = ConfigurationManager.AppSettings["MongoPassword"]; //Pass@123  
            var mongoPort = ConfigurationManager.AppSettings["MongoPort"];  //27017  
            var mongoHost = ConfigurationManager.AppSettings["MongoHost"];  //localhost  

            // Creating credentials  
            var credential = MongoCredential.CreateMongoCRCredential
            (mongoDatabaseName,
                mongoUsername,
                mongoPassword);

            // Creating MongoClientSettings  
            var settings = new MongoClientSettings
            {
                Credentials = new[] { credential },
                Server = new MongoServerAddress(mongoHost, Convert.ToInt32(mongoPort))
            };
            _client = new MongoClient(settings);
            _database = _client.GetDatabase(mongoDatabaseName);
        }
    }
}