using Kidemy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Components
{
	public class ContactUsFooterViewComponent:ViewComponent
	{

		#region Fields

		private readonly IContactUsService _contactUsService;

		#endregion

		#region Ctor
		public ContactUsFooterViewComponent(IContactUsService contactUsService)
		{
			_contactUsService = contactUsService;
		}

		#endregion

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var result = await _contactUsService.GetContactUs();

			return View("ContactUsFooter",result.Value);
		}

	}
}
