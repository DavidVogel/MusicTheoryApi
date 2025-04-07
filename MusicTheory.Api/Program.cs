using MusicTheory.Services;
using MusicTheory.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MusicTheory.Domain;
using Asp.Versioning;
using NSwag;
using NSwag.Examples;
using MusicTheory.Domain.Examples;

var builder = WebApplication.CreateBuilder(args);

// Configure EF Core to use a database
string? baseConnectionString;

if (builder.Environment.IsDevelopment())
{
    // Use local SQL Server
    baseConnectionString = builder.Configuration.GetConnectionString("LOCAL_CONNECTIONSTRING");
}
else
{
    baseConnectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}

// EnableRetryOnFailure
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(baseConnectionString,
        connectionOptions => { connectionOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null); }
    ));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Password settings (strength requirements)
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT authentication
var jwtConfig = builder.Configuration.GetSection("JWT");
string jwtSecret = jwtConfig["Secret"];
string jwtIssuer = jwtConfig["Issuer"];
string jwtAudience = jwtConfig["Audience"];

// AddAuthentication configures the default scheme as JWT Bearer, then uses AddJwtBearer to set up token validation parameters.
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = false;
        if (jwtSecret != null)
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                ValidateIssuer = true,
                ValidIssuer = jwtIssuer,
                ValidateAudience = true,
                ValidAudience = jwtAudience,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                // No tolerance for expired tokens (immediate expiry)
                ClockSkew = TimeSpan.Zero
            };
    });

// Add authorization services
builder.Services.AddAuthorization();

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

// Register application services and repositories for DI
builder.Services.AddScoped<IScaleService, ScaleService>();
builder.Services.AddScoped<IChordService, ChordService>();
builder.Services.AddScoped<IProgressionService, ProgressionService>();
builder.Services.AddSingleton<IScaleRepository, InMemoryScaleRepository>();
builder.Services.AddSingleton<IProgressionRepository, InMemoryProgressionRepository>();

// Configure examples
builder.Services.AddExampleProviders(typeof(ChordProgressionExample).Assembly);
builder.Services.AddExampleProviders(typeof(ChordProgressionsExample).Assembly);
builder.Services.AddExampleProviders(typeof(ChordExample).Assembly);

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument((config, provider) =>
{
    config.AddExamples(provider);
    config.UseControllerSummaryAsTagDescription = true;
    config.PostProcess = doc =>
    {
        // Clear the default server(s)
        doc.Servers.Clear();

        // Remove duplicative server config if present in extension data
        if (doc.ExtensionData?.ContainsKey("servers") == true)
        {
            doc.ExtensionData.Remove("servers");
        }

        // Add custom server
        doc.Servers.Add(new OpenApiServer
        {
            Url = "https://musictheoryapi.com",
            Description = "Production server"
        });

        doc.Info = new OpenApiInfo
        {
            Title = "Music Theory API",
            Version = "v0.0.1",
            Description = "API for music theory concepts and operations",
            License = new OpenApiLicense
            {
                Name = "Elastic License 2.0",
                Url = "https://www.elastic.co/licensing/elastic-license"
            },
        };

        doc.Info.ExtensionData ??= new Dictionary<string, object>()!;
        doc.Info.ExtensionData["x-logo"] = new Dictionary<string, object>
        {
            { "url", "/img/george-crumb-notation.jpg" },
            { "altText", "Music Theory API Logo" }
        };

        doc.Tags = new List<OpenApiTag>
        {
            new()
            {
                Name = "registerdto_model",
                ExtensionData = new Dictionary<string, object>
                {
                    ["x-displayName"] = "The User Registration Model",
                    ["description"] = "<SchemaDefinition schemaRef=\"#/components/schemas/RegisterDto\" />"
                }!
            },
            new()
            {
                Name = "logindto_model",
                ExtensionData = new Dictionary<string, object>
                {
                    ["x-displayName"] = "The User Login Model",
                    ["description"] = "<SchemaDefinition schemaRef=\"#/components/schemas/LoginDto\" />"
                }!
            },
            new()
            {
                Name = "chord_model",
                ExtensionData = new Dictionary<string, object>
                {
                    ["x-displayName"] = "The Chord Model",
                    ["description"] = "<SchemaDefinition schemaRef=\"#/components/schemas/Chord\" />"
                }!
            },
            new()
            {
                Name = "note_model",
                ExtensionData = new Dictionary<string, object>
                {
                    ["x-displayName"] = "The Note Model",
                    ["description"] = "<SchemaDefinition schemaRef=\"#/components/schemas/Note\" />"
                }!
            },
            new()
            {
                Name = "notename_model",
                ExtensionData = new Dictionary<string, object>
                {
                    ["x-displayName"] = "The Note Name Model",
                    ["description"] = "<SchemaDefinition schemaRef=\"#/components/schemas/NoteName\" />"
                }!
            },
            new()
            {
                Name = "chordtype_model",
                ExtensionData = new Dictionary<string, object>
                {
                    ["x-displayName"] = "The Chord Type Model",
                    ["description"] = "<SchemaDefinition schemaRef=\"#/components/schemas/ChordType\" />"
                }!
            },
            new()
            {
                Name = "chord_progression_model",
                ExtensionData = new Dictionary<string, object>
                {
                    ["x-displayName"] = "The Chord Progression Model",
                    ["description"] = "<SchemaDefinition schemaRef=\"#/components/schemas/ChordProgression\" />"
                }!
            }
        };
        doc.ExtensionData ??= new Dictionary<string, object>()!;
        doc.ExtensionData["x-tagGroups"] = new[]
        {
            new
            {
                name = "Controllers", tags = new[]
                {
                    "Auth",
                    "Chords",
                    "Progressions",
                    "Scales"
                }
            },
            new
            {
                name = "Models", tags = new[]
                {
                    "registerdto_model",
                    "logindto_model",
                    "chord_model",
                    "note_model",
                    "notename_model",
                    "chordtype_model",
                    "chord_progression_model"
                }
            }
        };
    };
});

builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(0);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new HeaderApiVersionReader("X-Api-Version"));
    })
    .AddMvc() // This is needed for controllers
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi(); // http://localhost:<port>/swagger/v1/swagger.json
    app.UseSwaggerUi();

    // Add ReDoc UI to interact with the document
    // Available at: http://localhost:<port>/redoc
    app.UseReDoc(options =>
    {
        options.Path = "/redoc";
    });
}

app.UseHttpsRedirection();

// UseAuthentication must come before UseAuthorization, and both should come before app.MapControllers (or any endpoint mapping). This way, for each request, the JWT middleware will run and set the user principal if the token is valid, and then the [Authorize] attributes can allow or deny access based on that user.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
