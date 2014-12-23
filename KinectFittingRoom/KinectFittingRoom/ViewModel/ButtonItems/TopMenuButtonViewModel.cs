using KinectFittingRoom.ViewModel.ClothingItems;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectFittingRoom.ViewModel.ButtonItems
{
    public class TopMenuButtonViewModel : ButtonViewModelBase
    {
        #region Consts
        private const double _plusFactor = 1.05;
        private const double _minusFactor = 0.95;
        #endregion
        #region Private Fields
        /// <summary>
        /// Type of button
        /// </summary>
        private Functionality _buttonType;
        #endregion
        #region Public Properties
        /// <summary>
        /// Gets or sets button object
        /// </summary>
        public TopMenuButtonViewModel TopButtonObject
        {
            get { return this; }
        }
        #endregion
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="TopMenuButtonViewModel"/> class.
        /// </summary>
        /// <param name="buttonType">Type of button</param>
        public TopMenuButtonViewModel(Functionality buttonType)
        {
            _buttonType = buttonType;
        }
        #endregion
        #region Commands
        /// <summary>
        /// The top menu command, executed after clicking on button from top menu
        /// </summary>
        private ICommand _topMenuCommand;
        /// <summary>
        /// Gets the top menu command.
        /// </summary>
        /// <value>
        /// The top menu command.
        /// </value>
        public ICommand TopMenuCommand
        {
            get { return _topMenuCommand ?? (_topMenuCommand = new DelegateCommand<object>(FunctionalityExecuted)); }
        }
        /// <summary>
        /// Executes when button from top menu was hit.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public void FunctionalityExecuted(object parameter)
        {
            var values = (object[])parameter;
            var clickedButton = (TopMenuButtonViewModel)values[0];
            var width = (double)values[1];
            var height = (double)values[2];
            var imageArea = (System.Windows.Controls.Image)values[3];
            var clothesArea = (System.Windows.Controls.ItemsControl)values[4];


            switch (clickedButton._buttonType)
            {
                case Functionality.showMenu:
                    if (TopMenuManager.Instance.TopMenuButtons.Count > 1)
                        ReturnToMainMenu();
                    else
                        TopMenuManager.Instance.TopMenuButtons = new ObservableCollection<TopMenuButtonViewModel>(TopMenuManager.Instance.AllButtons);
                    break;
                case Functionality.maleFemaleCategory:
                    TopMenuManager.Instance.TopMenuButtons = new ObservableCollection<TopMenuButtonViewModel>(TopMenuManager.Instance.MaleFemaleCategory);
                    break;
                case Functionality.maleCategory:
                    #warning Categories not implemented!
                    ReturnToMainMenu();
                    break;
                case Functionality.femaleCategory:
                    #warning Categories not implemented!
                    ReturnToMainMenu();
                    break;
                case Functionality.changeSize:
                    TopMenuManager.Instance.TopMenuButtons = new ObservableCollection<TopMenuButtonViewModel>(TopMenuManager.Instance.ChangeSizeButtons);
                    break;
                case Functionality.makeBigger:
                    if (ClothingManager.Instance.ChosenClothes.Count != 0)
                        ScaleImage(_plusFactor);
                    break;
                case Functionality.makeSmaller:
                    if (ClothingManager.Instance.ChosenClothes.Count != 0)
                        ScaleImage(_minusFactor);
                    break;
                case Functionality.clearClothingSet:
                    ClothingManager.Instance.ChosenClothes.Clear();
                    ClothingManager.Instance.ChosenClothes = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>();
                    ReturnToMainMenu();
                    break;
                case Functionality.takePicture:
                    MakeScreenShot(imageArea, clothesArea, (int)width, (int)height);
                    ReturnToMainMenu();
                    break;
                case Functionality.turnOnOffSounds:
                    if (View.Buttons.KinectButton.SoundsOn)
                        View.Buttons.KinectButton.SoundsOn = false;
                    else
                        View.Buttons.KinectButton.SoundsOn = true;
                    ReturnToMainMenu();
                    break;
                case Functionality.exit:
                    Application.Current.MainWindow.Close();
                    break;

            }
        }
        #endregion Commands
        #region Methods
        /// <summary>
        /// Scales images of clothes
        /// </summary>
        /// <param name="ratio">The ratio of scaling</param>
        public void ScaleImage(double ratio)
        {
            ClothingItemBase lastItem = ClothingManager.Instance.LastAddedItem;
            lastItem.Image = new Bitmap(lastItem.Image, (int)lastItem.Image.Width, (int)(ratio * lastItem.Image.Height));

            Dictionary<ClothingItemBase.ClothingType, ClothingItemBase> tmp = ClothingManager.Instance.ChosenClothes;
            tmp[tmp.FirstOrDefault(a => a.Value.PathToImage == lastItem.PathToImage).Key] = lastItem;
            ClothingManager.Instance.ChosenClothes = new Dictionary<ClothingItemBase.ClothingType, ClothingItemBase>(tmp);
        }
        /// <summary>
        /// Makes screen capture
        /// </summary>
        /// <param name="visual">ImageArea control from UI</param>
        /// <param name="visual2">ClothesArea control from UI</param>
        /// <param name="actualWidth">Actual screen width</param>
        /// <param name="actualHeight">Actual screen height</param>
        private void MakeScreenShot(Visual visual, Visual visual2, int actualWidth, int actualHeight)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\photo.png";
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(actualWidth, actualHeight, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(visual);
            renderTargetBitmap.Render(visual2);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            using (Stream fileStream = File.Create(filePath))
            {
                pngImage.Save(fileStream);
            }
            if (View.Buttons.KinectButton.SoundsOn)
            {
                SoundPlayer player = new SoundPlayer(Properties.Resources.CameraClick);
                player.Play();
            }
        }
        /// <summary>
        /// Clean all top butons and displays main button
        /// </summary>
        private void ReturnToMainMenu()
        {
            TopMenuManager.Instance.TopMenuButtons.Clear();
            TopMenuManager.Instance.TopMenuButtons.Add(new TopMenuButtonViewModel(TopMenuButtonViewModel.Functionality.showMenu) { Image = Properties.Resources.menu });
        }
        #endregion
        /// <summary>
        /// Enumeration of top buttons functionalities
        /// </summary>
        public enum Functionality
        {
            changeSize,
            clearClothingSet,
            femaleCategory,
            makeBigger,
            makeSmaller,
            maleCategory,
            maleFemaleCategory,
            showMenu,
            takePicture,
            turnOnOffSounds,
            exit
        }
    }
}
