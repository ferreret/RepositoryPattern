using CommandApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
        opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET Single
app.MapGet("api/v1/commands/{commandId}", async (AppDbContext context, string commandId) => {
    var command = await context.Commands.FirstOrDefaultAsync(x => x.CommandId == commandId);

    if (command == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(command);
});

// Get multiple
app.MapGet("api/v1/commands", async (AppDbContext context) =>
{
    var commands = await context.Commands.ToListAsync();

    return Results.Ok(commands);
});

// // Create
// app.MapGet("api/v1/commands", async (AppDbContext context) => {

// });

// // Update
// app.MapGet("api/v1/commands", async (AppDbContext context) => {

// });

// app.MapGet("api/v1/commands", async (AppDbContext context) => {

// });

// app.MapGet("api/v1/commands", async (AppDbContext context) => {

// });

app.Run();