using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using  WebApplication13.Models;

namespace WebApplication13.Respository
{
   public  interface IBlobStorageRespository
    {
        IEnumerable<BlobViewModel> GetBlobs();
        bool DeleteBlob(string file, string fileExtension);
        bool UploadBlob(HttpPostedFileBase blobFile);

         Task<bool> DownloadBlobAsync(string file, string fileExtension);
    } 
}
