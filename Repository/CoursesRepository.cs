using E_Learning.DB;
using E_Learning.DB.Models;
using E_Learning.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository
{
    public interface ICoursesRepository : IRepository<Courses>
    {
        public Task<List<Courses>> GetCoursesByTitle(string coursetitle);

        public Task<List<Courses>> GetUserCoursesList(int? userId);

        public Task<Courses> GetStudentCourse(int courseId, int studentId);

        public Task<Courses> GetAuthorCourse(int courseId, int authorId);
    }

    public class CoursesRepository : ICoursesRepository
    {
        private readonly MyDBContext _dbContext;

        public CoursesRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Courses entity)
        {
            await _dbContext.Courses.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Courses entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteById(int id)
        {
            var @object = await _dbContext.Courses.FirstAsync(x => x.Id == id);
            if (@object == null)
                return false;
            _dbContext.Courses.Remove(@object);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Courses>> GetAll()
        {
            return await _dbContext.Courses
                .Include(x => x.Author)
                .Include(x => x.CoursesUsers)
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Questions)
                        .ThenInclude(q => q.UserAnswers)
                .ToListAsync();
        }

        public async Task<Courses> GetAuthorCourse(int courseId, int authorId)
        {
            return await _dbContext.Courses
               .Include(x => x.CoursesUsers)
               .Include(c => c.Sections)
               .ThenInclude(s => s.Questions).ThenInclude(q => q.UserAnswers)
               .FirstAsync(c => c.Id == courseId);
        }

        public async Task<Courses> GetById(int id)
        {
            return await _dbContext.Courses
                .Include(c => c.Author)
                .Include(c => c.Sections).ThenInclude(s => s.Questions).ThenInclude(q => q.UserAnswers)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<List<Courses>> GetCoursesByTitle(string coursetitle)
        {
            return await _dbContext.Courses.Where(x => x.Title.Contains(coursetitle)).ToListAsync();
        }

        public async Task<Courses> GetStudentCourse(int courseId, int studentId)
        {
            return await _dbContext.Courses
                .Include(c => c.Author)
                .Include(c => c.Sections).ThenInclude(s => s.Questions)
                .Include(c => c.CoursesUsers.Where(cu => cu.UserId == studentId && cu.CourseId == courseId))
                .FirstAsync(x => x.Id == courseId);
        }

        public async Task<List<Courses>> GetUserCoursesList(int? userId)
        {
            return await _dbContext.Courses
               .Include(x => x.Author)
               .Include(x => x.CoursesUsers.Where(x => x.UserId == userId))
               .Include(c => c.Sections).ThenInclude(s => s.Questions)
               .ToListAsync();
        }

        public Task Update(Courses entity)
        {
            throw new NotImplementedException();
        }
    }
}