using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.DTOs.Course
{
    public class PopularCourseCategoriesModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<CoursesForPopularCategoriesModel> PopularCourses { get; set; }
    }
}
