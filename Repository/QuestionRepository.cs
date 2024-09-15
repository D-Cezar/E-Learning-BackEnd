using E_Learning.DB;
using E_Learning.DB.Models;
using E_Learning.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository
{
    public interface IQuestionRepository : IRepository<Questions>
    {
    }

    public class QuestionRepository : IQuestionRepository
    {
        private readonly MyDBContext _dbContext;

        public QuestionRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Questions entity)
        {
            await _dbContext.Questions.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public Task Delete(Questions entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteById(int id)
        {
            var question = await _dbContext.Questions.FindAsync(id);

            if (question == null)
                return false;

            _dbContext.Questions.Remove(question);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public Task<List<Questions>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Questions> GetById(int id)
        {
            return await _dbContext.Questions.FirstAsync(question => question.Id == id);
        }

        public async Task Update(Questions entity)
        {
            var existingQuestion = await _dbContext.Questions.FindAsync(entity.Id);

            if (existingQuestion != null)
            {
                _dbContext.Entry(existingQuestion).CurrentValues.SetValues(entity);

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}