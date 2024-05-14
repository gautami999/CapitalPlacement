using Microsoft.Azure.Cosmos;
using System.Net;
using CapitalPlacement.Repositories.Contract;

public class CosmosDbRepository<T> : IRepository<T> where T : class
{
    private readonly Container _container;
    private readonly string databaseName;
    private readonly string containerName;

    public CosmosDbRepository(CosmosClient cosmosClient)
    {
        databaseName = "DataBase1";
        containerName = "Container1";
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var query = _container.GetItemQueryIterator<T>(new QueryDefinition("SELECT * FROM c"));
        var results = new List<T>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task CreateAsync(T item)
    {
        await _container.CreateItemAsync(item, new PartitionKey(Guid.NewGuid().ToString()));
    }

    public async Task UpdateAsync(T item)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(Guid.NewGuid().ToString()));
    }

    public async Task DeleteAsync(int id)
    {
        await _container.DeleteItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
    }
}