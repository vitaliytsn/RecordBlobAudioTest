using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WebApplication13.Respository;

namespace WebApplication13.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            
            return View();
        }
    
        [HttpPost]
        public ActionResult Action(HttpPostedFileBase postedFile)
        {
            IBlobStorageRespository blob = new BlobStorageRespositary();
            blob.UploadBlob((HttpPostedFileBase)postedFile);
            return View("Index");
        }

      
    }
}