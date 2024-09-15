using static E_Learning.Constants;

namespace E_Learning.DTOs.Responses
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Roles Role { get; set; }

        public string Email { get; set; }
    }
}