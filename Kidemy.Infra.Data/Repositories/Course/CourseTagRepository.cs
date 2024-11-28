using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseTagRepository : EfRepository<CourseTag, int>, ICourseTagRepository
    {
        public CourseTagRepository(KidemyContext context) : base(context)
        {
        }

        public async Task<List<int>> GetIdsByTitles(List<string> tags)
        {
            return await _dbSet.Where(n => tags.Contains(n.Title)).Select(n => n.Id).ToListAsync();
        }

        public async Task<int> GetTagIdByTitle(string title)
        {
            return await _dbSet.Where(n => n.Title == title).Select(n => n.Id).FirstOrDefaultAsync();
        }

        public async Task<List<CourseTag>> GetTagsThatInsertedInDB(List<string> tagTitles)
        {
            return await _dbSet.Where(n => tagTitles.Contains(n.Title)).ToListAsync();
        }
    }
}
