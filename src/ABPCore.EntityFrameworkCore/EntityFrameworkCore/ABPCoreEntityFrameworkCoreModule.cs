using System;
using System.Data.Common;
using Abp.Collections.Extensions;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.EntityFrameworkCore.Uow;
using Abp.Modules;
using Abp.MultiTenancy;
using Abp.Reflection;
using Abp.Reflection.Extensions;
using Abp.Runtime.Session;
using ABPCore.Configuration;
using ABPCore.Web;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ABPCore.EntityFrameworkCore
{
    [DependsOn(
        typeof(ABPCoreCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class ABPCoreEntityFrameworkCoreModule : AbpModule
    {

        private readonly ITypeFinder _typeFinder;


        public ABPCoreEntityFrameworkCoreModule(ITypeFinder typeFinder)
        {
            this._typeFinder = typeFinder;
            this.Logger = NullLogger.Instance;
        }

        public override void PreInitialize()
        {
            Configuration.ReplaceService<IConnectionStringResolver, CustomerDBConnectionStringResolver>(DependencyLifeStyle.Transient);

            Configuration.Modules.AbpEfCore().AddDbContext<BPDbContext>(options =>
            {

                if (options.ExistingConnection != null)
                {
                    options.DbContextOptions.UseSqlServer(options.ExistingConnection);
                }
                else
                {
                    options.DbContextOptions.UseSqlServer(options.ConnectionString);
                }
            });



            Configuration.Modules.AbpEfCore().AddDbContext<EPlusDBConext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    options.DbContextOptions.UseSqlServer(options.ExistingConnection);
                }
                else
                {
                    options.DbContextOptions.UseSqlServer(options.ConnectionString);
                }
            });


            Configuration.Modules.AbpEfCore().AddDbContext<ProjectManageDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    options.DbContextOptions.UseSqlServer(options.ExistingConnection);
                }
                else
                {
                    options.DbContextOptions.UseSqlServer(options.ConnectionString);
                }

            });

            RegisterGenericRepositories();
        }



        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ABPCoreEntityFrameworkCoreModule).GetAssembly());
        }


        private void RegisterGenericRepositories()
        {
            base.IocManager.Register<ExtendedRepositoryRegistrar>();
            ITypeFinder tFinder = this._typeFinder;
            Type[] dbContextTypes = tFinder.Find(ExtendedDbContextHelper.DoesDBNeedRegisterGenericRepositories);
            if (dbContextTypes.IsNullOrEmpty<Type>())
            {
                this.Logger.Warn("No class found derived from ExtendedDbContextBase.");
                return;
            }
            using (IDisposableDependencyObjectWrapper<ExtendedRepositoryRegistrar> repositoryRegistrar = base.IocManager.ResolveAsDisposable<ExtendedRepositoryRegistrar>())
            {
                Type[] array = dbContextTypes;
                for (int i = 0; i < array.Length; i++)
                {
                    Type dbContextType = array[i];
                    repositoryRegistrar.Object.RegisterForDbContext(dbContextType, base.IocManager);
                }
            }
        }
    }






    /// <summary>
    /// Implements <see cref="IDbPerTenantConnectionStringResolver"/> to dynamically resolve
    /// connection string for a multi tenant application.
    /// </summary>
    public class CustomerDBConnectionStringResolver : DefaultConnectionStringResolver
    {

        public CustomerDBConnectionStringResolver(
            IAbpStartupConfiguration configuration)
            : base(configuration)
        {

        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            switch (args["DbContextType"].ToString())
            {
                case "ABPCore.EPlusDBConext":
                    return configuration.GetConnectionString(ABPCoreConsts.EECPlusConnectionStringName);
                case "ABPCore.BPDbContext":
                    return configuration.GetConnectionString(ABPCoreConsts.BPConnectionStringName);
                case "ABPCore.ProjectManageDbContext":
                    return configuration.GetConnectionString(ABPCoreConsts.PMConnectionStringName);
            }

            return string.Empty;
        }

        //public virtual string GetNameOrConnectionString(DbPerTenantConnectionStringResolveArgs args)
        //{
        //    if (args.TenantId == null)
        //    {
        //        //Requested for host
        //        return base.GetNameOrConnectionString(args);
        //    }

        //    var tenantCacheItem = _tenantCache.Get(args.TenantId.Value);
        //    if (tenantCacheItem.ConnectionString.IsNullOrEmpty())
        //    {
        //        //Tenant has not dedicated database
        //        return base.GetNameOrConnectionString(args);
        //    }

        //    return tenantCacheItem.ConnectionString;
        //}

        //protected virtual int? GetCurrentTenantId()
        //{
        //    return _currentUnitOfWorkProvider.Current != null
        //        ? _currentUnitOfWorkProvider.Current.GetTenantId()
        //        : AbpSession.TenantId;
        //}
    }
}