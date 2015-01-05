using KinectFittingRoom.View.Buttons.Events;
using KinectFittingRoom.ViewModel;
using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace KinectFittingRoom.View.Buttons
{
    /// <summary>
    /// Screen shot button class that responds to Kincect events
    /// </summary>
    class KinectScreenShotButtun : KinectButton
    {
        #region Constants
        /// <summary>
        /// The timespan
        /// </summary>
        private const int Timespan = 3;
        #endregion Constants
        #region Private Fields
        /// <summary>
        /// The screenshot timer
        /// </summary>
        private readonly DispatcherTimer _screenshotTimer;
        /// <summary>
        /// The number of _screenshotTimer ticks
        /// </summary>
        private int _ticks;
        #endregion Private Fields
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="KinectScreenShotButtun"/> class.
        /// </summary>
        public KinectScreenShotButtun():base()
        {
            _screenshotTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };
            _screenshotTimer.Tick += ScreenshotTimer_Tick;
        }
        #endregion .ctor
        #region Methods
        /// <summary>
        /// Handles the Tick event of the _screenshotTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ScreenshotTimer_Tick(object sender, EventArgs e)
        {
            _ticks++;
            if (_ticks < Timespan)
                (Application.Current.MainWindow as MainWindow).ScreenshotText.Text = (Timespan - _ticks) + "...";
            else
                (Application.Current.MainWindow as MainWindow).ScreenshotText.Text = "uśmiech :)";

            if (_ticks > Timespan)
            {
                _screenshotTimer.Stop();
                _ticks = 0;
                (Application.Current.MainWindow as MainWindow).ScreenshotGrid.Visibility = Visibility.Collapsed;
                MakeScreenshot();
            }
        }
        /// <summary>
        /// Imitates the click event for KinectScreenShotButtun
        /// </summary>
        protected override void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            SetValue(IsClickedProperty, true);

            (Application.Current.MainWindow as MainWindow).ScreenshotGrid.Visibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).ScreenshotText.Text = "3...";
            _screenshotTimer.Start();

            AfterClickTimer.Start();
        }
        /// <summary>
        /// Makes the screenshot.
        /// </summary>
        private void MakeScreenshot()
        {
            int actualWidth = (int)(Application.Current.MainWindow as MainWindow).ImageArea.ActualWidth;
            int actualHeight = (int)(Application.Current.MainWindow as MainWindow).ImageArea.ActualHeight;
            string fileName = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss", CultureInfo.InvariantCulture);
            fileName += ".png";
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Wirtualna Przymierzalnia");
            Directory.CreateDirectory(directoryPath);
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(actualWidth, actualHeight, 96, 96,
                PixelFormats.Pbgra32);
            renderTargetBitmap.Render((Application.Current.MainWindow as MainWindow).ImageArea);
            renderTargetBitmap.Render((Application.Current.MainWindow as MainWindow).ClothesArea);
            renderTargetBitmap.Render(CreateWatermarkLayer(actualWidth, actualHeight));
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using (Stream fileStream = File.Create(directoryPath + "\\" + fileName))
            {
                CropTransparentPixels(pngImage).Save(fileStream);
            }
            if (KinectViewModel.SoundsOn)
                KinectViewModel.CameraPlayer.Play();
        }
        /// <summary>
        /// Create watermark on screenshot
        /// </summary>
        /// <param name="width">Width of ImageArea</param>
        /// <param name="height">Height of ImageArea</param>
        /// <returns>Visual with watermark</returns>
        private Visual CreateWatermarkLayer(int width, int height)
        {
            int margin = 10;
            DrawingVisual visualWatermark = new DrawingVisual();
            BitmapImage image = new BitmapImage(new Uri(@"pack://application:,,,/Resources/watermark.png"));
            Point imageLocation = new Point(width - (image.Width + margin), height - (image.Height + margin));

            using (var drawingContext = visualWatermark.RenderOpen())
            {
                drawingContext.DrawRectangle(null, null, new Rect(0, 0, width, height));
                drawingContext.DrawImage(image, new Rect(imageLocation.X, imageLocation.Y, image.Width, image.Height));
            }
            visualWatermark.Opacity = 0.4;
            return visualWatermark;
        }
        /// <summary>
        /// Crops the transparent pixels of image from ImageArea
        /// </summary>
        /// <param name="pngImage">Image from ImageArea</param>
        /// <returns>Cropped image</returns>
        private PngBitmapEncoder CropTransparentPixels(PngBitmapEncoder pngImage)
        {
            int pixelsCount = 0;
            BitmapSource bitmapSource = pngImage.Frames[0];
            var pixel = new byte[4];
            for (int i = 0; i < bitmapSource.Width; i++)
            {
                bitmapSource.CopyPixels(new Int32Rect(i, 0, 1, 1), pixel, 4, 0);
                if (pixel[0] == 0)
                    pixelsCount++;
            }

            PngBitmapEncoder croppedImage = new PngBitmapEncoder();
            croppedImage.Frames.Add(BitmapFrame.Create(new CroppedBitmap(bitmapSource, new Int32Rect(pixelsCount + 1, 0, (int)bitmapSource.Width - pixelsCount - 1, (int)bitmapSource.Height))));
            return croppedImage;
        }
        #endregion Methods
    }
}
