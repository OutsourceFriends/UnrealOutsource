namespace DomainService.Objects.DTOs
{
    namespace MyProject.DomainService.Objects
    {
        public class UserDto
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string PasswordHash { get; set; }
            public string FullName { get; set; }
            public string Role { get; set; }
        }
    }

}