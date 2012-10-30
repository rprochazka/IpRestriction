using System.Configuration;

namespace IPRestriction.Handler.Configuration
{
    public class ProviderConfigurationElementCollection : ConfigurationElementCollection
    {
        public ProviderConfigurationElement this[int index]
        {
            get
            {
                return BaseGet(index) as ProviderConfigurationElement;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderConfigurationElement)element).Name;
        }
    }
}