using E_Learning.DB;
using E_Learning.DB.Models;
using E_Learning.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository
{
    public interface ICourseSectionRepository : IRepository<CourseSections>
    {
        public Task<bool> VerifyMatchForSectionIds(int courseId, List<int> ids);

        public Task<List<string>> GetTitlesByIds(int coureId, List<int> ids);

        public Task<List<int>> SectionsIdsByCourseId(int courseId);

        public Task<List<string>> CompletedSectionTitles(int courseId, int userId);
    }

    public class CourseSectionRepository : ICourseSectionRepository
    {
        private readonly MyDBContext _dbContext;

        public CourseSectionRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(CourseSections entity)
        {
            await _dbContext.CourseSections.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<string>> CompletedSectionTitles(int courseId, int userId)
        {
            var completedSectionsIds = await _dbContext.CoursesUsers
        .Where(cu => cu.CourseId == courseId && cu.UserId == userId).Select(cu => cu.CompletedSectionIds)
        .FirstOrDefaultAsync();

            var completedTitles = await _dbContext.CourseSections
                .Where(cs => completedSectionsIds.Contains(cs.Id))
                .Select(cs => cs.Title)
                .ToListAsync();

            return completedTitles;
        }

        public Task Delete(CourseSections entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CourseSections>> GetAll()
        {
            return await _dbContext.CourseSections.Include(q => q.Questions).ToListAsync();
        }

        public Task<CourseSections> GetById(int id)
        {
            return _dbContext.CourseSections.Include(q => q.Questions).FirstAsync(c => c.Id == id);
        }

        public async Task<List<string>> GetTitlesByIds(int coureId, List<int> ids)
        {
            return await _dbContext.CourseSections.Where(cs => ids.Contains(cs.Id) && cs.CourseId == coureId).Select(q => q.Title).ToListAsync();
        }

        public async Task<List<int>> SectionsIdsByCourseId(int courseId)
        {
            return await _dbContext.CourseSections.Where(s => s.CourseId == courseId).Select(q => q.Id).ToListAsync();
        }

        public async Task Update(CourseSections entity)
        {
            _dbContext.CourseSections.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> VerifyMatchForSectionIds(int courseId, List<int> ids)
        {
            var matchingIds = await _dbContext.CourseSections.Where(s => s.CourseId == courseId && ids.Contains(s.Id)).CountAsync();
            return ids.Count == matchingIds;
        }
    }
}