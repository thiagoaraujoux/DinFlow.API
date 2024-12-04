using DinFlow.API.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ApplicationDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Nosso Novo T�tulo do Swagger",
            Description = "Essa � a forma de fazermos uma nova descri��o de nossa API",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "API de Eventos do Leandro",
                Email = "leandrogarcia@unitins.br",
                Url = new Uri("https://www.unitins.br/nPortal/")
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
