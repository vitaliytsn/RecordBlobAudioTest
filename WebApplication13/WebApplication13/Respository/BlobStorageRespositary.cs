using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication13.Models;
using  Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;



namespace WebApplication13.Respository
{
    public class BlobStorageRespositary:IBlobStorageRespository
    {
        private StorageCredentials _storageCredentialsx;
        private CloudStorageAccount _cloudStorageAccountx;
        private CloudBlobClient _cloudBlobClientx;
        private CloudBlobContainer _cloudBlobContainerx;

        private string containerNamex = "mycontainer";
        private string downloadPath = @"C:\AZ\";

        public BlobStorageRespositary()
        {
            string accountNamex = "audioblob";
            string keyx = "Bsm8og/cnnbcl0UCaPL3EPlFT66wKgkrvkjn0BSe2UDdgU54JIR1etL2x3edcmngPV8ctkVHoRKTulDu+5+WxQ==";
            _storageCredentialsx= new StorageCredentials(accountNamex,keyx);
            _cloudStorageAccountx= new CloudStorageAccount(
                _storageCredentialsx,true);
            _cloudBlobClientx = _cloudStorageAccountx.CreateCloudBlobClient();
            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);

        }
        public IEnumerable<BlobViewModel> GetBlobs()
        {
            var context = _cloudBlobContainerx.ListBlobs().ToList();
            IEnumerable<BlobViewModel> VM = context.Select(x => new BlobViewModel
            {
                ContainerName = x.Container.Name,
                StorageUri =x.StorageUri.PrimaryUri.ToString(),
                PrimaryUri =x.StorageUri.PrimaryUri.ToString(),
                ActualFileName = x.Uri.AbsoluteUri.Substring(x.Uri.AbsoluteUri.LastIndexOf("/")+1),
                fileExtention = System.IO.Path.GetExtension(x.Uri.AbsoluteUri.Substring(x.Uri.AbsoluteUri.LastIndexOf("/")+1))
            }).ToList();
            return VM;
        }

        public bool DeleteBlob(string file, string fileExtension)
        {
            _cloudBlobContainerx=_cloudBlobClientx.GetContainerReference(containerNamex);
             CloudBlockBlob blockBlob   = _cloudBlobContainerx.GetBlockBlobReference(file + "." + fileExtension);
            bool deleted = blockBlob.DeleteIfExists();
            return deleted;
        }

        public bool UploadBlob(HttpPostedFileBase blobFile)
        {
            if (blobFile == null)
            {
                return false;
            }

            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);
            CloudBlockBlob blockBlob = _cloudBlobContainerx.GetBlockBlobReference(blobFile.FileName);

            using (var filestream = (blobFile.InputStream))
            {
                blockBlob.UploadFromStream(filestream);
            }

            return true;
        }

        public async Task<bool> DownloadBlobAsync(string file, string fileExtension)
        {
            
            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);
            CloudBlockBlob blockBlob = _cloudBlobContainerx.GetBlockBlobReference(file  + fileExtension);

            using (var fileStream = System.IO.File.OpenWrite(downloadPath + file + fileExtension))
            {
                await blockBlob.DownloadToStreamAsync(fileStream);
                return true;
            }
        }
    }
}