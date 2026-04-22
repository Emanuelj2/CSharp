using BankTransactionProcessor.Data;
using BankTransactionProcessor.Modles;
using BankTransactionProcessor.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



//connset to the inmemory database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BankTransactionProcessorDB"));

//add the bank service to the dependency injection container
builder.Services.AddScoped<BankService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



//seed some initial data into the in-memory database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.BankAccounts.Add(new BankAccount
    {
        Id = 1,
        AccountNumber = "ACC123",
        Balance = 1000,
        Type = BankTransactionProcessor.Modles.Type.Checking
    });

    context.SaveChanges();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
