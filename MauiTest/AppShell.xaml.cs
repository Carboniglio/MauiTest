using MauiTest;

namespace MauiTest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CameraPage), typeof(CameraPage));
            Routing.RegisterRoute(nameof(CardShower), typeof(CardShower));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}
