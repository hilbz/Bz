using Bz.Configuration.Startup;
using Bz.Dependency;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Modules
{
    /// <summary>
    /// 主要用管理模块
    /// </summary>
    internal class BzModuleManager : IBzModuleManager
    {
        public ILogger Logger { get; set; }

        private readonly BzModuleCollection _modules;

        private readonly IIocManager _iocManager;

        private readonly IModuleFinder _moduleFinder;

        public BzModuleManager(IIocManager iocManager, IModuleFinder moduleFinder)
        {
            _modules = new BzModuleCollection();
            _iocManager = iocManager;
            _moduleFinder = moduleFinder;
            Logger = NullLogger.Instance;
        }

        public virtual void InitializeModules()
        {
            LoadAll();

            var sortedModules = _modules.GetSortedModuleListByDependency();

            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(module => module.Instance.Shutdown());
        }

        /// <summary>
        /// 加载所有模块和设置模块的依赖项
        /// </summary>
        private void LoadAll()
        {
            var moduleTypes = AddMissingDependedModules(_moduleFinder.FindAll());

            //注册IOC 容器
            foreach (var moduleType in moduleTypes)
            {
                if (!BzModule.IsBzModule(moduleType))
                {
                    throw new BzInitializationException("该Type不是Bz模块："+moduleType.AssemblyQualifiedName);
                }

                _iocManager.RegisterIfNot(moduleType);
            }

            //添加到Module Collection
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = (BzModule)_iocManager.Resolve(moduleType);
                moduleObject.IocManager = _iocManager;
                moduleObject.Configuration = _iocManager.Resolve<IBzStartupConfiguration>();

                _modules.Add(new BzModuleInfo(moduleObject));
            }

            EnsureKernelModuleToBeFirst();

            SetDependencies();
            
        }

        private void EnsureKernelModuleToBeFirst()
        {
            var kernelModuleIndex = _modules.FindIndex(m=>m.Type==typeof(BzKernelModule));
            if (kernelModuleIndex>0)
            {
                var kernelModule = _modules[kernelModuleIndex];
                _modules.RemoveAt(kernelModuleIndex);
                _modules.Insert(0, kernelModule);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                //设置一个程序集的依赖项
                foreach (var referencedAssemblyName in moduleInfo.Assembly.GetReferencedAssemblies())
                {
                    var referencedAssembly = Assembly.Load(referencedAssemblyName);
                    var dependedModuleList = _modules.Where(m => m.Assembly == referencedAssembly).ToList();

                    if (dependedModuleList.Count>0)
                    {
                        moduleInfo.Dependencies.AddRange(dependedModuleList);
                    }
                }

                //通过特性[DependsOnAttribute]来设置模块的依赖
                foreach (var dependedModuleType in BzModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m=>m.Type==dependedModuleType);
                    if (dependedModuleInfo==null)
                    {
                        throw new BzInitializationException("未能找到依赖模块："+dependedModuleType.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm=>dm.Type==dependedModuleType))==null)
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }

            }
        }

        private static ICollection<Type> AddMissingDependedModules(ICollection<Type> allModules)
        {
            var initialModules = allModules.ToList();
            foreach (var module in initialModules)
            {
                FillDependedModules(module, allModules);
            }
            return allModules;
        }

        private static void FillDependedModules(Type module, ICollection<Type> allModules)
        {
            foreach (var dependedModule in BzModule.FindDependedModuleTypes(module))
            {
                if (!allModules.Contains(dependedModule))
                {
                    allModules.Add(dependedModule);
                    FillDependedModules(dependedModule, allModules);
                }
            }
        }
    }
}
