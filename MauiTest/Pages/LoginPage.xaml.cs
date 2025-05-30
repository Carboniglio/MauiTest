namespace MauiTest;

public partial class LoginPage : ContentPage 
{
    
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
       if (BindingContext is LoginViewModel vm )
        {
            vm.RefreshPlaceholder();
        }
    }


    private void OnLoginClicked(object sender, EventArgs e)
    {
        if (isUsernameSaved.IsChecked)
        {
            
            DataManager.SavePreference(DataManagerKeys.Username, Username.Text.ToString());
        }

        //add here password check with database API

    }
}