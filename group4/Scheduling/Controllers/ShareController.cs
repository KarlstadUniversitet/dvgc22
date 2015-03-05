using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using System.Diagnostics;

namespace Scheduling.Controllers
{
    public class ShareController : Controller
    {
        //
        // GET: /Share/

        public String Index()
        {
            (Session["Categories"] as CategoryHandler).updateTime = DateTime.Now;
            (Session["Categories"] as CategoryHandler).shareId = null;
            string id = DatabaseHandler.SaveCategoryHandler(Session["Categories"] as CategoryHandler);
            (Session["Categories"] as CategoryHandler).shareId = id;
            return Request.Url.AbsoluteUri + "/"+id;
        }

        public RedirectResult Fetch(string id)
        {
            Session["Categories"]=DatabaseHandler.FetchCategoryHandler(id);
            (Session["Categories"] as CategoryHandler).DontLoad=true;
            (Session["Categories"] as CategoryHandler).shareId = id;
            return Redirect("/");
        }

        public String UpdateCategoryHandler()
        {
            CategoryHandler sessionCatHandler = Session["Categories"] as CategoryHandler;
            if (sessionCatHandler == null) { return ""; }
            if (sessionCatHandler.shareId == null) { return ""; }
            String HashId = sessionCatHandler.shareId;
            DateTime Date;
            string returnString = "";
            if(Session["OldCategories"]==null)
            {
                returnString ="first time calling, doing nothing";
            }
            else if (!((String)Session["OldCategories"]).Equals(sessionCatHandler.GetComparisonString()))
            {
                (Session["Categories"] as CategoryHandler).updateTime = DatabaseHandler.UpdateCategoryHandler(sessionCatHandler);
                sessionCatHandler.updateTime = (Session["Categories"] as CategoryHandler).updateTime;
                returnString = "local changes has been made\n" + (string)Session["OldCategories"];
            }
            else if ((Session["Categories"] as CategoryHandler).updateTime < (Date = DatabaseHandler.GetDate(HashId)))
            {
                Session["Categories"] = DatabaseHandler.FetchCategoryHandler(HashId);
                returnString = "update";
            }
            else
            {
                returnString = "no local or external changes";
            }
            Session["OldCategories"] = sessionCatHandler.GetComparisonString();  

            return returnString;
        }


    }
}
