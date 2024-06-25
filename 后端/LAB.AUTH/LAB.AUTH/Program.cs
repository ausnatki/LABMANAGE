
using LAB.AUTH;
using LAB.AUTH.DataAccessor;
using LAB.AUTH.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Add services to the container.
// ��ȡ�����ļ��е����ݿ������ַ���
string connectionString = builder.Configuration.GetConnectionString("BooksDatabase");

// ע��BooksContext
builder.Services.AddDbContext<AuthContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions();
builder.Services.AddDbContext<AuthContext>();
builder.Services.Configure<Audience>(builder.Configuration.GetSection("Audience"));
builder.Services.AddScoped<LAB.AUTH.Server.ILoginService, LAB.AUTH.Server.LoginServiceImp>();
// consul��������
builder.Services.RegisterConsul(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
