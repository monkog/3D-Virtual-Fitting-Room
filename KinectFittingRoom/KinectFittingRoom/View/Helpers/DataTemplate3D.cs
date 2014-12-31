using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace KinectFittingRoom.View.Helpers
{
    [ContentProperty("Content")]
    public class DataTemplate3D : DispatcherObject
    {
        public Visual3D Content { get; set; }

        public Visual3D CreateItem(object source)
        {
            var type = Content.GetType();
            var types = new List<Type> {type};
            var current = type;
            while (current.BaseType != null)
            {
                types.Add(current.BaseType);
                current = current.BaseType;
            }

            var visual = (Visual3D)Activator.CreateInstance(type);
            var boundProperties = new HashSet<string>();
            foreach (var t in types)
            {
                foreach (var fi in t.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    var dp = fi.GetValue(null) as DependencyProperty;
                    if (dp == null) continue;

                    var binding = BindingOperations.GetBinding(Content, dp);
                    if (binding == null) continue;

                    boundProperties.Add(dp.Name);
                    BindingOperations.SetBinding(
                        visual, dp, new Binding { Path = binding.Path, Source = source });
                }
            }
            return visual;
        }
    }
}
