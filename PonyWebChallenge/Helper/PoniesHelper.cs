using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PonyWebChallenge.Helper
{
    public class PoniesHelper
    {
        public static List<string> PonyNames = new List<string>()
        {
            #region Pony Names
                        "Fluttershy",
                        "Rainbow Dash",
                        "Rarity",
                        "Applejack",
                        "Twilight Sparkle",
                        "Pinkie Pie",
                        "Princess Celestia",
                        "Princess Cadance",
                        "Apple Bloom",
                        "Sweetie Belle",
                        "Scootaloo",
                        "Princess Luna",
                        "Spike",
                        "Starlight Glimmer",
                        "Furry Heart",
                        "Zecora",
                        "Daring Do",
                        "Lyra Heartstrings",
                        "Prince Shining Armor",
                        "Derpy Hooves",
                        "Big Macintosh",
                        "Derpy Hooves",
                        "Snow Drop",
                        "Spitfire",
                        "Thorax",
                        "Granny Smith",
                        "Pound Cake",
                        "Pumpkin Cake",
                        "Fizzlepop Berrytwist",
                        "Pinkamana",
                        "Discord",
                        "Cheerilee",
                        "Soarin'",
                        "Gabby",
                        "Mrs. Cup Cake",
                        "Octavia",
                        "Vinyl Scratch and Kara Music",
                        "Babs Seed",
                        "Sassy Saddles",
                        "Star Swirl the Bearded",
                        "Flash Sentry",
                        "Fleetfoot",
                        "Mr. Carrot Cake",
                        "Mayor Mare",
                        "Luckette",
                        "Joe",
                        "Snails",
                        "Wind rider",
                        "Snips"
            #endregion
        };

        public static bool ContainsPony(string ponyName)
        {
            return PonyNames.Any(x => x == ponyName);
        }

        public static IEnumerable<SelectListItem> GetPonies()
        {
            var listViewModel = new List<SelectListItem>();
            foreach (string pony in PonyNames)
            {
                var viewModel = new SelectListItem();

                viewModel.Value = pony;
                viewModel.Text = pony;

                listViewModel.Add(viewModel);
            }
            return listViewModel.AsEnumerable();
        }
    }


}