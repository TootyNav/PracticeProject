using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using ServiceBusConsumer;
using WebApplication_mvc_test_ai;
using WebApplication_mvc_test_ai.Data;
using WebApplication_mvc_test_ai.Hubs;
using WebApplication_mvc_test_ai.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ITelemetryInitializer, TelemetryEnrichment>();
builder.Services.AddSignalR();

//builder.Services.AddSingleton<IQueueClient>(x =>new QueueClient(builder.Configuration["ServiceBus:CONNECTION_STRING"], builder.Configuration["ServiceBus:QueueName"]));
builder.Services.AddSingleton<ITopicClient>(x =>new TopicClient(
    builder.Configuration["ServiceBus:CONNECTIONSTRING"],
    builder.Configuration["ServiceBus:TopicName"]));

builder.Services.AddSingleton<IMessagePublisher, MessageTopicPublisher>();

builder.Services.AddApplicationInsightsTelemetry(c => c.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS:CONNECTION_STRING"]);
//builder.Services.AddHostedService<CalculatorConsumer2>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");


app.Run();
