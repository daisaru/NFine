using System.Web.Mvc;

namespace NFine.Web.Areas.BusinessManage
{
    public class BusinessManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BusinessManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "NFine.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}