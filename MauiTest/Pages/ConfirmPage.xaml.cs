using MauiTest;

namespace MauiTest
{
public partial class ConfirmPage: ContentPage
{
        string path;
        
	public ConfirmPage()
	{
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //if (!string.IsNullOrEmpty(photoPath))
            //{
            //    PhotoPreview.Source = photoPath;
            //    path = photoPath;
            //}
            //else
            //    path = string.Empty;
            path = DataManager.GetDataPath(DataManagerKeys.tempPhotokey.ToString());
            PhotoPreview.Source = path;
        }
        private async void RetryClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CameraPage"); 

        }

        private async void ConfirmClicked (object sender , EventArgs e )
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }

}