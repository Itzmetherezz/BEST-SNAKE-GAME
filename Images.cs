using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SNAKE_GAME
{
    public static class Images
    {
        public readonly static ImageSource Empty = LoadImage ("Empty");
        public readonly static ImageSource DeadBody = LoadImage("Empty");
        public readonly static ImageSource Food = LoadImage("Food");
        public readonly static ImageSource Body = LoadImage("Body");
        public readonly static ImageSource Head = LoadImage("Head");
        public readonly static ImageSource DeadHead = LoadImage("DeadHead");

        private static ImageSource  LoadImage (string fileName)
        {
            return new BitmapImage(new Uri($"Assets/{fileName}.png", UriKind.Relative));
        }
    }
}
