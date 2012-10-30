using System;

namespace IPRestriction.DependencyResolver
{
    public static class ServiceLocator
    {
        /// <summary>
        /// An abstraction of IoC ioC. 
        /// </summary>
        private static IContainer _ioc;

        /// <summary>
        /// Gets or sets the IoC container.
        /// </summary>
        /// <value>
        /// The IoC container abstraction.
        /// </value>
        public static IContainer IoC
        {
            get
            {
                if (_ioc == null)
                {
                    throw new InvalidOperationException(
                        "In order to use IoC container make sure that it is initialized first in application bootstrapper.");
                }

                return _ioc;
            }

            set { _ioc = value; }
        }

    }
}
