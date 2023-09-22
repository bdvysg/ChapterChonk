using ChapterChonk.DataAccess.Data;
using ChapterChonk.DataAccess.Repository.IRepository;
using ChapterChonk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterChonk.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CompanyRepository(ApplicationDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public void Update(Company obj)
        {
            _dbContext.Update(obj);
        }
    }
}
