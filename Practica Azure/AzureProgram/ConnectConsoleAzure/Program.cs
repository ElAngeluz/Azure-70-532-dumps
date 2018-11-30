using ConnectConsoleAzure.Entities;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ConnectConsoleAzure
{
    class Program
    {
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));

        static CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
        static CloudTable Table;

        static void Main(string[] args)
        {
            //CrearTable(_tableEntity: new CustomersEC("Pedro", "an@cf.cm"));
            //GetNameCustomerTable("an@cf.cm");
            GetNameAllCustomers();
            Console.ReadLine();
        }

        static void UsingBashTable(string _TableName = "customers")
        {
            var batch = new TableBatchOperation();

            batch.Insert(new CustomersEC("Pablo", "pablo@gtr.com"));
            batch.Insert(new CustomersEC("Anggie", "anggie@gtr.com"));
            batch.Insert(new CustomersEC("Pedro", "pedro@gtr.com"));
        }

        static void CrearTable(ITableEntity _tableEntity,string _TableName= "customers")
        {            
            Table = tableClient.GetTableReference(_TableName);
            Table.CreateIfNotExists();
            Table.Execute(TableOperation.Insert(_tableEntity));
        }

        static void GetNameCustomerTable(string _email, string _TableName = "customers", string _partitionKey ="EC")
        {
            Table = tableClient.GetTableReference(_TableName);
            var result = Table.Execute(TableOperation.Retrieve<CustomersEC>(_partitionKey,_email));
            Console.WriteLine(((CustomersEC)result.Result).Name);
        }

        static void GetNameAllCustomers (string _TableName = "customers", string _ParititionKey="EC")
        {
            var query = new TableQuery<CustomersEC>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,_ParititionKey));

            foreach (var customer in tableClient.GetTableReference(_TableName).ExecuteQuery(query))  //obtengo la referencia de la tabla y ejecuto la consulta           
                Console.WriteLine(customer.Name);
            
        }

        static void UpdateCustomer(CustomersEC _customerEc, string _TableName = "customers")
        {
            Table = tableClient.GetTableReference(_TableName);
            Table.Execute(TableOperation.Replace(_customerEc));
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
