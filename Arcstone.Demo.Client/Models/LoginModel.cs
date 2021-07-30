using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Client.Models
{
    public class LoginModel
    {
        [Required] public string UserName { set; get; }

        [Required] public string Password { set; get; }
    }
}