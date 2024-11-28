using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Repositories.Course
{
    public class CourseQuestionAnswerRepository : EfRepository<CourseQuestionAnswer, int>, ICourseQuestionAnswerRepository
    {
        public CourseQuestionAnswerRepository(DbContext context) : base(context)
        {
        }
    }
}
