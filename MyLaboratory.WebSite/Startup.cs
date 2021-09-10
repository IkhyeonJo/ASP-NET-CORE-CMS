using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyLaboratory.WebSite.Models;
using Microsoft.EntityFrameworkCore;
using MyLaboratory.WebSite.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MyLaboratory.Common.DataAccess.Data;
using MyLaboratory.Common.DataAccess.Models;
using System.Net;
using System.IO;
using MyLaboratory.WebSite.Filters;
using MyLaboratory.WebSite.Contracts;
using MyLaboratory.WebSite.Common;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using MyLaboratory.Common.DataAccess.Contracts;
using MyLaboratory.Common.DataAccess.Services;

namespace MyLaboratory.WebSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region ServerSettings
            ServerSetting.DomainName = Configuration.GetSection("ServerSetting")["DomainName"];
            ServerSetting.MaxLoginAttempt = Convert.ToInt32(Configuration.GetSection("ServerSetting")["MaxLoginAttempt"]);
            ServerSetting.SessionExpireMinutes = Convert.ToInt32(Configuration.GetSection("ServerSetting")["SessionExpireMinutes"]);
            ServerSetting.SmtpUserName = Configuration.GetSection("ServerSetting").GetSection("SmtpOptions")["smtpUserName"];
            ServerSetting.SmtpPassword = Configuration.GetSection("ServerSetting").GetSection("SmtpOptions")["smtpPassword"];
            ServerSetting.SmtpHost = Configuration.GetSection("ServerSetting").GetSection("SmtpOptions")["smtpHost"];
            ServerSetting.SmtpPort = Convert.ToInt32(Configuration.GetSection("ServerSetting").GetSection("SmtpOptions")["smtpPort"]);
            ServerSetting.SmtpSSL = Convert.ToBoolean(Configuration.GetSection("ServerSetting").GetSection("SmtpOptions")["smtpSSL"]);
            ServerSetting.FromEmail = Configuration.GetSection("ServerSetting").GetSection("SmtpOptions")["fromEmail"];
            ServerSetting.FromFullName = Configuration.GetSection("ServerSetting").GetSection("SmtpOptions")["fromFullName"];
            ServerSetting.IsDefault = Convert.ToBoolean(Configuration.GetSection("ServerSetting").GetSection("SmtpOptions")["IsDefault"]);
            ServerSetting.NoticeMaturityDateDay = Convert.ToInt32(Configuration.GetSection("ServerSetting")["NoticeMaturityDateDay"]);
            #endregion

            #region AddMariaDbContext
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                  //options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), providerOptions => providerOptions.EnableRetryOnFailure())); // �̰��ϴ� Excel Export �� ��
                  options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), providerOptions => providerOptions.EnableRetryOnFailure()).EnableSensitiveDataLogging());
            #endregion

            #region AddRepositories

            #region DB
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IExpenditureRepository, ExpenditureRepository>();
            services.AddScoped<IFixedIncomeRepository, FixedIncomeRepository>();
            services.AddScoped<IFixedExpenditureRepository, FixedExpenditureRepository>();
            #endregion

            #region LOCAL
            services.AddScoped<IMailRepository, MailRepository>();
            #endregion

            #endregion

            #region AddSession
            services.AddDistributedMemoryCache(); // Session Storage

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(ServerSetting.SessionExpireMinutes); 
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Domain = ServerSetting.DomainName.Replace("https://", "").Replace("/", "").ToString();
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });
            #endregion

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddResponseCaching(); // For HTTP Header Setting 

            #region Multi-Language
            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(
                opt =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("ko-KR")
                    };
                    opt.DefaultRequestCulture = new RequestCulture("en-US");
                    opt.SupportedCultures = supportedCultures;
                    opt.SupportedUICultures = supportedCultures;
                });
            #endregion

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(AuthorizationFilter)); // An instance
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //1.Exception / error handling

            //���� ����ȯ�濡�� ����Ǵ� ���
            //������ ���� ������ �̵���(UseDeveloperExceptionPage) �� �� ��Ÿ�� ������ �����մϴ�.
            //�����ͺ��̽� ���������� �̵��� �����ͺ��̽� ��Ÿ�� ������ �����մϴ�.
            //���� ���δ��� ȯ�濡�� ����Ǵ� ���
            //���� ó���� �̵����(UseExceptionHandler)�� ���� �̵����� �߻��� ���ܸ� �����մϴ�.
            //HSTS(HTTP Strict Transport Security Protocol) �̵����(UseHsts)�� Strict-Transport - Security����� �߰��մϴ�.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Exception/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection(); // 2. HTTPS ���𷺼� �̵���� (UseHttpsRedirection)�� HTTP ��û�� HTTPS�� ���𷺼��մϴ�.
            //app.UseStaticFiles(); // 3. ���� ���� �̵���� (UseStaticFiles)�� ���� ������ �����ϰ� �߰� ��û ó���� �ܶ���ŵ�ϴ�.
            // app.UseCookiePolicy(); 4. ��Ű ��å �̵���� (UseCookiePolicy)�� ���� EU �Ϲ� ������ ��ȣ ���� (GDPR) ������ �����ϴ�.


            app.UseRouting(); // 5. ��û�� ������ϱ����� �̵���� ����� (UseRouting)

            // app.UseRequestLocalization();
            // app.UseCors();


            #region Login
            app.UseAuthentication(); // 6.���� �̵����(UseAuthentication)�� ���� �ڿ��� ���� �׼����� ���Ǳ� ���� ����� ������ �õ��մϴ�.
            app.UseAuthorization(); // 7. ���� �̵���� (UseAuthorization)�� ����ڰ� ���� �ڿ��� �׼��� �� ���ִ� ������ �ο��մϴ�.
            app.UseSession(); // 8. ���� �̵���� (UseSession)�� ���� ���¸� �����ϰ� �����մϴ�. ���� ���� ���¸� ����ϴ� ��� ��Ű ��å �̵���� ���� �� MVC �̵���� ���� ���� �̵��� ȣ���Ͻʽÿ�.
            #endregion

            #region ���� ���� wwwroot�� �����Ϸ��� �ϸ� �ʱ� ȭ������ ��������
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    bool isAccountSessionExist = context.Context.Session.TryGetValue("AccountSession", out _); // �α����� �Ǿ� ���� ������ ������� Ȯ��

                    if (isAccountSessionExist == false && !context.Context.Request.Path.StartsWithSegments("/anonymous")) // �α����� �� �� �����ΰ�? �׷��ٸ� ~/anonymous �ڿ����� ���� ���
                    {
                        context.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // ���� ���� 
                        context.Context.Response.ContentLength = 0; // ���� ���� ����
                        context.Context.Response.Body = Stream.Null; // ���� ����
                        context.Context.Response.Redirect("/"); // �α��� ȭ������
                    }
                    else if (isAccountSessionExist == true)
                    {
                        context.Context.Session.TryGetValue("AccountSession", out byte[] resultByte);
                        var loginedAccount = JsonConvert.DeserializeObject<Account>(Encoding.Default.GetString(resultByte)); // Get Session

                        if (loginedAccount.Role == Role.Admin && !context.Context.Request.Path.StartsWithSegments("/admin")) // �α����� �Ǿ��� ������ Admin�ΰ�? �׷��ٸ� ~/admin �ڿ����� ���� ���
                        {
                            #region Profile Image �̰ų�, Profile ���� ���ε� �ƹ�Ÿ ���� ���� ���
                            if (context.Context.Request.Path.ToString().Contains("/upload/Management/Profile/default-avatar.jpg") || context.Context.Request.Path.ToString().Contains($"/upload/Management/Profile/{loginedAccount.Email}/"))
                            {

                            }
                            #endregion

                            #region �� �� ���� ����
                            else
                            {
                                context.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // ���� ���� 
                                context.Context.Response.ContentLength = 0; // ���� ���� ����
                                context.Context.Response.Body = Stream.Null; // ���� ����
                                context.Context.Response.Redirect("/"); // �α��� ȭ������
                            }
                            #endregion

                        }
                        else if(loginedAccount.Role == Role.User && !context.Context.Request.Path.StartsWithSegments("/user")) // �α����� �Ǿ��� ������ User�ΰ�? �׷��ٸ� ~/user �ڿ����� ���� ���
                        {
                            #region Profile Image �̰ų�, Profile ���� ���ε� �ƹ�Ÿ ���� ���� ���
                            if (context.Context.Request.Path.ToString().Contains("/upload/Management/Profile/default-avatar.jpg") || context.Context.Request.Path.ToString().Contains($"/upload/Management/Profile/{loginedAccount.Email}/"))
                            {

                            }
                            #endregion

                            #region �� �� ���� ����
                            else
                            {
                                context.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // ���� ���� 
                                context.Context.Response.ContentLength = 0; // ���� ���� ����
                                context.Context.Response.Body = Stream.Null; // ���� ����
                                context.Context.Response.Redirect("/"); // �α��� ȭ������
                            }
                            #endregion
                        }
                        else
                        {

                        }
                    }
                }
            });
            #endregion


            #region Default HTTP Header Setting
            //https://docs.microsoft.com/en-us/aspnet/core/performance/caching/middleware?view=aspnetcore-5.0
            app.UseResponseCaching();

            #region ��� �������� ������ ���� �ڵ带 �����Ѵ�. �α׾ƿ� �� �ڷΰ����ϸ� �α��� �ߴ� �������� ������ ������ ĳ�� Disable ����

            //<META http-equiv="Expires" content="-1">
            //<META http-equiv="Pragma" content="no-cache">
            //<META http-equiv="Cache-Control" content="No-Cache">

            //�Ʒ� �ڵ�� ���� Html Header�� ���� META ������ ����� ����.

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        //Public = true,
                        //MaxAge = TimeSpan.FromSeconds(10),
                        NoCache = true, // For resolve logout back button problem
                        NoStore = true // For resolve logout back button problem
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });
            #endregion
            #endregion

            //app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"); }); //9.���� ����Ʈ ����� �̵����(MapRazorPages �� �����ϴ� UseEndPoints)�� ����Ͽ� ��û ���������ο� Razor Pages ��������Ʈ�� �߰��մϴ�.

            #region Multi-Language
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
