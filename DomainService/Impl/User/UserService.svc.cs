using System.Collections.Generic;
using System.Linq;
using DomainService.DbContext;
using DomainService.Objects.DTOs.MyProject.DomainService.Objects;
using DomainService.Objects.Services;

namespace DomainService.Impl.User
{
    public class UserService : IUserService
    {
        public List<UserDto> GetAllUsers()
        {
            using (var ctx = new MyProjectDbContext())
            {
                return ctx.Users
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
            using (var ctx = new MyProjectDbContext())
            {
                var u = ctx.Users.Find(id);
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
            using (var ctx = new MyProjectDbContext())
            {
                if (ctx.Users.Any(x => x.UserName == user.UserName))
                    return false;
                var entity = new Adds.Entities.User
                {
                    UserName = user.UserName,
                    PasswordHash = user.PasswordHash,
                    FullName = user.FullName
                };
                ctx.Users.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool UpdateUser(UserDto user)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Users.Find(user.Id);
                if (entity == null) return false;
                entity.FullName = user.FullName;

                ctx.SaveChanges();
                return true;
            }
        }

        public bool DeleteUser(int id)
        {
            using (var ctx = new MyProjectDbContext())
            {
                var entity = ctx.Users.Find(id);
                if (entity == null) return false;
                ctx.Users.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
