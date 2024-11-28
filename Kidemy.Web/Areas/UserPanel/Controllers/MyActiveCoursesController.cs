using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class MyActiveCoursesController : BaseUserPanelController
    {

        [HttpGet("/userpanel/my-active-courses")]
        public async Task<IActionResult> MyActiveCourseList()
        {

            return View();
        }
    }
}
