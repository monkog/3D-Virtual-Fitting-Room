using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;

namespace KinectFittingRoom.View.Helpers
{
    public class ItemsVisual3D : ModelVisual3D
    {
        #region Dependency Properties
        /// <summary>
        /// Item template property
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate", typeof(DataTemplate3D), typeof(ItemsVisual3D), new PropertyMetadata(null));
        /// <summary>
        /// The items source property
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable), typeof(ItemsVisual3D)
            , new PropertyMetadata(null, (s, e) => ((ItemsVisual3D)s).ItemsSourceChanged(e)));
        #endregion Dependency Properties
        #region Public Properties
        /// <summary>
        /// Gets or sets the <see cref="DataTemplate3D" /> used to display each item.
        /// </summary>
        /// <value>
        /// The item template.
        /// </value>
        public DataTemplate3D ItemTemplate
        {
            get { return (DataTemplate3D)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        /// <summary>
        /// Gets or sets a collection used to generate the content of the <see cref="ItemsVisual3D" />.
        /// </summary>
        /// <value>
        /// The items source.
        /// </value>
        public ICollection ItemsSource
        {
            get { return (ICollection)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        #endregion Public Properties
        #region Private Methods
        /// <summary>
        /// Handles changes in the ItemsSource property.
        /// </summary>
        /// <param name="e">
        /// The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void ItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            Children.Clear();

            foreach (var model in (from object item in ItemsSource
                                   select ItemTemplate.CreateItem(item)).Where(model => model != null))
                Children.Add(model);
        }
        #endregion Private Methods
    }
}
