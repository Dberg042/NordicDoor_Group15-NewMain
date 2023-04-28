using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NordicDoor_Group15.Data;
using NordicDoor_Group15.Core;
using NordicDoor_Group15.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Constants = NordicDoor_Group15.Core.Constants;
using NordicDoor_Group15.Areas.Identity.Data;
using MedicalOffice.Utilities;
using System.Drawing.Printing;
using Microsoft.EntityFrameworkCore.Storage;
using NordicDoor_Group15.Utilities;

namespace NordicDoor_Group15.Controllers
{
    
    public class SuggestionsController : Controller
    {
        private readonly ApplicationIdentityDbContext _context;

        public SuggestionsController(ApplicationIdentityDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }

        // GET: Suggestions. It enables to search and filter with parameters belov
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager},{Constants.Roles.User}")]
        public async Task<IActionResult> Index(string searchString, int? TeamID, string? CreatorID,int? page, int? pageSizeID, string actionButton, string sortDirection = "asc", string sortField = "Suggestion")

        {
            //Clear the sort/filter/paging URL Cookie for Controller
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);

            //Toggle the Open/Closed state of the collapse depending on if we are filtering
            ViewData["Filtering"] = ""; //Assume not filtering
            //Then in each "test" for filtering, add ViewData["Filtering"] = " show" if true;

            PopulateDropDownLists();    //Data for team Filter DDL

            //List of sort options.
            //NOTE: make sure this array has matching values to the column headings
            string[] sortOptions = new[] { "Title", "Creator", "StartDate", "Team", "Status", "Category" };

            var suggestion = _context.Suggestions.
                Include(e => e.Team).
                Include(e => e.Creator).
                Include(e => e.SuggestionThumbnail).
                AsNoTracking();

            if (TeamID.HasValue)
            {
                suggestion = suggestion.Where(p => p.TeamID == TeamID);
                ViewData["Filtering"] = " show";
            }
            if (!string.IsNullOrEmpty(CreatorID))
            {
                suggestion = suggestion.Where(p => p.CreatorID == CreatorID);
                ViewData["Filtering"] = " show";
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                suggestion = suggestion.Where(p => p.Title.ToUpper().Contains(searchString.ToUpper())
                                       || p.Description.ToUpper().Contains(searchString.ToUpper()));
                ViewData["Filtering"] = " show";
            }

            //Before we sort, see if we have called for a change of filtering or sorting
            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted!
            {
                page = 1;//Reset page to start
               
                if (sortOptions.Contains(actionButton))//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }

            if (sortField == "Title")
            {
                if (sortDirection == "asc")
                {
                    suggestion = suggestion
                        .OrderBy(p => p.Title);
                }
                else
                {
                    suggestion = suggestion
                        .OrderByDescending(p => p.Title);
                }
            }
            if (sortField == "Creator")
            {
                if (sortDirection == "asc")
                {
                    suggestion = suggestion
                        .OrderBy(p => p.Creator.FullName);
                }
                else
                {
                    suggestion = suggestion
                        .OrderByDescending(p => p.Creator.FullName);
                }
            }
            else if (sortField == "StartDate")
            {
                if (sortDirection == "asc")
                {
                    suggestion = suggestion
                        .OrderByDescending(p => p.StartDate);
                }
                else
                {
                    suggestion = suggestion
                        .OrderBy(p => p.StartDate);
                }
            }
            else if (sortField == "Team")
            {
                if (sortDirection == "asc")
                {
                    suggestion = suggestion
                        .OrderBy(p => p.Team.ID);

                }
                else
                {
                    suggestion = suggestion
                        .OrderByDescending(p => p.Team.ID);

                }
            }
            else if (sortField == "Status")
            {
                if (sortDirection == "asc")
                {
                    suggestion = suggestion
                        .OrderBy(p => p.Status);

                }
                else
                {
                    suggestion = suggestion
                        .OrderByDescending(p => p.Status);

                }
            }
            else if (sortField == "Category")
            {
                if (sortDirection == "asc")
                {
                    suggestion = suggestion
                        .OrderBy(p => p.Category);

                }
                else
                {
                    suggestion = suggestion
                        .OrderByDescending(p => p.Category);

                }
            }
            else //Sorting by Employee Name
            {
                if (sortDirection == "asc")
                {
                    suggestion = suggestion
                        .OrderBy(p => p.Id);
                        
                }
                else
                {
                    suggestion = suggestion
                        .OrderByDescending(p => p.Id);
                }
            }
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, "Suggestions");
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Suggestion>.CreateAsync(suggestion.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }


