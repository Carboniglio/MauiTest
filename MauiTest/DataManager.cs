using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MauiTest
{
    public enum DataManagerKeys
    {
        tempPhotokey,
        Username,
        Password,
        isPasswordRemembered,
        LAST

    }


    internal static class DataManager
    {
        public static Dictionary<string, string> FilePaths;
        public static List<CardInfo> SavedCards;
        private static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "saved_cards.json");

        public static async  void Initialize()
        {
            SavedCards = new List<CardInfo>();
            FilePaths = new Dictionary<string, string>();
            await SavedCardsListFiller();
        }

        #region card saver data
        public static async Task SaveCardListToJsonAsync(List<CardInfo> cards)
        {
            string json = JsonSerializer.Serialize(cards);
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<List<CardInfo>> LoadCardsFromJsonAsync()
        {
            if (!File.Exists(filePath))
                return new List<CardInfo>();

            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<CardInfo>>(json) ?? new List<CardInfo>();
        }

        public static async Task SavedCardsListFiller()
        {

            if (File.Exists(filePath))
            {
                var cards = await LoadCardsFromJsonAsync();
                SavedCards.Clear();
                foreach (var card in cards)
                    SavedCards.Add(card);

            }
            else
                return;

        }

        public static async Task SaveCardsAsync( CardInfo card)
        {
            SavedCards.Add(card);
            await SaveCardListToJsonAsync(SavedCards.ToList());
            SavedCards.Clear();
         
        }
        public static async Task DeleteAllCards()
        {
            SavedCards.Clear();
            await SaveCardListToJsonAsync(SavedCards.ToList());
        }

        
        #endregion
        public static void SaveDataPath(string key, string value)
        {
            FilePaths.Add(key, value);
        }

        public static string GetDataPath(string key)
        {
            return FilePaths[key];
        }

        public static void DeleteDataPath(string key)
        {
            FilePaths.Remove(key);
        }


        //use this to save settings 
        public static void SavePreference<T>(Enum dataManagerKey, T value)
        {
            string key = dataManagerKey.ToString();

            switch (value)
            {
                case string s:
                    Preferences.Set(key, s);
                    break;

                case int i:
                    Preferences.Set(key, i);
                    break;

                case float f:
                    Preferences.Set(key, f);
                    break;
                case bool b:
                    Preferences.Set(key, b);
                    break;

            }


        }

        public static T LoadPreference<T>(Enum dataManagerKey)
        {
            string key = dataManagerKey.ToString();

            var type = typeof(T);
            if (type == typeof(string))
                return (T)(object)Preferences.Get(key, string.Empty);
            if (type == typeof(int))
                return (T)(object)Preferences.Get(key, 0);
            if (type == typeof(float))
                return (T)(object)Preferences.Get(key, 0.0f);
            if (type == typeof(bool))
                return (T)(object)Preferences.Get(key, false);

            return default;

        }

        //use this to save sensible data (username , password , api key  etc ) 
        public static void SaveSecureStorage(string key, string value)
        {
            SecureStorage.SetAsync(key, value).Wait();

        }
        public static async Task<string?> LoadSecureStorage(string key)
        {
            string? value = await SecureStorage.GetAsync(key);
            return value;
        }
    }
}
