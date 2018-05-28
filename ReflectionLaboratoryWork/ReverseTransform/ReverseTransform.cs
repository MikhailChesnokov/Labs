namespace ReverseTransform
{
    using System.Drawing;
    using Interface;



    public class ReverseTransform : IPlugin
    {
        public string Name => "Переворот изображения";

        public string Version => string.Empty;

        public string Author => string.Empty;



        public void Transform(IMainApp app)
        {
            Bitmap bitmap = app.Image;

            for (int i = 0; i < bitmap.Width; ++i)
            for (int j = 0; j < bitmap.Height / 2; ++j)
            {
                Color color = bitmap.GetPixel(i, j);
                bitmap.SetPixel(i, j, bitmap.GetPixel(i, bitmap.Height - j - 1));
                bitmap.SetPixel(i, bitmap.Height - j - 1, color);
            }

            app.Image = bitmap;
        }
    }
}