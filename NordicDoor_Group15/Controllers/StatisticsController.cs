using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NordicDoor_Group15.Areas.Identity.Data;
using NordicDoor_Group15.Core;
using NordicDoor_Group15.Models;


namespace NordicDoor_Group15.Controllers
{

    public class StatisticsController : Controller
    {
        private readonly ApplicationIdentityDbContext _context;

        public StatisticsController(ApplicationIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Statistics/Details/5
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.TeamManager},{Constants.Roles.User}")]
        public ActionResult Details(int id)
        {
            //var a = GetTeamSuggestionSummary();

            return View();
        }
       
        //These code selecting Teams and count suggestion number and push to list to statistic
        [HttpPost]
        public List<object> GetTeamSuggestionSummary()
        {

            var data = _context.Teams.Join(_context.Suggestions,
                 team => team.ID,
                 suggestion => suggestion.TeamID,
                 (team, suggestion) => new { team.TeamName, suggestion.Id })
                .GroupBy(g => g.TeamName)
                .Select(s => new TeamSuggestionSummary
                {
                    TeamName = s.Key,
                    SuggestionCount = s.Count()
                }).ToList();

            var returnValue = new List<object>();
            returnValue.Add(data.Select(s => s.TeamName).ToList());
            returnValue.Add(data.Select(s => s.SuggestionCount).ToList());

            return returnValue;
        }

             
    }
}


