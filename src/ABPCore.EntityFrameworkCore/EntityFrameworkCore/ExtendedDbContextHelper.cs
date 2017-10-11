using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABPCore.EntityFrameworkCore
{
    public class ExtendedDbContextHelper
    {
        public static bool DoesDBNeedRegisterGenericRepositories(Type type)
        {
            return type.IsPublic && !type.IsAbstract && type.IsClass && typeof(ExtendedDbContextBase).IsAssignableFrom(type);
        }

        public static IEnumerable<EntityTypeInfo> GetDbEntityTypeInfos(Type dbContextType)
        {
            IEnumerable<PropertyInfo> properties = dbContextType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(IsDbEntity);
            return properties.Select(p => new EntityTypeInfo(p.PropertyType.GenericTypeArguments[0], p.DeclaringType));
        }

        public static bool IsDbEntity(PropertyInfo property)
        {
            return (IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) || IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))) && typeof(IDbEntity).IsAssignableFrom(property.PropertyType.GenericTypeArguments[0]);
        }

        /// <summary>
        /// Checks whether <paramref name="givenType" /> implements/inherits <paramref name="genericType" />.
        /// </summary>
        /// <param name="givenType">Type to check</param>
        /// <param name="genericType">Generic type</param>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
            Type[] interfaces = givenType.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                Type interfaceType = interfaces[i];
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }
            return !(givenType.BaseType == null) && IsAssignableToGenericType(givenType.BaseType, genericType);
        }
    }
}
