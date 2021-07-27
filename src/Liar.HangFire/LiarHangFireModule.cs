using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using Hangfire.PostgreSql;
using Hangfire.SQLite;
using Liar.Domain.Shared;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;

namespace Liar.HangFire
{
    [DependsOn(typeof(AbpBackgroundJobsHangfireModule))]
    public class LiarHangFireModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHangfire(config =>
            {
                var tablePrefix = LiarConsts.DbTablePrefix + "hangfire";

                switch (AppSettings.DBType)
                {
                    case "MySql":
                        config.UseStorage(
                            new MySqlStorage(AppSettings.ConnectionStrings,
                            new MySqlStorageOptions
                            {
                                TablePrefix = tablePrefix
                            }));
                        break;

                    case "Sqlite":
                        config.UseSQLiteStorage(AppSettings.ConnectionStrings, new SQLiteStorageOptions
                        {
                            SchemaName = tablePrefix
                        });
                        break;

                    case "SqlServer":
                        //config.UseSqlServerStorage(AppSettings.ConnectionStrings, new SqlServerStorageOptions
                        //{
                        //    SchemaName = tablePrefix
                        //});
                        break;

                    case "PostgreSql":
                        config.UsePostgreSqlStorage(AppSettings.ConnectionStrings, new PostgreSqlStorageOptions
                        {
                            SchemaName = tablePrefix
                        });
                        break;
                }
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHangfireServer();
            app.UseHangfireDashboard(options: new DashboardOptions
            {
                Authorization = new[]
                {
                    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = false,
                        SslRedirect = false,
                        LoginCaseSensitive = true,
                        Users = new []
                        {
                            new BasicAuthAuthorizationUser
                            {
                                Login = AppSettings.Hangfire.Login,
                                PasswordClear =  AppSettings.Hangfire.Password
                            }
                        }
                    })
                },
                DashboardTitle = "任务调度中心"
            });

            var service = context.ServiceProvider;

            service.UseTestJob();
        }
    }
}
