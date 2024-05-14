using Microsoft.Azure.Cosmos;
using CapitalPlacement.Repositories.Contract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Cosmos DB client
builder.Services.AddSingleton<CosmosClient>(serviceProvider =>
{
    string connectionString = builder.Configuration.GetConnectionString("CosmosDbConnectionString");
    return new CosmosClient(connectionString);
});

// Register repositories
builder.Services.AddSingleton<IRepository<ProgramModel>, CosmosDbRepository<ProgramModel>>();
builder.Services.AddSingleton<IRepository<QuestionModel>, CosmosDbRepository<QuestionModel>>();
builder.Services.AddSingleton<IRepository<AnswerModel>, CosmosDbRepository<AnswerModel>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();