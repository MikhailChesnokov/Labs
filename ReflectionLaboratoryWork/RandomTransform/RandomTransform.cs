namespace RandomTransform
{
    using System;
    using System.Drawing;
    using Interface;



    public class RandomTransform : IPlugin
    {
        public string Name => "Случайная трансформация";

        public string Version => string.Empty;

        public string Author => string.Empty;



        public void Transform(IMainApp app)
        {
            Bitmap bitmap = app.Image;
            Random rand = new Random(DateTime.Now.Millisecond);
            int pixels = (int)(0.1 * bitmap.Width * bitmap.Height);

            for (int i = 0; i < pixels; ++i)
                bitmap.SetPixel(rand.Next(bitmap.Width - 1), rand.Next(bitmap.Height), Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));

            app.Image = bitmap;
        }
    }
}