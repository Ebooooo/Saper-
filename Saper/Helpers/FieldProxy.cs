using Saper.Data.Models;
using System.Windows;

namespace Saper.Helpers
{
    public class FieldProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new FieldProxy();
        }
        public Field Field
        {
            get { return (Field)GetValue(FieldProperty); }
            set { SetValue(FieldProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register("Field", typeof(Field), typeof(FieldProxy), new PropertyMetadata(null));
    }
}
