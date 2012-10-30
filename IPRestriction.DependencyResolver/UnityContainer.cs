using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace IPRestriction.DependencyResolver
{
    public class UnityContainerAdapter : IContainer
    {
        /// <summary>
        /// Instance of unity container to which all the calls get delegated.
        /// </summary>
        private readonly IUnityContainer _unityContainer;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityContainerAdapter"/> class.
        /// </summary>
        public UnityContainerAdapter() : this(new UnityContainer()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityContainerAdapter"/> class.
        /// </summary>
        /// <param name="unityContainer">
        /// The unity container.
        /// </param>
        public UnityContainerAdapter(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
            _unityContainer.RegisterInstance<IContainer>(this);
        }


        public IContainer Register<I, T>() where T : I
        {
            _unityContainer.RegisterType<I, T>();
            return this;
        }

        public IContainer Register<I, T>(object[] injectionConstructorParams) where T : I
        {
            _unityContainer.RegisterType<I, T>(new InjectionConstructor(injectionConstructorParams));
            return this;
        }
        
        public IContainer Register<I, T>(string key) where T : I
        {
            _unityContainer.RegisterType<I, T>(key);
            return this;
        }

        public IContainer RegisterSingleton<I, T>() where T : I
        {
            _unityContainer.RegisterType<I, T>(new ContainerControlledLifetimeManager());
            return this;
        }

        public T Retrieve<T>()
        {
            return _unityContainer.Resolve<T>();
        }
       
        public T Retrieve<T>(string key)
        {
            return _unityContainer.Resolve<T>(key);
        }

        public IEnumerable<T> RetrieveAll<T>()
        {
            return _unityContainer.ResolveAll<T>();
        }
    }
}