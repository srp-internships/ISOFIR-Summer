using StudentCard.Application;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(
    builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException(),
        builder.Configuration.GetSection("AppSettings:Token").Value!);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors(s => s.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build());
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();