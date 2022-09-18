using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TestLoadingPictures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await ShowImageAsync(Image1, "https://http.cat/200");
            await ShowImageAsync(Image2, "https://http.cat/301");
            await ShowImageAsync(Image3, "https://http.cat/303");
            await ShowImageAsync(Image4, "https://http.cat/307");
            await ShowImageAsync(Image5, "https://http.cat/401");
            await ShowImageAsync(Image6, "https://http.cat/420");
            
        }

        private async Task ShowImageAsync(Image image, string url)
        {
            var httpClient = new HttpClient();
            var responce = await httpClient.GetAsync(url);
            var imageBytes = await responce.Content.ReadAsByteArrayAsync();
            image.Source = LoadImage(imageBytes);

        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
