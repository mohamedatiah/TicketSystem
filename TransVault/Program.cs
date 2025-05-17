using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TransfloUser;
using TransVault.Application.Interfaces;
using TransVault.Application.Services;
using TransVault.Domain.Interfaces;
using TransVault.Extensions;
using TransVault.Infrastructure.Data;
using TransVault.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapper(typeof(MappingConfig));

//builder.Services.AddDatabaseConfiguration();
//builder.Services.AddDbContext<TransVaultDbContext>(option =>
//{
//    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"))
//          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//});
builder.Services.AddDbContext<TransVaultDbContext>((provider, options) =>
{
    var interceptor = provider.GetRequiredService<AuditSaveChangesInterceptor>();

    options.UseInMemoryDatabase("TransVaultInMemoryDB").AddInterceptors(interceptor); // Give your in-memory database a name

    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    //       .AddInterceptors(interceptor);
});

// JWT Auth
var jwtKey = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
    };
});
// Dependency Injection - Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITicketNoteService, TicketNoteService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuditSaveChangesInterceptor>();


// Register infrastructure services (adapters)


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();
app.UseAuthentication();   // Must be before UseAuthorization
app.UseAuthorization();    // Required to enforce [Authorize]

app.MapControllers();
app.Run();
