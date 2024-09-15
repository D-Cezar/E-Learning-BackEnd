using E_Learning.DB;
using E_Learning.DB.Models;
using E_Learning.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository
{
    public interface IUserRepository : IRepository<Users>
    {
        public Users GetUsersByName(string username);

        public Task<Users> Authenticate(string email);

        public Task<List<Users>> GetActiveUsers(int courseId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext _myDBContext;

        public UserRepository(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

        public Task Add(Users entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Users entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Users>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Users> GetById(int id)
        {
            return await _myDBContext.Users.FindAsync(id);
        }

        public async Task<Users> Authenticate(string email)
        {
            return await _myDBContext.Users.FirstAsync(u => u.Email == email);
        }

        public Users GetUsersByName(string username)
        {
            throw new NotImplementedException();
        }

        public Task Update(Users entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Users>> GetActiveUsers(int courseId)
        {
            return await _myDBContext.Users
                .Where(u => u.CoursesUsers.Any(cu => cu.CourseId == courseId && cu.EnrollDate != null))
                .Include(u => u.CoursesUsers.Where(cu => cu.CourseId == courseId))
                .Include(u => u.UserAnswers.Where(ua => ua.Question.SectionCourse.CourseId == courseId))
                .ThenInclude(ua => ua.Question).ThenInclude(q => q.SectionCourse)
                .ToListAsync();
        }
    }
}