using E_Learning.DB;
using E_Learning.DB.Models;
using E_Learning.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository
{
    public interface IUserAnswerRepository : IRepository<UserAnswers>
    {
        public Task<List<UserAnswers>> GetByCourseId(int id);

        public Task<List<UserAnswers>> GetByUserId(int id);

        public Task<List<UserAnswers>> GetUserLastAnswers(int userId, int courseId);

        public Task<decimal> CorrectAnswersPercentage(int courseId);

        public Task<bool> DeleteQuestionAnswers(int questionId);
    }

    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly MyDBContext _dbContext;

        public UserAnswerRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(UserAnswers entity)
        {
            await _dbContext.UserAnswers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<decimal> CorrectAnswersPercentage(int courseId)
        {
            var totalAnswers = await _dbContext.UserAnswers
            .Include(ua => ua.Question)
            .Where(ua => ua.Question.SectionCourse.CourseId == courseId)
            .CountAsync();

            if (totalAnswers == 0) return 0;

            var totalCorrectAnswers = await _dbContext.UserAnswers
                .Include(ua => ua.Question)
                .Where(ua => ua.Question.SectionCourse.CourseId == courseId && ua.Question.Answer == ua.GivenAnswer)
                .CountAsync();

            var correctAnswersPercentage = (decimal)totalCorrectAnswers / totalAnswers * 100;

            return Math.Round(correctAnswersPercentage, 2);
        }

        public Task Delete(UserAnswers entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteQuestionAnswers(int questionId)
        {
            var answersToDelete = await _dbContext.UserAnswers
        .Where(ua => ua.QuestionId == questionId)
        .ToListAsync();

            if (answersToDelete == null || !answersToDelete.Any())
                return false;

            _dbContext.UserAnswers.RemoveRange(answersToDelete);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public Task<List<UserAnswers>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserAnswers>> GetByCourseId(int id)
        {
            var answers = await _dbContext.Courses.Where(c => c.Id == id)
                .SelectMany(c => c.Sections)
                .SelectMany(s => s.Questions)
                .SelectMany(q => q.UserAnswers)
                .ToListAsync();
            return answers;
        }

        public Task<UserAnswers> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserAnswers>> GetByUserId(int id)
        {
            var answers = await _dbContext.UserAnswers.Where(a => a.UserId == id)
                .Include(ua => ua.Question)
                .ToListAsync();
            return answers;
        }

        public async Task<List<UserAnswers>> GetUserLastAnswers(int userId, int courseId)
        {
            return await _dbContext.UserAnswers
                .Include(ua => ua.Question)
                .Include(ua => ua.User)
                .Where(ua => ua.UserId == userId && ua.Question.SectionCourse.CourseId == courseId)
                .GroupBy(ua => ua.QuestionId)
                .Select(g => g.OrderByDescending(ua => ua.AnswerTime).FirstOrDefault())
                .ToListAsync();
        }

        public Task Update(UserAnswers entity)
        {
            throw new NotImplementedException();
        }
    }
}