using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Respository;
using System.Threading.Tasks;

namespace WebApplication13.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobStorageRespository repo;

        public BlobController()
        {
            repo = new BlobStorageRespositary();
        }
        // GET: Blob
        public ActionResult Index()
        {
            var blobVM = repo.GetBlobs();
            return View(blobVM);
        }
        public async Task<ActionResult> DownloadBlob(string file, string fileExtension)
        {
            bool isDownloaded = await repo.DownloadBlobAsync(file, fileExtension);
            return RedirectToAction("Index");
        }
        
        public JsonResult RemoveBlob(string file, string extension)
        {
            bool isDeleted = repo.DeleteBlob(file, extension);
            return Json(isDeleted, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UploadBlob()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UploadBlob(HttpPostedFileBase UploadFileName)
        {
            bool isUploaded = repo.UploadBlob(UploadFileName);
            if (isUploaded)
                return RedirectToAction("Index");
            return View();
        }
    }
}