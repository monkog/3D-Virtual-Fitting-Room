using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using KinectFittingRoom.View.Buttons.Events;
using KinectFittingRoom.ViewModel;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using KinectFittingRoom.ViewModel.ClothingItems;

namespace KinectFittingRoom
{
    /// <summary>
    /// Kinect Fitting Room main window
    /// </summary>
    public partial class MainWindow
    {
        #region .ctor
        public MainWindow()
        {
            InitializeComponent();
            SubscribeForHandPositionChanges();
        }
        #endregion .ctor
        #region Private Methods
        /// <summary>
        /// Subscribes for hand position changes.
        /// </summary>
        private void SubscribeForHandPositionChanges()
        {
            var dataContext = DataContext as KinectViewModel;
            Debug.Assert(dataContext != null, "DataContext != null");
            dataContext.KinectService.Hand.PropertyChanged += KinectService_PropertyChanged;
            ClothingManager.Instance.PropertyChanged += ClothingManager_PropertyChanged;
        }
        /// <summary>
        /// Handles the PropertyChanged event of the ClothingManager.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ClothingManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ChosenClothesModels":
                    if (((ClothingManager)sender).ChosenClothesModels.Count == 0)
                        ClothesArea.Children.Clear();
                    break;
            }
        }
        /// <summary>
        /// Handles the PropertyChanged event of the Hand.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void KinectService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "RightPosition":
                case "LeftPosition":
                    var dataContext = DataContext as KinectViewModel;
                    Debug.Assert(dataContext != null, "DataContext != null");
                    HandleHandMoved(dataContext.KinectService.Hand.LeftPosition, dataContext.KinectService.Hand.RightPosition);
                    break;
            }
        }
        /// <summary>
        /// Handles the hand moved event.
        /// </summary>
        /// <param name="leftHand">The left hand position.</param>
        /// <param name="rightHand">The right hand position.</param>
        private void HandleHandMoved(Point leftHand, Point rightHand)
        {
            HandCursor.Visibility = Visibility.Collapsed;

            var element = ButtonPanelsCanvas.InputHitTest(leftHand);
            var hand = leftHand;
            if (!(element is UIElement))
            {
                element = ButtonPanelsCanvas.InputHitTest(rightHand);
                hand = rightHand;
                if (!(element is UIElement))
                    return;
            }

#warning TODO
            // TODO: Change that to MVVM
            HandCursor.Visibility = Visibility.Visible;
            Canvas.SetLeft(HandCursor, hand.X - HandCursor.ActualWidth / 2.0);
            Canvas.SetTop(HandCursor, hand.Y - HandCursor.ActualHeight / 2.0);
            ButtonsManager.Instance.RaiseCursorEvents(element, hand);
        }
        #endregion Private Methods
        /// <summary>
        /// Makes a screen shot
        /// </summary>
        private void ScreenShotEvent(object sender, HandCursorEventArgs args)
        {
            int actualWidth = (int)ImageArea.ActualWidth;
            int actualHeight = (int)ImageArea.ActualHeight;
            string fileName = DateTime.Now.ToString("yyyy.MM.dd-HH.mm", CultureInfo.InvariantCulture);
            fileName += ".png";
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Wirtualna Przymierzalnia");
            Directory.CreateDirectory(directoryPath);
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(actualWidth, actualHeight, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(ImageArea);
            renderTargetBitmap.Render(ClothesArea);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            using (Stream fileStream = File.Create(directoryPath + "\\" + fileName))
            {
                pngImage.Save(fileStream);
            }
            if (KinectViewModel.SoundsOn)
                KinectViewModel.CameraPlayer.Play();
        }
    }
}
