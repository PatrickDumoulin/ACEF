using Ninject.Modules;
using Ninject.Parameters;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Definitions;

namespace CoreLib.Injection
{
    public static class Injector
    {
        private static IKernel kernel;
        private static object sync_lock = new object();

        public static void AddDefaultBinder(INinjectModule binder)
        {
            if (kernel == null)
                InitializeKernel(new List<INinjectModule>() { binder });
            else
                kernel.Load(binder);
        }
        public static bool IsBindingDefined<T>()
        {
            return BaseBinder.IsBindingDefined<T>();
        }
        public static void InitializeKernel(List<INinjectModule> projectModuleList)
        {
            lock (sync_lock)
            {
                if (kernel == null)
                {
                    kernel = new StandardKernel();

                    List<INinjectModule> moduleList = new List<INinjectModule>();
                    moduleList.AddRange(GetDefaultBinderList());

                    if (projectModuleList != null)
                    {
                        foreach (var module in projectModuleList)
                        {
                            if (!moduleList.Any(f => f.GetType() == module.GetType()))
                                moduleList.Add(module);

                            if (module is IDependentBinder)
                            {
                                var dependantModule = (IDependentBinder)module;

                                if (!moduleList.Any(f => f.GetType() == dependantModule.ReferencedBinder.GetType()))
                                    moduleList.Add(dependantModule.ReferencedBinder);
                            }
                        }
                    }

                    kernel.Load(moduleList);
                }
            }
        }

        public static IKernel Kernel { get { return kernel; } }

        #region ImplementItem
        public static T ImplementItem<T>()
        {
            return kernel.Get<T>();
        }
        public static T ImplementItem<T>(string bindingName)
        {
            return kernel.Get<T>(bindingName);
        }
        #endregion

        #region ImplementBLL
        public static T ImplementBll<T>()
        {
            return ImplementBll<T>(null, ProviderDALTypes.ENTITY.ToString(), ProviderDALTypes.ENTITY);
        }
        public static T ImplementBll<T>(ProviderDALTypes dalType)
        {
            return ImplementBll<T>(null, dalType.ToString(), dalType);
        }
        public static T ImplementBll<T>(IDAL parentDal, ProviderDALTypes dalType)
        {
            return ImplementBll<T>(parentDal, dalType.ToString(), dalType);
        }
        #endregion

        #region Implement DAL
        public static T ImplementDal<T>(ProviderDALTypes dalType) where T : IDAL
        {
            return ImplementDal<T>(dalType, null);
        }
        public static T ImplementDal<T>(ProviderDALTypes dalType, IDAL externalDal) where T : IDAL
        {
            string bindingName = resolveBindingName(dalType);
            return ImplementDal<T>(bindingName, dalType, externalDal);
        }
        #endregion

        #region Private methods
        private static T ImplementBll<T>(IDAL parentDal, string bindingName, ProviderDALTypes dalType)
        {
            List<ConstructorArgument> internalList = new List<ConstructorArgument>();

            if (parentDal != null)
                internalList.Add(new ConstructorArgument("dal", parentDal, true));

            internalList.Add(new ConstructorArgument("dalType", dalType, true));

            return kernel.Get<T>(bindingName, internalList.ToArray());
        }
        private static T ImplementDal<T>(string bindingName, ProviderDALTypes dalType, IDAL externalDal) where T : IDAL
        {
            if (externalDal == null)
            {
                return string.IsNullOrEmpty(bindingName) ? kernel.Get<T>() : kernel.Get<T>(bindingName);
            }
            else
            {
                List<ConstructorArgument> internalList = new List<ConstructorArgument>();
                internalList.Add(new ConstructorArgument("externalDal", externalDal, true));
                return string.IsNullOrEmpty(bindingName) ? kernel.Get<T>(internalList.ToArray()) : kernel.Get<T>(bindingName, internalList.ToArray());
            }
        }

        private static string resolveBindingName(ProviderDALTypes dalProvider)
        {
            return dalProvider.ToString();
        }
        private static List<INinjectModule> GetDefaultBinderList()
        {
            List<INinjectModule> defaultBinderList = new List<INinjectModule>();
            return defaultBinderList;
        }
        #endregion

    }
}
