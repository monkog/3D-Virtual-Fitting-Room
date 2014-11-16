using System.Collections.Generic;
using System.Drawing;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class ClothingCategory : ViewModelBase
    {
        #region Private Fields
        /// <summary>
        /// List of clothes in current category
        /// </summary>
        private List<ClothingCategory> _clothes;
        /// <summary>
        /// The button's image
        /// </summary>
        private Bitmap _image;
        #endregion Private Fields
        #region Public Properties        
        /// <summary>
        /// Gets or sets the clothes list.
        /// </summary>
        /// <value>
        /// The clothes list.
        /// </value>
        public List<ClothingCategory> Clothes
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
        /// <summary>
        /// Gets or sets the button's image.
        /// </summary>
        /// <value>
        /// The button's image.
        /// </value>
        public Bitmap Image
        {
            get { return _image; }
            set
            {
                if (_image == value)
                    return;
                _image = value;
                OnPropertyChanged("Image");
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
            get { return _categoryCommand ?? (_categoryCommand = new DelegateCommand<object>(CategoryExecuted)); }
        }

        public void CategoryExecuted(object parameter)
        {
            if (true)
                ;
        }

        #endregion Commands
    }
}
