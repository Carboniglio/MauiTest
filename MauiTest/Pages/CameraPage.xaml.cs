namespace MauiTest
{
    public partial class CameraPage : ContentPage
    {
        public CameraPage()
        {
            InitializeComponent();
        }

        private void MainCamera_MediaCaptured(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
        {
            if (Dispatcher.IsDispatchRequired)
            {
                Dispatcher.Dispatch(() => TestImage.Source = ImageSource.FromStream(() => e.Media));
                return;
            }
            TestImage.Source = ImageSource.FromStream(() => e.Media);
        }

        private async void SaveMediaClicked(object sender, EventArgs e)
        {
            try
            {
                // Scatta la foto
                var photo = await MediaPicker.CapturePhotoAsync();

                if (photo != null)
                {
                    // Prende il percorso temporaneo dell'app
                    string fileName = $"foto_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                    var tempFilePath = Path.Combine(FileSystem.CacheDirectory, fileName);
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                    // Copia la foto dal file originale (readonly) in una posizione scrivibile
                    using var stream = await photo.OpenReadAsync();
                    using var fileStream = File.OpenWrite(tempFilePath);
                    await stream.CopyToAsync(fileStream);

                    if (DataManager.FilePaths.ContainsKey(DataManagerKeys.tempPhotokey.ToString()))
                    {
                        DataManager.DeleteDataPath(DataManagerKeys.tempPhotokey.ToString());
                    }

                    DataManager.SaveDataPath(DataManagerKeys.tempPhotokey.ToString(), tempFilePath);

                    // Puoi ora usare tempFilePath per visualizzare o caricare la foto
                    Console.WriteLine($"?? Foto salvata in: {tempFilePath}");
                    await Navigation.PushAsync(new ConfirmPage());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Errore durante lo scatto: {ex.Message}");
            }

        }

    }
}