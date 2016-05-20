using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Template.BusinessLogic;
using Template.BusinessLogic.Implementation;
using Template.Data;

namespace Template.MVC5.Controllers
{
    public class ApplicationsController : Controller
    {
        // GET: Applications
        public ActionResult Index(string status, string IDNum)
        {
            var apps = new List<ClientApplication>();
            var appbusiness = new ApplicationBusiness();
            if (!String.IsNullOrEmpty(status))
            {
                if(status== "Approved")
                {
                    apps = appbusiness.getApprovedApplications();
                    ViewBag.Stat = "Approved Applications";
                }
                else if(status == "Declined")
                {
                    apps = appbusiness.getDeclinedApplications();
                    ViewBag.Stat = "Declined Applications";
                }
                else if(status == "Incomplete")
                {
                    apps = appbusiness.getIncompleteApplications();
                    ViewBag.Stat = "Incomplete Applications";
                }
                else if (status == "New")
                {
                    apps = appbusiness.getNewApplications();
                    ViewBag.Stat = "New Applications";
                }
                else
                {
                    apps = appbusiness.getAllApplications();
                    ViewBag.Stat = "All Applications";
                }
            }
            else
            {
                apps = appbusiness.getAllApplications();
                ViewBag.Stat = "All Applications";
            }
                
            if(!String.IsNullOrEmpty(IDNum))
            {
                if (appbusiness.getApplicationByIDNumber(IDNum) != null)
                {
                    apps = new List<ClientApplication>();
                    apps.Add(appbusiness.getApplicationByIDNumber(IDNum));
                    ViewBag.Stat = "Search results : ";
                }
                else
                {
                    ViewBag.NotFound = "NO Application found that matches " + IDNum + "";
                    apps = new List<ClientApplication>();
                    ViewBag.Stat = "Search results : ";
                }                                
            }

            ViewBag.Rec = apps.Count;            
            return View(apps);
        }
        public ActionResult ViewApplication(int? appID)
        {
            if(appID==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var appbusiness = new ApplicationBusiness();
            ViewBag.List = appbusiness.getBeneficiariesByAppID((int)appID);
            ViewBag.DocList = appbusiness.AttachedDocList((int)appID);
            TempData["appID"] = appID;
            return View(appbusiness.getApplication((int)appID));
        }
        public FileContentResult DisplayFile(int? id)
        {
            var appbusiness = new ApplicationBusiness();
            return File(appbusiness.doc((int)id), "application/octet-stream");
        }
        public ActionResult Approve(int? appID)
        {
            if (appID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var appbusiness = new ApplicationBusiness();
            appbusiness.ApproveApplication((int)appID);
            appbusiness.createDocuments((int)appID);  
            appbusiness.updateAppStatus((int)appID, "Approved");
            var EB = new EmailBusiness();
            EB.to = new MailAddress(appbusiness.getApplication((int)appID).emailAdress);
            EB.sub = "Application Status - Mpiti Funeral Undertakers";
            EB.body = "Hi, "+ appbusiness.getApplication((int)appID).firstName+
                " <br/>We feel gratefull to inform you that your application was succcessfully approved."
                + "<br/>Welcome to our family."
                + "<br/><br/> Regards<br/><b>Mpiti Funeral Undertakers - Communications Team</b>";
            EB.Notification();
            return RedirectToAction("Index");
        }
        public ActionResult Decline(int? appID)
        {
            if (appID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var appbusiness = new ApplicationBusiness();
            appbusiness.DeclineApplication((int)appID,"");
            appbusiness.updateAppStatus((int)appID, "Declined");
            var objRB = new RegisterBusiness();
            var EB = new EmailBusiness();
            EB.to = new MailAddress(appbusiness.getApplication((int)appID).emailAdress);
            EB.sub = "Application Status - Mpiti Funeral Undertakers";
            EB.body = "Hi, " + appbusiness.getApplication((int)appID).firstName +
                " <br/>We regret to inform you that your application for funeral policy was unsucccessful."
                + "<br/>We look forward to serve people."
                + "<br/><br/> Regards<br/><b>Mpiti Funerals Undertakers - Communications Team</b>";
            EB.Notification();
            return RedirectToAction("Index");
        }
        public ActionResult ApplicationStatus()
        {
            var appbusiness = new ApplicationBusiness();
            return View(appbusiness.getApplicationByIDNumber(HttpContext.User.Identity.Name));
        }
    }
}