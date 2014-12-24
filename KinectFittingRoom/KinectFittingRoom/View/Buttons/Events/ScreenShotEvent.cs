using KinectFittingRoom.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectFittingRoom.View.Buttons.Events
{
    class ScreenShotEvent
    {
        public ScreenShotEvent(Visual visual1, Visual visual2, int actualWidth, int actualHeight)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\photo.png";
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(actualWidth, actualHeight, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(visual1);
            renderTargetBitmap.Render(visual2);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            using (Stream fileStream = File.Create(filePath))
            {
                pngImage.Save(fileStream);
            }
            if (KinectViewModel.SoundsOn)
                KinectViewModel.Player2.Play();
        }
    }
}
