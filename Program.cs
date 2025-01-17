
// using Serilog;
using Microsoft.EntityFrameworkCore;
// using Pomelo.EntityFrameworkCore.MySql;
using NewDotnetProject.Data;
using NewDotnetProject.Configurations;
using NewDotnetProject.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddTransient<IStudentsRepository, StudentRepoClass>();
builder.Services.AddTransient<IUserRepository, UserRepoClass>();
builder.Services.AddTransient<IItemRepository, ItemRepoClass>();
builder.Services.AddScoped(typeof(ICollegeRepository<>) , typeof(CollegeRepoClass<>));
// =================Logging ======================
builder.Logging.ClearProviders();
// builder.Logging.AddConsole();
// builder.Logging.AddDebug();
#region Serilog Settings

// ======= USING THIRD PARTY LOGGING =========
// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Information()
//     .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();

//=== override console log to log file
// builder.Host.UseSerilog();
//=== add log in log file also along with other default log
// builder.Logging.AddSerilog();

builder.Logging.AddLog4Net();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container. ======= Registration of database connection ============
builder.Services.AddDbContext<CollegeDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        new MySqlServerVersion(new Version(10, 4, 22)),
     mysqlOptions => mysqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null) // Retry settings

    )
);

builder.Services.AddCors(options => options.AddPolicy("MyTestCors" , Policy =>{
    // Allow all origins 
    Policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
}));

// JWT authentication configuration
builder.Services.AddAuthentication( options =>{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    // options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new  TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey  = new SymmetricSecurityKey(key),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

var app = builder.Build();

// Enable middleware for serving generated Swagger as a JSON endpoint.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Production"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyTestCors");
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>{
  endpoints.MapGet("api/testEndpoint",
    context => context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWTSecret")));  
});

app.MapControllers();
app.Run();

