using Saper.Data.Models;
using Saper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Saper.TemplateSelectors
{
    public class FieldTemplateSelector : DataTemplateSelector
    {
        public FieldProxy Proxy { get; set; }
        public DataTemplate Default { get; set; }
        public DataTemplate Empty { get; set; }
        public DataTemplate Number { get; set; }
        public DataTemplate Mine { get; set; }
        public DataTemplate Coverd { get; set; }
        public DataTemplate Ask { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Data.Enums.FieldState fieldState)
            {
                var field = Proxy.Field;
                switch (fieldState)
                {
                    case Data.Enums.FieldState.Defalut:
                        return Default;
                    case Data.Enums.FieldState.Showed:
                        if (field.Value > 0)
                            return Number;
                        else if (field.Value == 0)
                            return Empty;
                        else
                            return Mine;
                    case Data.Enums.FieldState.Coverd:
                        return Coverd;
                    case Data.Enums.FieldState.Ask:
                        return Ask;
                }
            }
            return Default;
        }
    }
}
