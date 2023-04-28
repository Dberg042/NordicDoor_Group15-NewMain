using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NordicDoor_Group15.Areas.Identity.Data;
using NordicDoor_Group15.Data;
using NuGet.Configuration;

namespace NordicDoor_Group15.Models
{
    /// <summary>
    /// To seed data to database for the first time
    /// </summary>
    public static class SeedData
    {
        
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            
            ApplicationIdentityDbContext context = applicationBuilder.ApplicationServices.CreateScope()
            .ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
            {
                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(
                   new Team
                   {
                       TeamNumber = "1",
                       TeamName = "Accounting"
                   },
                   new Team
                   {
                        TeamNumber = "2",
                        TeamName = "Desing"
                   }
                   ,
                   new Team
                   {
                         TeamNumber = "3",
                         TeamName = "FrontEnd"
                   },
                   new Team
                   {
                       TeamNumber = "4",
                       TeamName = "BackEnd"
                   }

                );
                    context.SaveChanges();

                }
                // Look for any suggestion.
                if (!context.Suggestions.Any())
                {
                    context.Suggestions.AddRange(
                      new Suggestion
                      {
                          Title = "Fixing the oak doors",
                          Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec lorem ipsum, malesuada et sagittis vel, elementum ac ligula. ",
                          MainBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec lorem ipsum, malesuada et sagittis vel, elementum ac ligula. Suspendisse mollis sagittis ipsum sit amet dignissim. Maecenas ac rhoncus massa. Ut consectetur vehicula felis, ac porttitor diam feugiat vel. Morbi ac sapien convallis, bibendum nisl non, auctor elit. Etiam id porttitor turpis. In vel faucibus velit, ac pulvinar enim. Sed mattis diam quam, sit amet vestibulum odio convallis eu.",
                          TeamID = context.Teams.FirstOrDefault(d => d.TeamNumber == "1" && d.TeamName == "Accounting").ID,
                          StartDate = DateTime.Parse("1959-4-15"),
                          Status = "Do",
                          Category = "Kvalitet",
                      },
                      new Suggestion
                      {
                          Title = "Instaling the sensor activated doors",
                          Description = "Suspendisse mollis sagittis ipsum sit amet dignissim.",
                          MainBody = "Pellentesque dapibus diam eros, id ultrices lorem convallis sit amet. Proin porta eros a massa rhoncus, id ullamcorper urna hendrerit. Integer porta quam massa, et sollicitudin mauris maximus et. Nulla bibendum eu augue id malesuada. Pellentesque nisl ante, ultrices a leo ac, hendrerit elementum mauris. Vestibulum mollis ultrices efficitur. In ac quam at justo tristique rutrum. Nulla facilisi.",
                          TeamID = context.Teams.FirstOrDefault(d => d.TeamNumber == "1" && d.TeamName == "Accounting").ID,
                          StartDate = DateTime.Parse("1999-4-15"),
                          Status = "Study",
                          Category = "HMS",
                      }
                   );
                    context.SaveChanges();
                }




            }
                        
        }
    }

}
