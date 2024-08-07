using CoreLib.Definitions;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Injection
{
    public abstract class BaseBinder : NinjectModule
    {
        private static Dictionary<Type, List<string>> bindedTypes = new Dictionary<Type, List<string>>();

        #region Bind standard Item (not bll nor dal)
        public void BindItem<T, U>()
            where U : T
        {
            BindItem<T, U>(string.Empty);
        }
        public void BindItem<T, U>(string bindingName) where U : T
        {
            bool toBind = false;

            if (bindedTypes == null)
                bindedTypes = new Dictionary<Type, List<string>>();

            if (!bindedTypes.ContainsKey(typeof(T)))
            {
                bindedTypes.Add(typeof(T), new List<string>() { bindingName });
                toBind = true;
            }
            else
            {
                var list = bindedTypes[typeof(T)];
                if (!list.Contains(bindingName))
                {
                    list.Add(bindingName);
                    toBind = true;
                }
            }

            if (toBind)
            {
                if (string.IsNullOrEmpty(bindingName))
                    Bind<T>().To<U>();
                else
                    Bind<T>().To<U>().Named(bindingName);
            }
        }
        #endregion

        #region Bind Bll
        public void BindBll<T, U>() where U : T, IBLL
        {
            BindBll<T, U>(ProviderDALTypes.ENTITY);
        }
        public void BindBll<T, U>(ProviderDALTypes dalProvider) where U : T, IBLL
        {
            string bindingName = this.resolveBindingName(dalProvider);
            internalBindBll<T, U>(bindingName);
        }

        public static bool IsBindingDefined<T>()
        {
            Type bindingType = typeof(T);
            return bindedTypes.ContainsKey(bindingType);
        }
        #endregion

        #region Bind Dal
        public void BindDal<T, U>() where U : T, IDAL
        {
            string bindingName = this.resolveBindingName(ProviderDALTypes.ENTITY);
            internalBindDal<T, U>(bindingName);
        }
        public void BindDal<T, U>(ProviderDALTypes dalProvider) where U : T, IDAL
        {
            string bindingName = this.resolveBindingName(dalProvider);
            internalBindDal<T, U>(bindingName);
        }

        #endregion

        #region Override
        public override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion

        #region Private methods
        private void internalBindBll<T, U>(string bindingName) where U : T, IBLL
        {
            bool toBind = false;

            if (bindedTypes == null)
                bindedTypes = new Dictionary<Type, List<string>>();

            if (!bindedTypes.ContainsKey(typeof(T)))
            {
                bindedTypes.Add(typeof(T), new List<string>() { bindingName });
                toBind = true;
            }
            else
            {
                var list = bindedTypes[typeof(T)];
                if (!list.Contains(bindingName))
                {
                    list.Add(bindingName);
                    toBind = true;
                }
            }

            if (toBind)
            {
                if (string.IsNullOrEmpty(bindingName))
                    Bind<T>().To<U>();
                else
                    Bind<T>().To<U>().Named(bindingName);
            }
        }
        private void internalBindDal<T, U>(string bindingName) where U : T, IDAL
        {
            bool toBind = false;

            if (bindedTypes == null)
                bindedTypes = new Dictionary<Type, List<string>>();

            if (!bindedTypes.ContainsKey(typeof(T)))
            {
                bindedTypes.Add(typeof(T), new List<string>() { bindingName });
                toBind = true;
            }
            else
            {
                var list = bindedTypes[typeof(T)];
                if (!list.Contains(bindingName))
                {
                    list.Add(bindingName);
                    toBind = true;
                }
            }

            if (toBind)
            {


                if (string.IsNullOrEmpty(bindingName))
                    Bind<T>().To<U>();
                else
                    Bind<T>().To<U>().Named(bindingName);
            }
        }
        private string resolveBindingName(ProviderDALTypes dalProvider)
        {
            return dalProvider.ToString();
        }
        #endregion
    }
}
