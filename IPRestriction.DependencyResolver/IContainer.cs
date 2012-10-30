using System.Collections.Generic;

namespace IPRestriction.DependencyResolver
{
    public interface IContainer
    {
        IContainer Register<I, T>() where T : I;
        IContainer Register<I, T>(object[] injectionConstructorParams) where T : I;
        IContainer Register<I, T>(string key) where T : I;
        IContainer RegisterSingleton<I, T>() where T : I;
        T Retrieve<T>();
        T Retrieve<T>(string key);
        IEnumerable<T> RetrieveAll<T>();
    }
}