using System;
using System.Collections.Generic;
using System.Reflection;

namespace wgabel.Systems
{
    public static class Servicer
    {
        private static readonly IDictionary<Type,Type> types = new Dictionary<Type, Type>();
        private static readonly IDictionary<Type,object> typeInstances = new Dictionary<Type, object>();
        public static void Register<TContract, TImplementation>()
        {
            types[typeof(TContract)] = typeof(TImplementation);
        }
        public static void Register<TContract, TImplementation>(TImplementation instance)
        {
            typeInstances[typeof(TContract)] = instance;
        }
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        public static object Resolve(Type contract)
        {
            if (typeInstances.ContainsKey(contract))
            {
                return typeInstances[contract];
            }
            else
            {
                Type implementation = types[contract];
                ConstructorInfo constructor = implementation.GetConstructors()[0];
                ParameterInfo[] constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length == 0)
                {
                    return Activator.CreateInstance(implementation);
                }
                List<object> parameters = new List<object>(constructorParameters.Length);
                foreach (ParameterInfo parameterInfo in constructorParameters)
                {
                    parameters.Add(Resolve(parameterInfo.ParameterType));
                }
                return constructor.Invoke(parameters.ToArray());
            }
        }
    }
}