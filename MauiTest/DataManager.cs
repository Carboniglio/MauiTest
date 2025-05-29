using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static  Dictionary<string, string> FilePaths;
        public static List<string> CardNames;

        public static void Initialize ()
        {
            FilePaths = new Dictionary<string, string>();
            CardNames = new List<string>();
        }

        public static void SaveCardName( string value)
        {
           CardNames.Add(value);
        }
        public static void DeleteCardName( string value)
        {
           CardNames.Remove(value);
        }


        public static void SaveDataPath(string key , string value)
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
        public static void SavePreference<T>(string key , T value)
        {
            switch(value)
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
                case bool b :
                    Preferences.Set(key, b);
                    break;
                
            }
               
            
        }

        public static T LoadPreference<T>(string key )
        {
            var type = typeof(T);
            if (type == typeof(string)) 
                return (T)(object)Preferences.Get(key, string.Empty);
            if (type == typeof(int))
                return (T)(object)Preferences.Get(key,0);
            if (type == typeof(float))
                return (T)(object)Preferences.Get(key, 0.0f);
            if (type == typeof(bool))
                return (T)(object)Preferences.Get(key,false);

            return default;

        }

        //use this to save sensible data (username , password , api key  etc ) 
        public static void SaveSecureStorage(string key ,string  value)
        {
         SecureStorage.SetAsync(key, value).Wait();

        }
        public static async Task<string?>  LoadSecureStorage(string key)
        {
            string? value = await SecureStorage.GetAsync(key);
            return value;
        }
    }
}
