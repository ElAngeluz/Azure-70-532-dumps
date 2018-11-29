using ConnectConsoleAzure.Entities;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace ConnectConsoleAzure
{
    class Program
    {
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

        static void Main(string[] args)
        {
            CrearTable();
        }

        static void CrearTable()
        {
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("customers");
            table.CreateIfNotExists();

            table.Execute(Microsoft.WindowsAzure.Storage.Table.TableOperation.Insert(new CustomersEC("Pedro", "an@cf.cm")));
        }

        static void CrearBlob()
        {
            var blogClient = storageAccount.CreateCloudBlobClient();

            var container = blogClient.GetContainerReference("images");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            var container2 = blogClient.GetContainerReference("imagescopy");
            container2.CreateIfNotExists();

            //UpLoad(@"C:\Users\privera\Documents\GitHub\Azure-70-532-dumps\Practica Azure\Comandos-utilizados-en-el-m-dulo.docx", container.GetBlockBlobReference("test2.docx"));
            //DownLoad(@"C:\Users\privera\Documents\GitHub\Azure-70-532-dumps\Comandos-utilizados-en-el-m-dulo.docx", container.GetBlockBlobReference("test2.docx"));

            var blobOrigin = container.GetBlockBlobReference("test2.docx");
            var blobCopy = container2.GetBlockBlobReference("test3copy.docx");

            var cb = new AsyncCallback(x => Console.WriteLine("Copia Completa"));

            blobCopy.BeginStartCopy(blobOrigin.Uri, cb, null);

            Console.ReadLine();
        }

        static void UpLoad(string _path, CloudBlockBlob _blockBlob)
        {
            using (var fileStream = System.IO.File.OpenRead(_path))
            {
                _blockBlob.UploadFromStream(fileStream);
            }
        }

        static void CopiaEntreContainer()
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

            var blogClient = storageAccount.CreateCloudBlobClient();

            var container = blogClient.GetContainerReference("images");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            var container2 = blogClient.GetContainerReference("imagescopy");
            container2.CreateIfNotExists();

            var blobOrigin = container.GetBlockBlobReference("test2.docx");
            var blobCopy = container2.GetBlockBlobReference("test3copy.docx");

            var cb = new AsyncCallback(x => Console.WriteLine("Copia Completa"));

            blobCopy.BeginStartCopy(blobOrigin.Uri, cb, null);

            Console.ReadLine();
        }

        /// <summary>
        /// Descarga Archivos de un bloque
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_blockBlob"></param>
        static void DownLoad(string _path, CloudBlockBlob _blockBlob)
        {
            using (var fileStream = System.IO.File.OpenWrite(_path))
            {
                _blockBlob.DownloadToStream(fileStream);
            }
        }
    }
}
