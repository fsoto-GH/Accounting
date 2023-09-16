using Accounting.API.DAOs.Account;
using Accounting.API.DAOs.Person;
using Accounting.API.DAOs.Transaction;
using Accounting.API.Services.Account;
using Accounting.API.Services.Person;
using Accounting.API.Services.Transaction;
using Accounting.API.DAOs;
using Accounting.API.Services.Person.PasswordHasher;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersonDao, PersonDao>();
builder.Services.AddScoped<IAccountDao, AccountDao>();
builder.Services.AddScoped<ITransactionDao, TransactionDao>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
