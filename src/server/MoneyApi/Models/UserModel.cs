namespace MoneyApi.Models;

using System.ComponentModel.DataAnnotations;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string FullName { get; set; } = null!;

    public UserModel()
    {

    }

    public UserModel(Db.User user)
    {
        Id = user.Id;
        FullName = user.FullName;
    }

    public class NewUser
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;
    }

    public record UserLogin
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

