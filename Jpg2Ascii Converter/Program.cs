using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace JpgToAscii_Converter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Write("Enter picture path :> ");
            string imagePathNName = Console.ReadLine();
            string txtName = "";

            txtName = imagePathNName.Split('/', '\\').Last().Replace(".png", ".txt").Replace(".jpg", ".txt");
            
            Bitmap image = new Bitmap(imagePathNName);

            int width = 480; //Ascii art quality
            int height = (int)(image.Height * ((float)width / image.Width));
            Bitmap resizedImage = new Bitmap(image, new Size(width, height));

            char[] asciiChars = { '☻', '☺', '&', '#', '@', '%', '=', '+', '*', ':', '-', '.', ' ' };
            string asciiArt = "";
            for (int row = 0; row < resizedImage.Height; row++)
            {
                for (int col = 0; col < resizedImage.Width; col++)
                {
                    Color pixelColor = resizedImage.GetPixel(col, row);
                    int brightness = (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3f / 255 * (asciiChars.Length - 1));

                    asciiArt += " " + asciiChars[brightness];
                }
                asciiArt += "\n";

                float p = (row * resizedImage.Width * 100) / (resizedImage.Height * resizedImage.Width);
                Console.WriteLine(p + "%");
            }

            File.WriteAllText(txtName, asciiArt);
            Process.Start(txtName);
        }
    }
}