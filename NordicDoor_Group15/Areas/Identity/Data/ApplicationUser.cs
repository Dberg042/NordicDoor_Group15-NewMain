using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using NordicDoor_Group15.Models;

namespace NordicDoor_Group15.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Display(Name = "Employee")]
    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }

    [Display(Name = "Employee Number")]
    [Required(ErrorMessage = "You cannot leave Employee Number blank.")]
    [StringLength(10, ErrorMessage = "Employee Number cannot be more than 10 characters long.")]
    public string EmployeeNumber { get; set; }
    [Display(Name = "First Name")]
    [Required(ErrorMessage = "You cannot leave the first name blank.")]
    [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
    public string FirstName { get; set; }
    
    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "You cannot leave the first name blank.")]
    [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
    public string LastName { get; set; }

    [Display(Name = "Teams")]
    public ICollection<Membership> Memberships { get; set; } = new HashSet<Membership>();

    public ICollection<Suggestion> Suggestions { get; set; } = new HashSet<Suggestion>();

}

public class ApplicationRole : IdentityRole
{

}