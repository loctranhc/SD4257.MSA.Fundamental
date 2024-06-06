using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MSA.Common.Contracts.Domain;
using MSA.Common.Contracts.Settings;
using MSA.Common.MongoDB;

namespace MSA.Common.Mongo
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        //Register Mongo Client
        services.AddSingleton(ServiceProvider => {
            var configuration = ServiceProvider.GetService<IConfiguration>();
            var serviceSetting = configuration?.GetSection(nameof(ServiceSetting)).Get<ServiceSetting>();
            var mongoDBSetting = configuration?.GetSection(nameof(MongoDBSetting)).Get<MongoDBSetting>();
            var mongoClient = new MongoClient(mongoDBSetting?.ConnectionString);
            
            return mongoClient.GetDatabase(serviceSetting?.ServiceName);
        });

        return services;
    }

    public static IServiceCollection AddRepositories<T>(this IServiceCollection services, string collectionName) where T : IEntity
    {
        services.AddSingleton<IRepository<T>>(serviceProvider => 
        {
            var database = serviceProvider.GetService<IMongoDatabase>();

            if(database is null)
                throw new NullReferenceException($"Not able to solve {nameof(IMongoDatabase)} service.");

            return new MongoRepository<T>(database, collectionName);
        });
        return services;
    }
    }
}