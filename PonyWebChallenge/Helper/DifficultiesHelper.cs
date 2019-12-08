using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static PonyWebChallenge.Models.DifficultiesEnum;

namespace PonyWebChallenge.Helper
{
    public class DifficultiesHelper
    {
        public static IEnumerable<SelectListItem> GetDifficulties()
        {
            var listViewModel = new List<SelectListItem>();
            foreach (string difficulty in Enum.GetNames(typeof(Difficulty)))
            {
                var viewModel = new SelectListItem();

                viewModel.Value = difficulty;
                viewModel.Text = difficulty;

                listViewModel.Add(viewModel);
            }
            return listViewModel.AsEnumerable();
        }

        public static bool ContainsDifficulty(Difficulty difficulty)
        {
            return Enum.IsDefined(typeof(Difficulty), difficulty);
        }
    }
}