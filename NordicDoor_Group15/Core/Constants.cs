namespace NordicDoor_Group15.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string TeamManager = "TeamManager";
            public const string User = "User";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireUser = "RequireUser";
            public const string RequireTeamManager = "RequireTeamManager";
        }
    }
}
