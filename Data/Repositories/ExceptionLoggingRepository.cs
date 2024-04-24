using Data.DatabaseObjects.Models;

namespace Data.Repositories
{
    public class ExceptionLoggingRepository
    {
        public ExceptionLoggingRepository(string connString)
        {
            _dbContext = new DatabaseContext(connString);
        }
        
        private readonly DatabaseContext _dbContext;
    }
}