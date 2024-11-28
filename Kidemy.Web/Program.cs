using Barnamenevisan.Localizing.Statics;
using Kidemy.Application.Mediator;
using Kidemy.Application.Tools;
using Kidemy.Domain.Statics;
using Kidemy.Infra.IOC;
using Kidemy.Web.Configuration;
using Kidemy.Web.Controllers;
using Kidemy.Web.Filters;
using Kidemy.Web.Jobs;
using Kidemy.Web.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using NLog.Web;
using Quartz;
using Quartz.AspNetCore;
using System.Globalization;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // add filters
    var mvcBuilder = builder.Services.AddControllersWithViews(options =>
    {
        // TODO
        //options.Filters.Add(new CheckIsBan());
        options.Filters.Add(new SanitizeFilter());
    });

    // Add services to the container.

    // SiteTools
    builder.Configuration.GetSection("SiteTools").Get<SiteTools>();

    #region Data protection

    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/KeyDirectory/")))
        .SetApplicationName(SiteTools.SiteName)
        .SetDefaultKeyLifetime(TimeSpan.FromMinutes(int.Parse(builder.Configuration.GetSection("MachineKeyLifetime").Value)));

    #endregion

    #region Add Localization

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

    mvcBuilder.AddViewLocalization(
                LanguageViewLocationExpanderFormat.Suffix,
                options => options.ResourcesPath = "Resources");

    mvcBuilder.AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    });

    builder.Services.AddScoped<IStringLocalizer>(factory =>
        factory.GetRequiredService<IStringLocalizer<SharedResource>>()
    );

    #endregion

    // Authentication
    builder.ConfigAuthentication();

    // Dependencies
    var connectionString = builder.Configuration.GetConnectionString("KidemyConnectionString");
    DependencyContainer.RegisterDependencies(builder.Services, connectionString);

    // payment methods
    builder.Services.AddScoped<ZarinPalPaymentController>();

    // mediatR
    builder.Services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(typeof(IMediatorHandler).Assembly);
    });

    builder.Services.AddEasyCaching(options =>
    {
        options.UseInMemory();
    });

    //ReCapchaV3
    //builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();


    // NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    #region job
    builder.Services.AddQuartz(opt =>
    {
        var CloseQuestionAnswerAfter1MonthjobKey = new JobKey("CloseCourseQuestionAnswerAfter1Monthjob");
        opt.AddJob<CloseCourseQuestionAnswerAfter1Monthjob>(options =>
        {
            options.WithIdentity(CloseQuestionAnswerAfter1MonthjobKey);
        });
        opt.AddTrigger(options =>
        {
            options.ForJob(CloseQuestionAnswerAfter1MonthjobKey)
                   .WithIdentity("CloseCourseQuestionAnswerAfter1Monthjob-Trigger")
                   .WithSimpleSchedule(SimpleScheduleBuilder.RepeatHourlyForever(int.Parse(builder.Configuration.GetSection("CloseCourseQuestionAnswerAfter1Monthjob:AmountOfHour").Value ?? "48")));

        });
    });

    builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

    #endregion

    var app = builder.Build();

    CommonTools.SetLocalizer(app.Services.GetRequiredService<IStringLocalizer<SharedResource>>());


    #region Localization

    var supportedCultures = app.Configuration.GetSection("SupportedCultures:Cultures")
        .GetChildren()
        ?.Select(x => new CultureInfo(x.Value))
        .ToList();

    var defaultCulture = app.Configuration.GetSection("SupportedCultures")["DefaultCulture"].ToString();

    SiteTools.SupportedCultures = supportedCultures;
    SiteTools.DefaultCultureName = defaultCulture;
    LocalizingTools.SupportedCultures = SiteTools.SupportedCultures;
    LocalizingTools.DefaultCultureName = SiteTools.DefaultCultureName;

    var options = new RequestLocalizationOptions()
    {
        DefaultRequestCulture = new RequestCulture(defaultCulture),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures,
        RequestCultureProviders = new List<IRequestCultureProvider>()
        {
            new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        }
    };

    app.UseRequestLocalization(options);

    #endregion

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/HandleError/InternalError");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.Use(async (context, next) =>
    {
        await next();
        switch (context.Response.StatusCode)
        {
            case 404:
                context.Request.Path = "/HandleError/NotFound";
                await next();
                break;

            //case 403:
            //    context.Request.Path = "/HandleError/NotAuthorized";
            //    await next();
            //    break;

            default:
                break;
        }
    });

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();


    app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception? exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
