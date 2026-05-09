var builder = WebApplication.CreateBuilder(args);

// Rejestracja kontrolerów w kontenerze DI
builder.Services.AddControllers();

// Opcjonalnie Swagger ułatwiający testowanie
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapowanie tras z atrybutów w kontrolerach
app.MapControllers();

app.Run();