using AspNetCoreRateLimit;
using Kraken.Api.IoC;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var _servico = builder.Services;
var _ambiente = builder.Environment;

ConfigurationManager _configuracao = builder.Configuration;
//_servico.Configure<List<Configuracao>>(_configuracao.GetSection("Configuracao"));

//_servico.AddSingleton(_configuracao);
//_servico.Configure<List<Configuracao>>(_configuracao.GetSection("Configuracao"));

// Add services to the container.
_servico.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

_servico.AddCors(o => o.AddPolicy("AllowAnyOrigins", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
_servico.AddEndpointsApiExplorer();
_servico.AddHttpContextAccessor();

_servico.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ENGESOFT Consultoria - API Kraken",
        Description = "Serviço de gerenciamento.",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

_servico.AddSwaggerGen();
_servico.AddMemoryCache();

_servico.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "GET:/api/NotaFiscal/*",
            Period = "1h",
            Limit = 1
        },
        new RateLimitRule
        {
            Endpoint = "*",
            Period = "5s",
            Limit = 1,
        }
    };
});

_servico.AdicionarServicos(builder.Configuration);

var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("pt-BR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    //app.UseSwaggerUI();

    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Serviço Kraken V1");
    });
}

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseRequestLocalization();

//app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.UseCors("AllowAnyOrigins");

app.Run();