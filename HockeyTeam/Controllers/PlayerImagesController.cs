using HockeyTeam.DAL;
using HockeyTeam.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace HockeyTeam.Controllers
{
    public class PlayerImagesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: PlayerImages
        public ActionResult Index()
        {
            return View(db.PlayerImages.ToList());
        }

        // GET: PlayerImages/Create
        public ActionResult Upload()
        {
            return View();
        }

        // POST: PlayerImages/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase[] files)
        {
            bool allValid = true;
            string inValidFiles = "";
            //check the user has entered a file
            if (files[0] != null)
            {
                //if the user has entered less than ten files
                if (files.Length <= 10)
                {
                    //check they are all valid
                    foreach (var file in files)
                    {
                        if (!ValidateFile(file))
                        {
                            allValid = false;
                            inValidFiles += ", " + file.FileName;
                        }
                    }
                    //if they are all valid then try to save them to disk
                    if (allValid)
                    {
                        foreach (var file in files)
                        {
                            try
                            {
                                SaveFileToDisk(file);
                            }
                            catch (Exception)
                            {
                                ModelState.AddModelError("FileName", "Sorry an error occurred saving the files to disk, please try again");
                            }
                        }
                    }
                    //else add an error listing out the invalid files
                    else
                    {
                        ModelState.AddModelError("FileName", "All files must be gif, png, jpeg or jpg and less than 2MB in size.The following files" + inValidFiles + " are not valid");
                    }
                }
                //the user has entered more than 10 files
                else
                {
                    ModelState.AddModelError("FileName", "Please only upload up to ten files at a time");
                }
            }
            else
            {
                //if the user has not entered a file return an error message
                ModelState.AddModelError("FileName", "Please choose a file");
            }
            if (ModelState.IsValid)
            {
                bool duplicates = false;
                bool otherDbError = false;
                string duplicateFiles = "";

                foreach (var file in files)
                {
                    //try and save each file
                    var playerToAdd = new PlayerImage { FileName = file.FileName };
                    try
                    {
                        db.PlayerImages.Add(playerToAdd);
                        db.SaveChanges();
                    }
                    //if there is an exception check if it is caused by a duplicate file
                    catch (DbUpdateException ex)
                    {
                        SqlException innerException = ex.InnerException.InnerException as SqlException;
                        if (innerException != null && innerException.Number == 2601)
                        {
                            duplicateFiles += ", " + file.FileName;
                            duplicates = true;
                            db.Entry(playerToAdd).State = EntityState.Detached;
                        }
                        else
                        {
                            otherDbError = true;
                        }
                    }
                }
                //add a list of duplicate files to the error message
                if (duplicates)
                {
                    ModelState.AddModelError("FileName", "All files uploaded except the files" + duplicateFiles + ", which already exist in the system." + " Please delete them and try again if you wish to re-add them");
                    return View();
                }
                else if (otherDbError)
                {
                    ModelState.AddModelError("FileName", "Sorry an error has occurred saving to the database, please try again");
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: PlayerImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlayerImage playerImage = db.PlayerImages.Find(id);

            var mappings = playerImage.PlayerImageMappings.Where(pim => pim.PlayerImageID == id);
            foreach (var mapping in mappings)
            {

                var mappingsToUpdate = db.PlayerImageMappings.Where(pim => pim.PlayerID == mapping.PlayerID);

                foreach (var mappingToUpdate in mappingsToUpdate)
                {
                    if (mappingToUpdate.ImageNumber > mapping.ImageNumber)
                    {
                        mappingToUpdate.ImageNumber--;
                    }
                }
            }

            System.IO.File.Delete(Request.MapPath(Constants.PlayerImagePath + playerImage.FileName));
            System.IO.File.Delete(Request.MapPath(Constants.PlayerThumbnailPath + playerImage.FileName));
            db.PlayerImages.Remove(playerImage);
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

        private bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((file.ContentLength > 0 && file.ContentLength < 2097152) && allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }

        private void SaveFileToDisk(HttpPostedFileBase file)
        {
            WebImage img = new WebImage(file.InputStream);
            if (img.Width > 190)
            {
                img.Resize(190, img.Height);
            }
            img.Save(Constants.PlayerImagePath + file.FileName);
            if (img.Width > 100)
            {
                img.Resize(100, img.Height);
            }
            img.Save(Constants.PlayerThumbnailPath + file.FileName);
        }

    }
}
