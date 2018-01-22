using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Dexiom.Ef.Migration
{
    public class TranslationCollection<T> : Collection<T> where T : Translation<T>, new()
    {
        public T this[CultureInfo culture]
        {
            get => this[culture.Name];
            set => this[culture.Name] = value;
        }

        public T this[string culture]
        {
            get
            {
                var translation = this.FirstOrDefault(x => x.CultureName == culture);
                if (translation == null)
                {
                    translation = new T {CultureName = culture};
                    Add(translation);
                }

                return translation;
            }
            set
            {
                var translation = this.FirstOrDefault(x => x.CultureName == culture);
                if (translation != null)
                {
                    Remove(translation);
                }

                value.CultureName = culture;
                Add(value);
            }
        }

        public bool HasCulture(string culture)
        {
            return this.Any(x => x.CultureName == culture);
        }

        public bool HasCulture(CultureInfo culture) => HasCulture(culture.Name);

    }
}
