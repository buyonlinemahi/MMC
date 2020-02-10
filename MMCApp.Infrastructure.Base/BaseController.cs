using MMCApp.Domain.Models.UserModel;
using MMCApp.Infrastructure.Global;
using System.Web.Mvc;
using MMCApp.Domain.Models.CommonModel;

namespace MMCApp.Infrastructure.Base
{
    public abstract class BaseController : Controller
    {
        public User MMCUser
        {
            get { return (User)Session[GlobalConst.SessionKeys.USER]; }
            set { Session[GlobalConst.SessionKeys.USER] = value; }
        }

        public string MMCScreenMode 
        { 
            get{return (string)Session[GlobalConst.SessionKeys.MMCScreenMode]; }
            set { Session[GlobalConst.SessionKeys.MMCScreenMode] = value; } 
        }//true means full screen, false mean normal
        
    }
}
