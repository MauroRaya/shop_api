using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using shop_api.Application.Services;
using shop_api.Infra.Contexts;
using shop_api.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddProblemDetails(options => 
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
        
        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Infra/Data/app.db"));

builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddScoped<OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("api/ping", () => "pong");

app.Run();