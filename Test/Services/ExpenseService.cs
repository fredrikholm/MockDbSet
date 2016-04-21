using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;
using Test.Model;

namespace Test.Services
{
    public class ExpenseService
    {
        private readonly TestContext _context;

        public ExpenseService(TestContext context)
        {
            _context = context;
        }

        public IList<Expense> GetAll()
        {
            return _context.Set<Expense>()
                .Include(p => p.Category)
                .Include("some-reference")
                .ToList();
        }

        public async Task<IList<Expense>> GetAllAsync()
        {
            return await _context.Set<Expense>()
                .Include(p => p.Category)
                .Include("some-reference")
                .ToListAsync();
        }
    }
}
