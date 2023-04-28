using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using MedicalOffice.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NordicDoor_Group15.Areas.Identity.Data;
using NordicDoor_Group15.Core;
using NordicDoor_Group15.Data;
using NordicDoor_Group15.Models;
using NordicDoor_Group15.Utilities;
using NordicDoor_Group15.ViewModels;
using static NordicDoor_Group15.Core.Constants;

namespace NordicDoor_Group15.Controllers
{
    
    public class TeamsController : Controller
    {
        private readonly ApplicationIdentityDbContext _context;

        public TeamsController(ApplicationIdentityDbContext context)
        {
            _context = context;

        }

        // GET: Teams
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager},{Constants.Roles.User}")]
        public async Task<IActionResult> Index(string SearchString, string? UserID, int? page, int? pageSizeID)
        {
            //Supply SelectList for User
            ViewData["UserID"] = new SelectList(_context
                .Users
                .OrderBy(s => s.FullName), "Id", "FullName");

            //Toggle the Open/Closed state of the collapse depending on if we are filtering
            ViewData["Filtering"] = ""; //Assume not filtering
            //Then in each "test" for filtering, add ViewData["Filtering"] = " show" if true;

            var teams = from d in _context.Teams
                .Include(d => d.Memberships).ThenInclude(d => d.User)
                        select d;

          
            if (!String.IsNullOrEmpty(SearchString))
            {
                teams = teams.Where(p => p.TeamNumber.ToUpper().Contains(SearchString.ToUpper())
                                       || p.TeamName.ToUpper().Contains(SearchString.ToUpper()) || p.Memberships.Any(m => m.User.FirstName.Contains(SearchString) || m.User.LastName.Contains(SearchString)));

                ViewData["Filtering"] = " show";
            }
            // Always sort by Doctor Name
            teams = teams
                        .OrderBy(p => p.TeamNumber)
                        .ThenBy(p => p.TeamName);

            //Add as many filters as needed
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, "Teams");
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Team>.CreateAsync(teams.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);
        }

        // GET: Teams/Details/5
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager},{Constants.Roles.User}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(d => d.Memberships).ThenInclude(d => d.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager}")]
        public IActionResult Create()
        {
            Team team = new Team();
            PopulateAssignedMembershipData(team);
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TeamNumber,TeamName")] Team team, string[] selectedOptions)
        {
            try
            {
                UpdateTeamMembership(selectedOptions, team);
                if (ModelState.IsValid)
                {
                    _context.Add(team);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException /* dex */) //entityframework core will retry transaction 5(it should be) times then ti will give up and raise one of these retyr limit exceeded exeption
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            //Validation Error so give the user another chance.
            PopulateAssignedMembershipData(team);
            return View(team);
        }

        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager}")]
        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(d => d.Memberships).ThenInclude(d => d.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (team == null)
            {
                return NotFound();
            }
            PopulateAssignedMembershipData(team);

            
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager}")]
        public async Task<IActionResult> Edit(int id, string[] selectedOptions)
        {
            //Go get the Team to update
            //Go get the Team to update
            var teamToUpdate = await _context.Teams
                .Include(d => d.Memberships).ThenInclude(d => d.User)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (teamToUpdate == null)
            {
                return NotFound();
            }

            //if (User.IsInRole($"{Constants.Roles.TeamManager}"))
            //{
            //    if (teamToUpdate.CreatedBy != User.Identity.Name)
            //    {
            //        ModelState.AddModelError("", "As a Team Manager, you cannot edit this " +
            //            "Team because you did not creator of this team.");
            //        ViewData["NoSubmit"] = "disabled=disabled";
            //    }
            //}
            //Update the Team's member
            UpdateTeamMembership(selectedOptions, teamToUpdate);

            if (await TryUpdateModelAsync<Team>(teamToUpdate, "", d => d.TeamNumber, d => d.TeamName))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(teamToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            //Validation Error so give the user another chance.
            PopulateAssignedMembershipData(teamToUpdate);
            return View(teamToUpdate);
           
        }

        // GET: Teams/Delete/5
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(d => d.Memberships).ThenInclude(d => d.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teams == null)
            {
                return Problem("Entity set 'ApplicationIdentityDbContext.Team'  is null.");
            }
            var team = await _context.Teams
              .Include(d => d.Memberships).ThenInclude(d => d.User)
              .FirstOrDefaultAsync(m => m.ID == id);

            if (team != null)
            {
                _context.Teams.Remove(team);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private void PopulateAssignedMembershipData(Team team)
        {
            //For this to work, you must have Included the child collection in the parent object
            var allOptions = _context.Users; //return all user to system
            var currentOptionsHS = new HashSet<string>(team.Memberships.Select(b => b.UserID)); //hashset current user option team has
            //Instead of one list with a boolean, we will make two lists
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.Id))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = s.Id,
                        DisplayText = s.FullName
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = s.Id,
                        DisplayText = s.FullName
                    });
                }
            }

            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }

        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager}")]
        private void UpdateTeamMembership(string[] selectedOptions, Team teamToUpdate)
        {
            if (selectedOptions == null)
            {
                teamToUpdate.Memberships = new List<Membership>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<string>(teamToUpdate.Memberships.Select(b => b.UserID));
            foreach (var s in _context.Users)
            {
                if (selectedOptionsHS.Contains(s.Id.ToString()))//it is selected
                {
                    if (!currentOptionsHS.Contains(s.Id))//but not currently in the Team's collection - Add it!
                    {
                        teamToUpdate.Memberships.Add(new Membership
                        {
                            UserID = s.Id,
                            TeamID = teamToUpdate.ID
                        });
                    }
                }
                else //not selected
                {
                    if (currentOptionsHS.Contains(s.Id))//but is currently in the Team's collection - Remove it!
                    {
                        Membership specToRemove = teamToUpdate.Memberships.FirstOrDefault(d => d.UserID == s.Id);
                        _context.Remove(specToRemove);
                    }
                }
            }
        }
        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.ID == id);
        }
    }
}
