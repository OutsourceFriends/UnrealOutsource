using System.Collections.Generic;
using System.Linq;
using MyProject.DomainService.Objects;

namespace MyProject.DomainService.Impl
{
    public class UserService : IUserService
    {
        public List<UserDto> GetAllUsers()
        {
            using (var ctx = new Myproject_dbEntities())
            {
                return ctx.users
                          .Select(u => new UserDto
                          {
                              Id = u.Id,
                              UserName = u.UserName,
                              PasswordHash = u.PasswordHash,
                              FullName = u.FullName
                          })
                          .ToList();
            }
        }

        public UserDto GetUserById(int id)
        {
            using (var ctx = new Myproject_dbEntities())
            {
                var u = ctx.users.Find(id);
                if (u == null) return null;
                return new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    PasswordHash = u.PasswordHash,
                    FullName = u.FullName
                };
            }
        }

        public bool CreateUser(UserDto user)
        {
            using (var ctx = new Myproject_dbEntities())
            {
                if (ctx.users.Any(x => x.UserName == user.UserName))
                    return false;
                var entity = new user
                {
                    UserName = user.UserName,
                    PasswordHash = user.PasswordHash,
                    FullName = user.FullName
                };
                ctx.users.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool UpdateUser(UserDto user)
        {
            using (var ctx = new Myproject_dbEntities())
            {
                var entity = ctx.users.Find(user.Id);
                if (entity == null) return false;
                entity.FullName = user.FullName;
                // По необходимости: change UserName or PasswordHash
                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteUser(int id)
        {
            using (var ctx = new Myproject_dbEntities())
            {
                var entity = ctx.users.Find(id);
                if (entity == null) return false;
                ctx.users.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
