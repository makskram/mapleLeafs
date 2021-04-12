using HockeyTeam.Models;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HockeyTeam.ViewModels
{
    public class PlayerIndexViewModel
    {
        public IPagedList<Player> Players { get; set; }
        public string Search { get; set; }
        public IEnumerable<PositionWithCount> CatsWithCount { get; set; }
        public string Position { get; set; }
        public string SortBy { get; set; }
        public Dictionary<string, string> Sorts { get; set; }

        public IEnumerable<SelectListItem> CatFilterItems
        {
            get
            {
                var allCats = CatsWithCount.Select(cc => new SelectListItem
                {
                    Value = cc.PositionName,
                    Text = cc.CatNameWithCount
                });

                return allCats;
            }
        }
    }

    public class PositionWithCount
    {
        public int PlayerCount { get; set; }
        public string PositionName { get; set; }
        public string CatNameWithCount
        {
            get
            {
                return PositionName + " (" + PlayerCount.ToString() + ")";
            }
        }
    }
}