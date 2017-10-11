using System;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection;
using Abp.Reflection.Extensions;
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
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            Configuration.Modules.AbpEfCore().AddDbContext<BPDbContext>(options =>
            {

                options.DbContextOptions.UseSqlServer(
                    configuration.GetConnectionString(ABPCoreConsts.BPConnectionStringName));

            });


            Configuration.Modules.AbpEfCore().AddDbContext<EPlusDBConext>(options =>
            {
                options.DbContextOptions.UseSqlServer(
                    configuration.GetConnectionString(ABPCoreConsts.EECPlusConnectionStringName));
            });


            Configuration.Modules.AbpEfCore().AddDbContext<ProjectManageDbContext>(options =>
            {
                options.DbContextOptions.UseSqlServer(
                    configuration.GetConnectionString(ABPCoreConsts.PMConnectionStringName));
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
}