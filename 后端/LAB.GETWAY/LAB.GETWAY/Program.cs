using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("OcelotConfig.json", optional: false, reloadOnChange: true); // ȷ���ļ���һ��
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ��ȡ appsettings.json �ļ���������֤����Կ��Secret�������ڣ�Aud����Ϣ
var audienceConfig = builder.Configuration.GetSection("Audience");
// ��ȡ��ȫ��Կ
var signingKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
// token Ҫ��֤�Ĳ�������
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true, // ������֤��ȫ��Կ
    IssuerSigningKey = signingKey, // ��ֵ��ȫ��Կ
    ValidateIssuer = true, // ������֤ǩ����
    ValidIssuer = audienceConfig["Iss"], // ��ֵǩ����
    ValidateAudience = true, // ������֤����
    ValidAudience = audienceConfig["Aud"], // ��ֵ����
    ValidateLifetime = true, // �Ƿ���֤ Token ��Ч��
    ClockSkew = TimeSpan.Zero, // ����ķ�����ʱ��ƫ����
    RequireExpirationTime = true, // �Ƿ�Ҫ�� Token �� Claims �б������ Expires
};

// ��ӷ�����֤������Ϊ TestKey
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = "TestKey";
})
.AddJwtBearer("TestKey", x =>
{
    x.RequireHttpsMetadata = false;
    // �� JwtBearerOptions �����У�IssuerSigningKey��ǩ����Կ����ValidIssuer��Token �䷢��������ValidAudience���䷢��˭�����������Ǳ���ġ�
    x.TokenValidationParameters = tokenValidationParameters;
});

// ��� Ocelot ���ط���ʱ������ Secret ��Կ��Iss ǩ���ˡ�Aud ����
builder.Services.AddOcelot(builder.Configuration).AddConsul().AddPolly();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// ����Ҫ���� swagger �Ļ� ���������
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // ����ģʽ
{
  
}

app.UseHttpsRedirection();

app.UseCors("AllowAnyPolicy");

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.MapControllers();

app.Run();
