using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); // UseSqlite or UseNpgsql for PostgreSQL

/* // Enable CORS (for frontend later)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}); */

// Add authentication services
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

//builder.Services.AddControllers();

// Use ReferenceHandler.Preserve - Prevents the circular reference from causing an infinite loop.
// and Serializes objects with unique references.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Apply Migrations & Seed Database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Apply migrations if needed

    var passwordHasher = new PasswordHasher<User>();
    SeedDatabase(dbContext, passwordHasher); // Seed initial data
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Function to Seed Initial Data
void SeedDatabase(AppDbContext context, PasswordHasher<User> passwordHasher)
{
    if (!context.Users.Any()) // Only seed if no users exist
    {
        // Seed Users
        var U1 = new User
        {
            Id = 1,
            Username = "John Doe",
            Email = "john@example.com",
            PasswordHash = passwordHasher.HashPassword(null, "Password123")
        };
        var U2 = new User
        {
            Id = 2,
            Username = "Jane Smith",
            Email = "jane@example.com",
            PasswordHash = passwordHasher.HashPassword(null, "SecurePass456")
        };

        context.Users.Add(U1);
        context.Users.Add(U2);

        context.SaveChanges();
    }
    if (context.Users.Any()) // If there are users seed accounts
    {
        // Seed Accounts  //  AccountType { Checking, Savings, Debt, Cash }
        var A1 = new Account 
        { 
            Id = 1, 
            Name = "CheckingAccount", 
            Type = AccountType.Checking, 
            Balance = 1500.00m, 
            UserId = 1 
        };
        var A2 = new Account 
        { 
            Id = 2, 
            Name = "SavingsAccount", 
            Type = AccountType.Savings, 
            Balance = 5000.00m, 
            UserId = 1
        };
        var A3 = new Account 
        { 
            Id = 3, 
            Name = "Cash", 
            Type = AccountType.Cash, 
            Balance = 200.00m, 
            UserId = 2
        };

        context.Accounts.Add(A1);
        context.Accounts.Add(A2);
        context.Accounts.Add(A3);

        context.SaveChanges();
    }
    if (context.Accounts.Any()) // If there are accounts seed transactions
    {
        // Seed Transactions  // TransactionType { Income, Expense, Transfer }
        // DateTime example = new DateTime(2018, 4, 4, 16, 0, 0);
        var T1 = new Transaction 
        { 
            Id = 1, 
            AccountId = 1, 
            Amount = -100.00m, 
            Type = TransactionType.Expense, 
            Description = "Groceries", 
            Date = new DateTime(2025, 5, 5, 16, 0, 0)
        };
        var T2 = new Transaction 
        { 
            Id = 2, 
            AccountId = 2, 
            Amount = 500.00m, 
            Type = TransactionType.Income, 
            Description = "Salary", 
            Date = new DateTime(2025, 4, 4, 9, 0, 0)
        };
        var T3 = new Transaction 
        { 
            Id = 3, 
            AccountId = 3, 
            Amount = 500.00m, 
            Type = TransactionType.Income, 
            Description = "CashGift", 
            Date = new DateTime(2025, 3, 3, 10, 0, 0)
        };

        context.Transactions.Add(T1);
        context.Transactions.Add(T2);
        context.Transactions.Add(T3);

        context.SaveChanges();
    }
}