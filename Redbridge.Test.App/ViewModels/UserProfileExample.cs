using System;
using Redbridge.SDK;

namespace TesterApp
{
    public class UserProfileExample
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
