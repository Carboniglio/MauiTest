using Microsoft.Maui.Controls;

namespace MauiTest;

public partial class CardShower : ContentPage
{
    public CardShower()
    {
        InitializeComponent();
       
        BindingContext = DataManager.SavedCards;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //await DataManager.DeleteAllCards();

         await DataManager.SavedCardsListFiller();
        

        await DisplayAlert("Debug", $"Carte caricate: {DataManager.SavedCards.Count}", "OK");
    }
}