using MauiTest;


namespace MauiTest
{
    public partial class MainPage : ContentPage
    {
        string savedPhotoPath;
        private bool isPieroAngela = false;
        public MainPage()
        {
            InitializeComponent();
            DataManager.Initialize();
           savedPhotoPath= string.Empty;
        }

        private async void OnChangeCameraPageClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CameraPage");


        }
        private async void OnChangeLoginPageClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("LoginPage");

        }
        private void TestClicked(object sender, EventArgs e)
        {
            if (!isPieroAngela)
            {
                TopImage.Source = "piero_angela.jpg";
                TestBtn.Text = "PIERO ANGELA :)";
                isPieroAngela = true;
            }
            else
            {
                TopImage.Source = "dotnet_bot.png";
                TestBtn.Text = "niente piero angela :( ";
                isPieroAngela = false;
            }



        }

        private async void OnSearchCardClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CardSearcher");
        }

        private async void ShowCardsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("CardShower");
        }
    }
}

