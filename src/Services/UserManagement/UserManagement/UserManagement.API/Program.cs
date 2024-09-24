//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Serilog;
using UserManagement.API;

var builder = WebApplication.CreateBuilder(args);

// Use Startup class to configure services
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
// Replace the default .NET Core logger with Serilog
builder.Host.UseSerilog();

var app = builder.Build();

// Use Startup class to configure the HTTP request pipeline
startup.Configure(app, app.Environment);

app.Run();
