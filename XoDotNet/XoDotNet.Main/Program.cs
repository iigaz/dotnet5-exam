using XoDotNet.Features.Helpers;
using XoDotNet.Main.Configuration;
using XoDotNet.Main.ServicesExtensions;
using XoDotNet.Mediator.DependencyInjectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator(AssemblyReference.Assembly);
builder.Services.AddAuth(builder.Configuration.Get<JwtConfig>()!);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();