        // GET: Suggestions/Details/5
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager},{Constants.Roles.User}")]
        public async Task<IActionResult> Details(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (id == null || _context.Suggestions == null)
            {
                return NotFound();
            }

            var suggestion= await _context.Suggestions.
                Include(e => e.Team).
                Include(e => e.Creator).
                Include(e => e.SuggestionPhoto).
                Include(i=> i.Photos).
                AsNoTracking().
                FirstOrDefaultAsync(m => m.Id == id);

           
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        // GET: Suggestions/Create
        public IActionResult Create()
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            var suggestion = new Suggestion();
            
            PopulateDropDownLists();
            return View();
        }
        // POST: Suggestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager},{Constants.Roles.User}")]
        public async Task<IActionResult>Create([Bind("Id,Title,Description,MainBody,TeamID,CreatorID," +
            "StartDate,Status,Category")] bool Justdoit, Suggestion suggestion, IFormFile thePicture)
        {

            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            try
            {
                
                if (ModelState.IsValid)
                {
                    await AddPicture(suggestion, thePicture);
                    _context.Add(suggestion);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction("Details", new { suggestion.Id });
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            
            PopulateDropDownLists(suggestion);
            return View(suggestion);

        }

        // GET: Suggestions/Edit/5
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager},{Constants.Roles.User}")]
        public async Task<IActionResult> Edit(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (id == null || _context.Suggestions == null)
            {
                return NotFound();
            }

            //var suggestion = await _context.Suggestions.FindAsync(id);
            var suggestion = await _context.Suggestions.
                Include(e => e.SuggestionPhoto).
                AsNoTracking().
                FirstOrDefaultAsync(m => m.Id == id);


            if (suggestion == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(suggestion);

            //if (User.IsInRole($"{Constants.Roles.User}"))
            //{
            //    if (suggestion.CreatedBy!= User.Identity.Name)
            //    {
            //        ModelState.AddModelError("", "As a Team Manager, you cannot edit this " +
            //            "Team because you did not enter them into the system.");
            //        ViewData["NoSubmit"] = "disabled=disabled";
            //    }
            //}

            return View(suggestion);
        }

        // POST: Suggestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager},{Constants.Roles.User}")]
        public async Task<IActionResult> Edit(int id, string chkRemoveImage, IFormFile thePicture/*, [Bind("Id,Title,Description,MainBody,Team,Owner,StartDate,Status,Justdoit,Category")] Suggestion suggestion*/)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            //var suggestionToUpdate = await _context.Suggestions.SingleOrDefaultAsync(p => p.Id == id);
            var suggestionToUpdate = await _context.Suggestions.
                Include(e => e.SuggestionPhoto).
                FirstOrDefaultAsync(m => m.Id == id);

            //if (User.IsInRole($"{Constants.Roles.User}"))
            //{
            //    if (suggestionToUpdate.CreatedBy != User.Identity.Name)
            //    {
            //        ModelState.AddModelError("", "As a Team Manager, you cannot edit this " +
            //            "Team because you did not enter them into the system.");
            //        ViewData["NoSubmit"] = "disabled=disabled";
            //    }
            //}

            if (suggestionToUpdate == null)
            {
                return NotFound();
            }

           
            //Try updating it with the values posted- My white list
            if (await TryUpdateModelAsync<Suggestion>(suggestionToUpdate, "",
                p => p.Title, p => p.Description, p => p.MainBody, p => p.TeamID, p => p.CreatorID,
                p => p.StartDate, p => p.Status, p => p.Category))
            {
                try
                {
                    //For the image
                    if (chkRemoveImage != null)
                    {
                        //If we are just deleting the two versions of the photo, we need to make sure the Change Tracker knows
                        //about them both so go get the Thumbnail since we did not include it.
                        suggestionToUpdate.SuggestionThumbnail = _context.SuggestionThumbnails.Where(p => p.SuggestionID == suggestionToUpdate.Id).FirstOrDefault();
                        //Then, setting them to null will cause them to be deleted from the database.
                        suggestionToUpdate.SuggestionPhoto = null;
                        suggestionToUpdate.SuggestionThumbnail = null;
                    }
                    else
                    {
                        await AddPicture(suggestionToUpdate, thePicture);
                    }
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index)); 
                    return RedirectToAction("Details", new {suggestionToUpdate.Id});
                }
                //try 5 times then raise exception
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuggestionExists(suggestionToUpdate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            
            PopulateDropDownLists(suggestionToUpdate);
            return View(suggestionToUpdate);
            
        }

        // GET: Suggestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (id == null || _context.Suggestions == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions
                .Include(e => e.Team)
                .Include(e => e.Creator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager}")]

        // POST: Suggestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (_context.Suggestions == null)
            {
                return Problem("Entity set 'ApplicationIdentityDbContext.Suggestion'  is null.");
            }
            var suggestion = await _context.Suggestions
                .Include(e => e.Team)
                .Include(e => e.Creator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            try
            {
                if (suggestion != null)
                {
                    _context.Suggestions.Remove(suggestion);
                }

                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index)); //since we are using ViewDataReturnURL
                return Redirect(ViewData["returnURL"].ToString());
            }
            catch (DbUpdateException)
            {

                //Note: there is really no reason a delete should fail if you can "talk" to the database.
                ModelState.AddModelError("", "Unable to delete record. Try again, and if the problem persists see your system administrator.");
            }
            return View(suggestion);
        }

        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager},{Constants.Roles.User}")]
        private async Task AddPicture(Suggestion suggestion, IFormFile thePicture)
        {
            //Get the picture and save it with the Suggestion (2 sizes)
            if (thePicture != null)
            {
                string mimeType = thePicture.ContentType;
                long fileLength = thePicture.Length;
                if (!(mimeType == "" || fileLength == 0))//Looks like we have a file!!!
                {
                    if (mimeType.Contains("image"))
                    {
                        using var memoryStream = new MemoryStream();
                        await thePicture.CopyToAsync(memoryStream);
                        var pictureArray = memoryStream.ToArray();//Gives us the Byte[]

                        //Check if we are replacing or creating new
                        if (suggestion.SuggestionPhoto != null)
                        {
                            //We already have pictures so just replace the Byte[]
                            suggestion.SuggestionPhoto.Content = ResizeImage.shrinkImageWebp(pictureArray, 500, 600);

                            //Get the Thumbnail so we can update it.  Remember we didn't include it
                            suggestion.SuggestionThumbnail = _context.SuggestionThumbnails.Where(p => p.SuggestionID == suggestion.Id).FirstOrDefault();
                            suggestion.SuggestionThumbnail.Content = ResizeImage.shrinkImageWebp(pictureArray, 75, 90);
                        }
                        else //No pictures saved so start new
                        {
                            suggestion.SuggestionPhoto = new SuggestionPhoto
                            {
                                Content = ResizeImage.shrinkImageWebp(pictureArray, 500, 600),
                                MimeType = "image/webp"
                            };
                            suggestion.SuggestionThumbnail = new SuggestionThumbnail
                            {
                                Content = ResizeImage.shrinkImageWebp(pictureArray, 75, 90),
                                MimeType = "image/webp"
                            };
                        }
                    }
                }
            }
        }
       
        private SelectList TeamSelectList(int? selectedId)
        {
            return new SelectList(_context.Teams
                     .OrderBy(d => d.ID)
                     , "ID", "FullName", selectedId);
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        private SelectList CreatorSelectList(string? selectedId)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            return new SelectList(_context.Users
                     .OrderBy(d => d.Id)
                     , "Id", "FullName", selectedId);
        }

        //ControllerName,ViewDataReturnURL are related with utilities/maintainURL.cs
        private string ControllerName()
        {
           return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }
        
        private void PopulateDropDownLists(Suggestion suggestion = null)
        {
            ViewData["TeamID"] = TeamSelectList(suggestion?.TeamID);
            ViewData["CreatorID"] = CreatorSelectList(suggestion?.CreatorID);
        }
        private bool SuggestionExists(int id)
        {
          return _context.Suggestions.Any(e => e.Id == id);
        }
    }
    
}










