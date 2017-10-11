using System;
using Abp.Dependency;
using Abp.Domain.Entities;
using Castle.Core.Logging;

namespace ABPCore.EntityFrameworkCore
{
    public class ExtendedRepositoryRegistrar : ITransientDependency
    {
        public ILogger Logger
        {
            get;
            set;
        }

        public ExtendedRepositoryRegistrar()
        {
            this.Logger = NullLogger.Instance;
        }

        public void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {
            var RepositoryInterface = typeof(IExtendedRepository<>);
            var RepositoryImplementation = typeof(EfRepositoryBase<,>);


            foreach (EntityTypeInfo entityTypeInfo in ExtendedDbContextHelper.GetDbEntityTypeInfos(dbContextType))
            {
                Type genericRepositoryType = RepositoryInterface.MakeGenericType(new Type[] { entityTypeInfo.EntityType });
                if (!iocManager.IsRegistered(genericRepositoryType))
                {
                    Type implType = (RepositoryImplementation.GetGenericArguments().Length == 1) ? RepositoryImplementation.MakeGenericType(new Type[]
                    {
                    entityTypeInfo.EntityType
                    }) : RepositoryImplementation.MakeGenericType(new Type[]
                    {
                    entityTypeInfo.DeclaringType,
                    entityTypeInfo.EntityType
                    });
                    iocManager.Register(genericRepositoryType, implType, DependencyLifeStyle.Transient);
                }
            }
        }
    }
}

