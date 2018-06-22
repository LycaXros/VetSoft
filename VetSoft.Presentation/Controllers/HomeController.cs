
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VetSoft.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Index method.  
        /// <summary>  
        /// Index method.   
        /// </summary>  
        /// <returns>Returns - Index view</returns>  
        public ActionResult Index()
        {
            return View();
        }
        #endregion
    }
}