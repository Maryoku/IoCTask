using System;
using System.Collections.Generic;

namespace IocExample
{
    class Kernel
    {
        private Dictionary<Type, Type> typeDictionary = new Dictionary<Type, Type>();
        private Dictionary<Type, Object> objectDictionary = new Dictionary<Type, Object>();

        public void BindTypeToType(Type key, Type value)
        {
            typeDictionary.Add(key, value);
        }

        public void BindObjectToType(Type key, Object value)
        {
            objectDictionary.Add(key, value);
        }

        public T GetObject<T>()
        {
            return (T)GetObjectOfType(typeof(T));
        }

        private Object GetObjectOfType(Type objectType)
        {
            if (objectDictionary.ContainsKey(objectType))
            {
                return objectDictionary[objectType];
            }
            else
            {
                objectType = typeDictionary[objectType];
                var constructorInfo = Utils.GetSingleConstructor(objectType);
                List<Object> parameters = new List<Object>();

                foreach (var parameter in constructorInfo.GetParameters())
                {
                    parameters.Add(GetObjectOfType(parameter.ParameterType));
                }

                return Utils.CreateInstance(objectType, parameters);
            }
        }
    }
}
