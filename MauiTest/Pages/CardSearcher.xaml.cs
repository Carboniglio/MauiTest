using System.Text.Json;
using System.Net.Http;
namespace MauiTest;

public partial class CardSearcher : ContentPage
{
    private readonly HttpClient _httpClient = new();
    private string url;

    private string name;
    private string cardImageUrl;
    public CardSearcher()
    {
        InitializeComponent();
    }


    private async void OnSearchClicked(object sender, EventArgs e)
    {
        name = CardNameEntry.Text;
        string cardName = CardNameEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(cardName))
        {
            await DisplayAlert("Errore", "Inserisci un nome di carta valido.", "OK");
            return;
        }

        url = $"https://api.scryfall.com/cards/named?fuzzy={Uri.EscapeDataString(cardName)}";

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                string errorBody = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Errore HTTP", $"Codice: {response.StatusCode}\n{errorBody}", "OK");
                return;
            }

            string json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            name = root.GetProperty("name").GetString();
            cardImageUrl = root.GetProperty("image_uris").GetProperty("normal").GetString();

            CardTitle.Text = cardName;
            CardImage.Source = ImageSource.FromUri(new Uri(cardImageUrl));
            AddCardBtn.IsVisible = true;


        }
        catch (Exception ex)
        {
            await DisplayAlert("Errore generico", ex.Message, "OK");
        }
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        CardNameEntry.Unfocus();
    }
    private async void OnAddClicked(object sender, EventArgs e)
    {
        if (name == null || cardImageUrl == null)
        {
            return;
        }

        var cardName = new CardInfo(name, cardImageUrl);
        if (cardName !=null)
        {
        await DataManager.SaveCardsAsync(cardName);

        await DisplayAlert("alert", "Card Added Succesfully", "OK");

        }
        else
            await DisplayAlert("alert", "Card  not Added ", "OK");


    }
}