using System.Collections.Generic;
using System.Windows.Input;
using KinectFittingRoom.ViewModel.ClothingItems;
using Microsoft.Practices.Prism.Commands;
using System.Drawing;
using System.Collections.ObjectModel;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    /// <summary>
    /// View model for the Clothing buttons
    /// </summary>
    public class ClothingButtonViewModel : ButtonViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// List of clothes in current category
        /// </summary>
        private List<ClothingItemBase> _clothes;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the clothes list.
        /// </summary>
        /// <value>
        /// The clothes list.
        /// </value>
        public List<ClothingItemBase> Clothes
        {
            get { return _clothes; }
            set
            {
                if (_clothes == value)
                    return;
                _clothes = value;
                OnPropertyChanged("Clothes");
            }
        }
        #endregion Public Properties
        #region Commands
        /// <summary>
        /// The category command, executed after clicking on Category button
        /// </summary>
        private ICommand _clothCommand;
        /// <summary>
        /// Gets the category command.
        /// </summary>
        /// <value>
        /// The category command.
        /// </value>
        public ICommand ClothCommand
        {
            get { return _clothCommand ?? (_clothCommand = new DelegateCommand<object>(CategoryExecuted)); }
        }
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void CategoryExecuted(object parameter)
        {
            // TODO: Resolve the Clothing Manager via the IOC container?
            // TODO: Change the clothing collection in Clothing Manager to the one corresponding to the chosen button
            // TODO: Preload the collection at startup or load dynamically in another thread?

            //MOJE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            List<ClothingItemBase> tmp = new List<ClothingItemBase>(ClothingManager.Instance.ChosenClothes);
            tmp.Add(Clothes.Find(a => a.Height == (parameter as Bitmap).Height && a.Width == (parameter as Bitmap).Width));
            ClothingManager.Instance.ChosenClothes = new ObservableCollection<ClothingItemBase>(tmp);
            tmp.Clear(); 
        }
        #endregion Commands
    }
}
