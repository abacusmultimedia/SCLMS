
using Sclms.DI;
using Sclms.Persistence.Context;
using Sclms.Persistence.Modles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Sclms.UseCases.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
 

///// JWT 

var key = Encoding.UTF8.GetBytes("R7DmWIE0FzL1UfvP5jgOUdKRkGidt3Wgl6N+T2m4psk=");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7274/", // Replace with your issuer
            ValidAudience = "http://localhost:4200/", // Replace with your audience
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });




builder.Services.AddIdentity<AppUsers, IdentityRole>()
    .AddEntityFrameworkStores<DrillingDBContext>()
    .AddSignInManager<SignInManager<AppUsers>>();

builder.Services.AddDbContext<DrillingDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DrillingDBContext")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILicenseService, LicenseService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

builder.Services.AddApplication();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowCredentials().AllowAnyHeader().SetIsOriginAllowed((host) => true);
});
app.UseRouting();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chatHub");

app.Run();
