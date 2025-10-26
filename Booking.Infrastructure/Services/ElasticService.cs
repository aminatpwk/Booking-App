using Booking.Application.Common.Configurations;
using Booking.Application.Common.Services;
using Booking.Domain.Users;
using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;

namespace Booking.Infrastructure.Services
{
    public class ElasticService : IElasticService
    {
        private readonly ElasticsearchClient _client;
        private readonly ElasticSettings _settings;

        public ElasticService(IOptions<ElasticSettings> optionsMonitor)
        {
            _settings = optionsMonitor.Value;
            var settings = new ElasticsearchClientSettings(new Uri(_settings.Url))
                //.Authentication()
                .DefaultIndex(_settings.DefaultIndex);

            _client = new ElasticsearchClient(settings);
        }

        public async Task<bool> AddOrUpdate(User user)
        {
            var response =  await _client.IndexAsync(user, idx =>
            idx.Index(_settings.DefaultIndex)
            .OpType(OpType.Index));

            return response.IsValidResponse;
        }

        public async Task<bool> AddOrUpdateBullk(IEnumerable<User> users, string indexName)
        {
            var response = await _client.BulkAsync(b =>
            b.Index(_settings.DefaultIndex)
            .UpdateMany(users, (ud, u) => ud.Doc(u).DocAsUpsert(true)));

            return response.IsValidResponse;
        }

        public async Task CreateIndexIfNotExistsAsync(string indexName)
        {
            if (!_client.Indices.Exists(indexName).Exists)
            {
                 await _client.Indices.CreateAsync(indexName);
            }
        }

        public async Task<User> Get(string key)
        {
            var response = await _client.GetAsync<User>(key, g => 
            g.Index(_settings.DefaultIndex));

            return response.Source;
        }

        public async Task<List<User>> GetAll()
        {
            var response = await _client.SearchAsync<User>(s =>
            s.Index(_settings.DefaultIndex));

            return response.IsValidResponse ? response.Documents.ToList() : default;
        }

        public async Task<bool> Remove(string key)
        {
            var response = await _client.DeleteAsync<User>(key, d =>
            d.Index(_settings.DefaultIndex));

            return response.IsValidResponse;
        }

        public async Task<long?> RemoveAll()
        {
            var response = await _client.DeleteByQueryAsync<User>(
                _settings.DefaultIndex,
                d => d.Query(q => q.MatchAll())
            );

            return response.IsValidResponse ? response.Deleted : default;
        }
    }
}
