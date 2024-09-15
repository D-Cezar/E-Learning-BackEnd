using E_Learning.DB;
using E_Learning.DB.Models;
using E_Learning.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_Learning.Repository
{
    public interface ICoursesUsersRepository : IRepository<CoursesUsers>
    {
        public Task<List<CoursesUsers>> GetUsersForCourse(int courseId);

        public Task<List<CoursesUsers>> GetCoursesForUser(int userId);

        public Task<CoursesUsers> GetCourseForUser(int userId, int courseId);

        public Task UpdateActiveSession(CoursesUsers entity, TimeSpan time);

        public Task UpdateCourseCompletion(CoursesUsers entity, int sectionId, bool completedCourse);

        public Task<List<decimal>> CompletedSectionsPercentages(int courseId);

        public Task<decimal> CompletedCoursePercentage(int courseId);

        public Task<TimeSpan> AverageTimeSpent(int courseId);
    }

    public class CoursesUsersRepository : ICoursesUsersRepository
    {
        private readonly MyDBContext _dbContext;

        public CoursesUsersRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(CoursesUsers entity)
        {
            await _dbContext.CoursesUsers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task Delete(CoursesUsers entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CoursesUsers>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CoursesUsers> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CoursesUsers>> GetCoursesForUser(int userId)
        {
            return await _dbContext.CoursesUsers.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<CoursesUsers> GetCourseForUser(int userId, int courseId)
        {
            return await _dbContext.CoursesUsers
                //.Include(c => c.CompletedSectionIDs)
                .FirstAsync(c => c.UserId == userId && c.CourseId == courseId);
        }

        public async Task<List<CoursesUsers>> GetUsersForCourse(int courseId)
        {
            return await _dbContext.CoursesUsers.Where(c => c.CourseId == courseId).ToListAsync();
        }

        public async Task Update(CoursesUsers entity)
        {
            if (entity != null)
            {
                _dbContext.CoursesUsers.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateActiveSession(CoursesUsers entity, TimeSpan time)
        {
            entity.ActiveTime = entity.ActiveTime + time;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCourseCompletion(CoursesUsers entity, int sectionId, bool completedCourse)
        {
            entity.CompletedSectionIds.Add(sectionId);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<decimal>> CompletedSectionsPercentages(int courseId)
        {
            var totalUsers = await _dbContext.CoursesUsers
        .Where(cu => cu.CourseId == courseId && cu.ActiveTime != null)
        .ToListAsync();

            var usersWhoHaveNotCompleted = totalUsers
                .Where(cu => cu.CompletedSectionIds.Count < _dbContext.CourseSections.Count(cs => cs.CourseId == courseId))
                .ToList();

            if (!usersWhoHaveNotCompleted.Any()) return new List<decimal>();

            var totalSections = await _dbContext.CourseSections
                .Where(cs => cs.CourseId == courseId)
                .ToListAsync();

            var sectionCompletionPercentages = new List<decimal>();

            foreach (var section in totalSections)
            {
                var usersCompletedThisSection = usersWhoHaveNotCompleted
                    .Count(cu => cu.CompletedSectionIds.Contains(section.Id));

                var sectionCompletionPercentage = (decimal)usersCompletedThisSection / usersWhoHaveNotCompleted.Count * 100;

                sectionCompletionPercentages.Add(Math.Round(sectionCompletionPercentage, 2));
            }

            return sectionCompletionPercentages;
        }

        public async Task<decimal> CompletedCoursePercentage(int courseId)
        {
            var totalUsers = await _dbContext.CoursesUsers.CountAsync(cu => cu.CourseId == courseId);
            if (totalUsers == 0) return 0;

            var totalSections = await _dbContext.CourseSections.CountAsync(cs => cs.CourseId == courseId);
            if (totalSections == 0) return 0;

            var usersCompletedCourse = await _dbContext.CoursesUsers
                .Where(cu => cu.CourseId == courseId && cu.CompletedSectionIds.Count == totalSections)
                .CountAsync();

            var completedCoursePercentage = (decimal)usersCompletedCourse / totalUsers * 100;

            return Math.Round(completedCoursePercentage, 2);
        }

        public async Task<TimeSpan> AverageTimeSpent(int courseId)
        {
            var timeSpans = await _dbContext.CoursesUsers.Where(c => c.CourseId == courseId && c.ActiveTime.HasValue)
            .Select(c => c.ActiveTime.Value)
            .ToListAsync();

            if (!timeSpans.Any())
            {
                return TimeSpan.Zero;
            }

            long averageTicks = (long)timeSpans.Average(ts => ts.Ticks);

            return new TimeSpan(averageTicks);
        }
    }
}