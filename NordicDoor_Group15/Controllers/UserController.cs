using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NordicDoor_Group15.Areas.Identity.Data;
using NordicDoor_Group15.Controllers;
using NordicDoor_Group15.Core;
using NordicDoor_Group15.Core.Repositories;
using NordicDoor_Group15.Core.ViewModels;
using NordicDoor_Group15.Data;
using NordicDoor_Group15.Models;
using NordicDoor_Group15.Repositories;
using NordicDoor_Group15.ViewModels;
using SkiaSharp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace NordicDoor_Group15.Controllers
{
    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationIdentityDbContext _context;


        public UserController(ApplicationIdentityDbContext context, IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager, IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userStore = userStore;
            _userManager = userManager;
            _context = context;
        }
        //public string StatusMessage { get; set; }
        public IActionResult Index()
        {
            

            var users = _unitOfWork.User.GetUsers();
            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel
            {
                User = user,
                Roles = roleItems,
            
            
            };
            
            return View(vm);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel
            {
                User = user,
                Roles = roleItems,

            };
           
            return View(vm);
        }


       

        public InputModel Input { get; set; }
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 
            [StringLength(255, ErrorMessage = "Max 255 character")]
            [Required]
            [Display(Name = "User Name")]
            public string EmployeeNumber { get; set; }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index");
            }
        }
            [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            //Loop through the roles in ViewModel
            //Check if the Role is Assigned In DB
            //If Assigned -> Do Nothing
            //If Not Assigned -> Add Role

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();

            foreach (var role in data.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }


            user.EmployeeNumber = data.User.EmployeeNumber;

            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;
            user.Email = data.User.Email;
            //user.EmployeeNumber = Input.EmployeeNumber;
            await _userStore.SetUserNameAsync(user, data.User.EmployeeNumber, CancellationToken.None);
            await _userStore.SetNormalizedUserNameAsync(user, data.User.EmployeeNumber, CancellationToken.None);
            //await _userStore.SetUserNameAsync(user, Input.EmployeeNumber, CancellationToken.None);
            _unitOfWork.User.UpdateUser(user);

            data.StatusMessage = "User profile has been updated";
                    
            return RedirectToAction("Details", new { id = user.Id });
            
        }

      
    }
}


