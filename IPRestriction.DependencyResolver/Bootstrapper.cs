namespace IPRestriction.DependencyResolver
{
    public static class Bootstrapper
    {
        public static void Init()
        {
            ServiceLocator.IoC = new UnityContainerAdapter();
        }

        /// <summary>
        /// Creates an empty IoC container
        /// </summary>
        /// <returns></returns>
        public static IContainer CreateIoCContainer()
        {
            var container = new UnityContainerAdapter();
            ServiceLocator.IoC = container;

            return container;
        }

    }
}
