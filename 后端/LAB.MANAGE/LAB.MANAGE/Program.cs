using LAB.MANAGE;
using LAB.MODEL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 添加 Consul 注册
builder.Services.RegisterConsul(builder.Configuration);

// 读取配置文件中的数据库连接字符串
string connectionString = builder.Configuration.GetConnectionString("BooksDatabase");

// 注册BooksContext
builder.Services.AddDbContext<LAB.DB.LabContext>(options =>
    options.UseSqlServer(connectionString));

// 添加其他服务
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// 这里注册服务的地方
builder.Services.AddScoped<LAB.SERVERS.ISysUserService, LAB.SERVERS.SysUserServiceImp>();
builder.Services.AddScoped<LAB.SERVERS.IAcademyService,LAB.SERVERS.AcademyServiceImp>();
builder.Services.AddScoped<LAB.SERVERS.ISemesteresService,LAB.SERVERS.SemesteresServiceImp>();
builder.Services.AddScoped<LAB.SERVERS.ILaboratoriesService,LAB.SERVERS.LaboratoriesServiceImp>();
builder.Services.AddScoped<LAB.SERVERS.IFloorService,LAB.SERVERS.FloorServiceImp>();
builder.Services.AddScoped<LAB.SERVERS.IBuilderService,LAB.SERVERS.BuilderServiceImp>();
builder.Services.AddScoped<LAB.SERVERS.IDailySafetyCheckService, LAB.SERVERS.DailySafetyCheckServiceImp>();
builder.Services.AddScoped<LAB.SERVERS.IHandingService,LAB.SERVERS.HandingServiceImp>();
builder.Services.AddScoped<LAB.SERVERS.IRepairsService, LAB.SERVERS.RepairsServiceImp>();
builder.Services.AddScoped<LAB.REPOSITORY.db_handing>();
builder.Services.AddScoped<LAB.REPOSITORY.db_repairs>();    
builder.Services.AddScoped<LAB.REPOSITORY.db_dailySafetyCheck>();
builder.Services.AddScoped<LAB.REPOSITORY.db_builder>();
builder.Services.AddScoped<LAB.REPOSITORY.db_floor>();
builder.Services.AddScoped<LAB.REPOSITORY.db_laboratories>();
builder.Services.AddScoped<LAB.REPOSITORY.db_academy>();
builder.Services.AddScoped<LAB.REPOSITORY.db_semesteres>();
builder.Services.AddScoped<LAB.REPOSITORY.db_sysuser>();



// 获取appsettings.json文件中配置认证中密钥（Secret）跟受众（Aud）信息
var audienceConfig = builder.Configuration.GetSection("Audience");
// 获取安全秘钥
var signingKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
// token要验证的参数集合
var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
{
    ValidateIssuerSigningKey = true, // 必须验证安全秘钥
    IssuerSigningKey = signingKey, // 赋值安全秘钥
    ValidateIssuer = true, // 必须验证签发人
    ValidIssuer = audienceConfig["Iss"], // 赋值签发人
    ValidateAudience = true, // 必须验证受众
    ValidAudience = audienceConfig["Aud"], // 赋值受众
    ValidateLifetime = true, // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
    ClockSkew = TimeSpan.Zero, // 允许的服务器时间偏移量
    RequireExpirationTime = true // 是否要求Token的Claims中必须包含Expires
};
// 添加服务验证，方案为TestKey
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "TestKey";
})
.AddJwtBearer("TestKey", x =>
{
    x.RequireHttpsMetadata = false;
    // 在JwtBearerOptions配置中，IssuerSigningKey(签名秘钥)、ValidIssuer(Token颁发机构)、ValidAudience(颁发给谁)三个参数是必须的。
    x.TokenValidationParameters = tokenValidationParameters;
});



var app = builder.Build();
app.UseCors("AllowAllOrigins");


app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // 认证
app.UseAuthorization(); // 授权

app.MapControllers();

app.Run();
