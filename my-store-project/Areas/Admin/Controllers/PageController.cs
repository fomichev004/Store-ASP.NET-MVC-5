using System; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVS_Store.Areas.Admin.Controllers 
{
	public class PageController : Controller 

	//Get: Admin/Dashboard 

	public ActionResult Index()
	{
		return View();
	}
}