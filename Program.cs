using MovieApi.Middleware;
using MovieApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using MovieApi.Services.DataServices;
using MovieApi.AppMapping;
using System.Xml;
using MassTransit;
using Microsoft.Extensions.Configuration;
using MovieApi.MassTransit.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateBootstrapLogger();

// Add logger
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Movie API",
        Description = "Movie API",
        TermsOfService = new Uri("https://github.com/openapitools/openapi-generator"),
        Contact = new OpenApiContact
        {
            Name = "Bohdan",
            Url = new Uri("https://github.com/Navatusein"),
            Email = "boghdan.kutsulima@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "Mit License",
            Url = new Uri("https://github.com/Navatusein/Goose-Hub-Movie-API/blob/main/LICENSE")
        },
        Version = "v1",
    });

    options.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{Assembly.GetEntryAssembly()!.GetName().Name}.xml");

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.OperationFilter<SecurityRequirementsOperationFilter>(true, jwtSecurityScheme.Reference.Id);
});

// Configure Frontend Authentication Service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthorizeJWT:Issuer"],
            ValidAudience = builder.Configuration["AuthorizeJWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthorizeJWT:Key"]))
        };
    });

// Configure Automapper
builder.Services.AddAutoMapper(typeof(AppMappingService));
builder.Services.AddTransient<ImageUrlResolver>();
builder.Services.AddTransient<ImageListUrlResolver>();
builder.Services.AddTransient<ContentUrlResolver>();

// Add Minio Service
builder.Services.AddSingleton<MinioService>();

// Add MongoDb Services
builder.Services.AddSingleton<MongoDbConnectionService>();
builder.Services.AddSingleton<AnimeService>();
builder.Services.AddSingleton<MovieService>();
builder.Services.AddSingleton<SerialService>();
builder.Services.AddSingleton<FranchiseService>();
builder.Services.AddSingleton<CommonService>();

// Add MassTransit
builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<AnimeAddContentConsumer>();
    options.AddConsumer<MovieAddContentConsumer>();
    options.AddConsumer<SerialAddContentConsumer>();
    options.AddConsumer<ContentExistConsumer>();

    options.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("movie-api", false));

    options.UsingRabbitMq((context, config) =>
    {
        var host = builder.Configuration.GetSection("RabbitMq:Host").Get<string>();

        config.Host(host, "/", host =>
        {
            host.Username(builder.Configuration.GetSection("RabbitMq:Username").Get<string>());
            host.Password(builder.Configuration.GetSection("RabbitMq:Password").Get<string>());
        });

        config.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add Exception Handling Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(options => {
    string[] origins = builder.Configuration.GetSection("Origins").Get<string[]>()!;

    options.WithOrigins(origins);
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowCredentials();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

