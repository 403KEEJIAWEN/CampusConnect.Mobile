using CampusConnect.Mobile.Models;
using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Mobile.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MatricNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
