using Bz.Reflection;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity
{
    /// <summary>
    /// DbModelBuilder的扩展方法
    /// </summary>
    public static class DbModelBuilderExtensions
    {
        /// <summary>
        /// 通过命名空间来配置<see cref="DbModelBuilder"/>使用的Mapper映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="namespaceStr"></param>
        public static void ConfigurationMapperWithNamespace(this DbModelBuilder modelBuilder, string namespaceStr)
        {
            var mapperTypes =
             Assembly.GetExecutingAssembly()
                 .GetTypes().Where(
                     t => !string.IsNullOrEmpty(t.Namespace)
                     && t.Namespace.Contains(namespaceStr)
                     && t.BaseType != null
                     && t.BaseType.IsGenericType
                     && ReflectionHelper.IsAssignableToGenericType(t.BaseType.GetGenericTypeDefinition(), typeof(EntityTypeConfiguration<>))
                         ).ToList();
            foreach (var type in mapperTypes)
            {
                dynamic instance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(instance);
            }
        }
    }
}
