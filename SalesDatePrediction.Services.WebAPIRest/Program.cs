using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Core;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Data;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.InfraStructure.Repository;
using SalesDatePrediction.Services.WebAPIRest.Helpers;
using SalesDatePrediction.Transversal.Common;
using SalesDatePrediction.Transversal.Logging;
using SalesDatePrediction.Transversal.Mapper;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;
using SalesDatePrediction.Application.Main;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Para iniciar las tareas en segundo plano.
//builder.Services.AddHostedService<RecurringBackgroundTasks>();
//builder.Services.AddHostedService<LifecycleBackgroundTasks>();
//builder.Services.Configure<HostOptions>(opciones =>
//{
//    opciones.ServicesStartConcurrently = true;
//    opciones.ServicesStopConcurrently = true;
//});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews().AddNewtonsoftJson();


builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });


var appSettingsSection = builder.Configuration.GetSection("Config");
builder.Services.Configure<AppSettings>(appSettingsSection);

//configure jwt authentication
var appSettings = appSettingsSection.Get<AppSettings>();

//Se especifican la vida útil de los servicios.
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();

//Se usa de Scoped o de ámbito porque necesitamos que se instancie una vez por solicitud
//Clientes
//builder.Services.AddScoped<IClientesApplication, ClientesApplication>();
//builder.Services.AddScoped<IClientesDomain, ClientesDomain>();
//builder.Services.AddScoped<IClientesRepository, ClientesRepository>();

builder.Services.AddScoped<ICustomersApplication, CustomersApplication>();
builder.Services.AddScoped<ICustomersDomain, CustomersDomain>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();

builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

var key = Encoding.ASCII.GetBytes(appSettings.Secret);
var IsSuer = appSettings.IsSuer;
var Audience = appSettings.Audience;

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.MapControllers();

app.Run();
