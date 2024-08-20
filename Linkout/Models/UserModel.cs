using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Linkout.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string? username { get; set; }
        public string? UNHASHEDpassword { get; set; }
    }
}