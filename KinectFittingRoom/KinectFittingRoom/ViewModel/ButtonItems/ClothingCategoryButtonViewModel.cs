using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using KinectFittingRoom.ViewModel.ClothingItems;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class ClothingCategoryButtonViewModel : ButtonViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// List of clothes in current category
        /// </summary>
        private List<ClothingButtonViewModel> _clothes;
        #endregion Private Fields
        #region Public Properties
        /// <summary>
        /// Gets or sets the clothes list.
        /// </summary>
        /// <value>
        /// The clothes list.
        /// </value>
        public List<ClothingButtonViewModel> Clothes
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
        private ICommand _categoryCommand;
        /// <summary>
        /// Gets the category command.
        /// </summary>
        /// <value>
        /// The category command.
        /// </value>
        public ICommand CategoryCommand
        {
            get { return _categoryCommand ?? (_categoryCommand = new DelegateCommand(CategoryExecuted)); }
        }
        /// <summary>
        /// Executes when the Category button was hit.
        /// </summary>
        public void CategoryExecuted()
        {
            if (ClothingManager.Instance.Clothing != null && ClothingManager.Instance.Clothing[0].Image == Clothes[0].Image)
                return;
            ClothingManager.Instance.Clothing = new ObservableCollection<ClothingButtonViewModel>(Clothes);
        }
        #endregion Commands
    }
}
