using MVC5Course.Models;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ProductRepository repo = RepositoryHelper.GetProductRepository();

        //protected override void HandleUnknownAction(string actionName)
        //{
        //    //base.HandleUnknownAction(actionName);
        //    this.RedirectToAction("Index", "Home").ExecuteResult(this.ControllerContext);   // 找不到頁面時會到 Home/Index
        //}
    }

    
}