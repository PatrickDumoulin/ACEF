using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Mappings
{
    public class EntityDALMappingFactoryWrapper
    {
        #region Encapsulate Record into BOL
        public T NewBol<T>(IRecord record)
            where T : IBOL, new()
        {
            Type type = typeof(T);
            ConstructorInfo ctor = null;
            if (record != null)
            {
                Type recordType = record.GetType();
                ctor = type.GetConstructor(new[] { recordType });
            }
            else
            {
                var ctors = type.GetConstructors();

                foreach (var x in ctors.Where(y => y.GetParameters().Length > 0))
                {
                    ParameterInfo param = x.GetParameters()[0];
                    Type ctorType = param.ParameterType;
                    if (typeof(IRecord).IsAssignableFrom(ctorType))
                    {
                        ctor = x;
                        break;
                    }
                }
            }

            T instance = (T)ctor.Invoke(new object[] { record });
            return instance;
        }
        public List<T> NewBols<T>(List<IRecord> records) where T : IBOL, new()
        {
            Type type = typeof(T);
            List<T> bols = new List<T>();
            ConstructorInfo ctor = null;

            if (records.Any())
            {
                Type recordType = records.First().GetType();
                ctor = type.GetConstructor(new[] { recordType });

                foreach (IRecord record in records)
                {
                    T instance = (T)ctor.Invoke(new object[] { record });
                    bols.Add(instance);
                }
            }

            return bols;
        }
        #endregion
    }
}
