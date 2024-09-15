using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static E_Learning.Constants;

namespace E_Learning.DB.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [Column("password", TypeName = "nvarchar(255)")]
        public string Password { set; get; }

        [Column("first_name", TypeName = "varchar(50)")]
        public string FirstName { set; get; }

        [Column("last_name", TypeName = "varchar(50)")]
        public string LastName { set; get; }

        [Column("email", TypeName = "varchar(50)")]
        public string Email { set; get; }

        [Required]
        [EnumDataType(typeof(Constants.Roles), ErrorMessage = "Invalid role")]
        public Roles Role { get; set; }

        public List<CoursesUsers> CoursesUsers { get; set; }

        public List<UserAnswers> UserAnswers { get; set; }
    }
}