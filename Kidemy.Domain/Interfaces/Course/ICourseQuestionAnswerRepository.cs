using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Interfaces.Course
{
    public interface ICourseQuestionAnswerRepository: IRepository<CourseQuestionAnswer,int>
    {
    }
}
