
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TrainAppMOTD.API.DataBase;
using TrainAppMOTD.API.DataBase.Context;
using TrainAppMOTD.API.DataBase.DAL;
using TrainAppMOTD.API.DataBase.DAL.Interfaces;
using TrainAppMOTD.API.RabbitMQ;
using TrainAppMOTD.API.Services;
using TrainAppMOTD.API.Services.Inteface;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

////////////////////////////////////////////////////////
//Dodanie secretsów
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

////////////////////////////////////////////////////////
//Dodanie DB+Connection string w secretsach 
builder.Services.AddDbContext<TrainAppMotdDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("myConxStr")));

////////////////////////////////////////////////////////
//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://zenquotes.io");
                      });
});



////////////////////////////////////////////////////////
//RABBIT+MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderRequestConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {

        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("sendRespond", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<OrderRequestConsumer>(provider);
        });
    }));
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDailyMOTDService, DailyMOTDService>();
builder.Services.AddScoped<IMotdRepository, MotdRepository>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//place for routing
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
