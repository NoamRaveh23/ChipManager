using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace ChipManager
{
	class FirebaseHelper
	{
        public static FirebaseClient firebase = new FirebaseClient("https://chipmanager-5c652-default-rtdb.europe-west1.firebasedatabase.app/");
        public static string database = "Games";

        public static async Task<List<Game>> GetAll()
        {
            return (await firebase
              .Child(database)
              .OnceAsync<Game>()).Select(item => new Game
              {
                  code = item.Object.code,
                  id = item.Object.id
              }).ToList();
        }

        public static async Task Add(Game game)
        {

            await firebase
              .Child(database)
              .PostAsync(game);
        }

        public static async Task<Game> Get(int code)
        {
            var allPersons = await GetAll();
            await firebase
              .Child(database)
              .OnceAsync<Game>();
            return allPersons.Where(a => a.code == code).FirstOrDefault();
        }

        public static async Task Update(Game state)
        {
            var toUpdatePerson = (await firebase
              .Child(database)
              .OnceAsync<Game>()).Where(a => a.Object.id == state.id).FirstOrDefault();

            await firebase
              .Child(database)
              .Child(toUpdatePerson.Key)
              .PutAsync(state);
        }

        public static async Task Delete(int personId)
        {
            var toDeletePerson = (await firebase
              .Child(database)
              .OnceAsync<Game>()).Where(a => a.Object.id == personId).FirstOrDefault();
            await firebase.Child(database).Child(toDeletePerson.Key).DeleteAsync();

        }
    }
}