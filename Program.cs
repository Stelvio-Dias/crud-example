using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KambaiaAPIDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("database")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var clients = app.MapGroup("clients").WithTags("Clients");

// Get all Clients
clients.MapGet("/", async (IRepository<Client> repository) => Results.Ok(await repository.GetAllClientsAsync()));

// Get Client By Id
clients.MapGet("/{id}", async (int id, IRepository<Client> repository) => Results.Ok(await repository.GetClientById(id)));

// Add Client
clients.MapPost("/", async ([FromBody]Client client, IRepository<Client> repository) => {
    await repository.AddClient(client);
    return Results.Created();
});

// Update Client
clients.MapPut("/", async ([FromBody]Client client, IRepository<Client> repository) =>
{
    var clientObj = await repository.GetClientById(client.Id);

    if (clientObj is null) return Results.NotFound("Nenhum cliente Encontrado");

    clientObj.Email = client.Email ?? clientObj.Email;
    clientObj.Name = client.Name ?? clientObj.Name;
    clientObj.Phone = client.Phone ?? clientObj.Phone;

    await repository.UpdateClient(client);

    return Results.Ok("Cliente editado");
});

// Remove client
clients.MapDelete("/{id}", async (int id, IRepository<Client> repository) => 
{
    var client = await repository.GetClientById(id);

    if (client is null) return Results.NotFound("Nenhum cliente Encontrado");

    await repository.RemoveClient(client);

    return Results.Ok("Cliente removido");
});

app.Run();
