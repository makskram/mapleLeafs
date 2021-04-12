using HockeyTeam.DAL;
using HockeyTeam.Models;
using HockeyTeam.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HockeyTeam.Controllers
{
    public class PlayersController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Players
        public ActionResult Index(string Position, string search, string sortBy, int? page)
        {
            //instantiate a new view model
            PlayerIndexViewModel viewModel = new PlayerIndexViewModel();

            //select the Players
            var players = db.Players.Include(p => p.Position);

            //perform the search and save the search string to the viewModel
            if (!String.IsNullOrEmpty(search))
            {
                players = players.Where(p => p.Name.Contains(search) ||
                                             p.Description.Contains(search) ||
                                             p.Position.Name.Contains(search));
                viewModel.Search = search;
            }

            //group search results into Positions and count how many items in each Position
            viewModel.CatsWithCount = from matchingPlayers in players
                                      where
                                      matchingPlayers.PositionID != null
                                      group matchingPlayers by
                                       matchingPlayers.Position.Name into
                                      catGroup
                                      select new PositionWithCount()
                                      {
                                          PositionName = catGroup.Key,
                                          PlayerCount = catGroup.Count()
                                      };

            if (!String.IsNullOrEmpty(Position))
            {
                players = players.Where(p => p.Position.Name == Position);
                viewModel.Position = Position;
            }

            //sort the results
            switch (sortBy)
            {
                case "score_lowest":
                    players = players.OrderBy(p => p.Score);
                    break;
                case "score_highest":
                    players = players.OrderByDescending(p => p.Score);
                    break;
                default:
                    players = players.OrderBy(p => p.Name);
                    break;
            }

            const int PageItems = 3;
            int currentPage = (page ?? 1);
            viewModel.Players = players.ToPagedList(currentPage, PageItems);
            viewModel.SortBy = sortBy;
            viewModel.Sorts = new Dictionary<string, string>
            {
                {"Score low to high", "score_lowest" },
                {"Score high to low", "score_highest" }
            };

            return View(viewModel);
        }


        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            PlayerViewModel viewModel = new PlayerViewModel();
            viewModel.PositionList = new SelectList(db.Positions, "ID", "Name");
            viewModel.ImageLists = new List<SelectList>();
            for (int i = 0; i < Constants.NumberOfPlayerImages; i++)
            {
                viewModel.ImageLists.Add(new SelectList(db.PlayerImages, "ID", "FileName"));
            }
            return View(viewModel);
        }

        // POST: Players/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerViewModel viewModel)
        {
            Player player = new Player();
            player.Name = viewModel.Name;
            player.Description = viewModel.Description;
            player.Score = viewModel.Score;
            player.PositionID = viewModel.PositionID;
            player.PlayerImageMappings = new List<PlayerImageMapping>();
            //get a list of selected images without any blanks
            string[] playerImages = viewModel.PlayerImages.Where(pi =>
                !string.IsNullOrEmpty(pi)).ToArray();
            for (int i = 0; i < playerImages.Length; i++)
            {
                player.PlayerImageMappings.Add(new PlayerImageMapping
                {
                    PlayerImage = db.PlayerImages.Find(int.Parse(playerImages[i])),
                    ImageNumber = i
                });
            }


            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            viewModel.PositionList = new SelectList(db.Positions, "ID", "Name", player.PositionID);
            viewModel.ImageLists = new List<SelectList>();
            for (int i = 0; i < Constants.NumberOfPlayerImages; i++)
            {
                viewModel.ImageLists.Add(new SelectList(db.PlayerImages, "ID", "FileName",
                    viewModel.PlayerImages[i]));
            }

            return View(viewModel);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            PlayerViewModel viewModel = new PlayerViewModel();
            viewModel.PositionList = new SelectList(db.Positions, "ID", "Name", player.PositionID);
            viewModel.ImageLists = new List<SelectList>();
            foreach (var imageMapping in player.PlayerImageMappings.OrderBy(pim => pim.ImageNumber))
            {
                viewModel.ImageLists.Add(new SelectList(db.PlayerImages, "ID", "FileName",
                    imageMapping.PlayerImageID));
            }
            for (int i = viewModel.ImageLists.Count; i < Constants.NumberOfPlayerImages; i++)
            {
                viewModel.ImageLists.Add(new SelectList(db.PlayerImages, "ID", "FileName"));
            }
            viewModel.ID = player.ID;
            viewModel.Name = player.Name;
            viewModel.Description = player.Description;
            viewModel.Score = player.Score;
            return View(viewModel);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerViewModel viewModel)
        {
            var playerToUpdate = db.Players.Include(p => p.PlayerImageMappings).Where(p => p.ID == viewModel.ID).Single();

            if (TryUpdateModel(playerToUpdate, "", new string[] { "Name", "Description", "Score", "PositionID" }))
            {
                if (playerToUpdate.PlayerImageMappings == null)
                {
                    playerToUpdate.PlayerImageMappings = new List<PlayerImageMapping>();
                }
                //get a list of selected images without any blanks
                string[] playerImages = viewModel.PlayerImages.Where(pi =>
                    !string.IsNullOrEmpty(pi)).ToArray();
                for (int i = 0; i < playerImages.Length; i++)
                {
                    //get the image currently stored
                    var imageMappingToEdit = playerToUpdate.PlayerImageMappings.Where(pim => pim.
                        ImageNumber == i).FirstOrDefault();
                    //find the new image
                    var image = db.PlayerImages.Find(int.Parse(playerImages[i]));
                    //if there is nothing stored then we need to add a new mapping
                    if (imageMappingToEdit == null)
                    {
                        //add image to the imagemappings
                        playerToUpdate.PlayerImageMappings.Add(new PlayerImageMapping
                        {
                            ImageNumber = i,
                            PlayerImage = image,
                            PlayerImageID = image.ID
                        });
                    }
                    //else it's not a new file so edit the current mapping
                    else
                    {
                        //if they are not the same
                        if (imageMappingToEdit.PlayerImageID != int.Parse(playerImages[i]))
                        {
                            //assign image property of the image mapping
                            imageMappingToEdit.PlayerImage = image;
                        }
                    }
                }

                //delete any other imagemappings that the user did not include in their
                //selections for the product
                for (int i = playerImages.Length; i < Constants.NumberOfPlayerImages; i++)
                {
                    var imageMappingToEdit = playerToUpdate.PlayerImageMappings.Where(pim =>
                        pim.ImageNumber == i).FirstOrDefault();
                    //if there is something stored in the mapping
                    if (imageMappingToEdit != null)
                    {
                        db.PlayerImageMappings.Remove(imageMappingToEdit);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}