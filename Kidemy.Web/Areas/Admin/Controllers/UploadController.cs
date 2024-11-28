using Kidemy.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text;

namespace Kidemy.Web.Areas.Admin.Controllers
{
    [Authorize]
    public partial class UploadController : BaseController
    {
        #region Models

        public class ChunkMetaData
        {
            public string UploadUid { get; set; }
            public string FileName { get; set; }
            public string RelativePath { get; set; }
            public string ContentType { get; set; }
            public long ChunkIndex { get; set; }
            public long TotalChunks { get; set; }
            public long TotalFileSize { get; set; }
        }

        public class FileResult
        {
            public bool uploaded { get; set; }
            public string fileUid { get; set; }
        }

        #endregion

        public static ConcurrentDictionary<string, DateTime> PendingFileNames = new ConcurrentDictionary<string, DateTime>();

        private void AppendToFile(string fullPath, IFormFile content)
        {
            try
            {
                var fileMode = System.IO.File.Exists(fullPath) ? FileMode.Append : FileMode.Create;
                using (FileStream stream = new FileStream(fullPath, fileMode, FileAccess.Write, FileShare.ReadWrite))
                {
                    content.CopyTo(stream);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        [HttpPost("chunk-save-file/{saveLocation}")]
        [ValidateAntiForgeryToken]
        public ActionResult Chunk_Upload_Save(IEnumerable<IFormFile> files, string metaData, string saveLocation)
        {
            if (metaData == null)
            {
                return Content("Meta data is required.");
            }

            foreach (var item in PendingFileNames)
            {
                if (DateTime.UtcNow - item.Value > TimeSpan.FromMinutes(1))
                {
                    PendingFileNames.TryRemove(item);
                }
            }

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(metaData));

            JsonSerializer serializer = new JsonSerializer();
            ChunkMetaData chunkData;
            using (StreamReader streamReader = new StreamReader(ms))
            {
                chunkData = (ChunkMetaData)serializer.Deserialize(streamReader, typeof(ChunkMetaData));
            }

            string path = string.Empty;
            // The Name of the Upload component is "files"
            if (files != null)
            {
                saveLocation = Encoding.UTF8.GetString(Convert.FromBase64String(saveLocation));
                path = Directory.GetCurrentDirectory() + "/wwwroot" + saveLocation;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fileName = chunkData.UploadUid + Path.GetExtension(chunkData.FileName);

                if (chunkData.ChunkIndex == 0)
                {
                    var isSuccess = PendingFileNames.TryAdd(fileName, DateTime.UtcNow);
                    if (!isSuccess) throw new Exception("a error happened");
                }

                lock (PendingFileNames.FirstOrDefault(f => f.Key == fileName).Key)
                {
                    foreach (var file in files)
                    {
                        AppendToFile(Path.Combine(path, fileName), file);
                    }
                }

                if (chunkData.ChunkIndex == chunkData.TotalChunks - 1)
                {
                    var isSuccess = PendingFileNames.TryRemove(PendingFileNames.FirstOrDefault(f => f.Key == fileName));
                    if (!isSuccess) throw new Exception("a error happened");
                }
            }

            FileResult fileBlob = new FileResult();
            fileBlob.uploaded = chunkData.TotalChunks - 1 <= chunkData.ChunkIndex;
            fileBlob.fileUid = chunkData.UploadUid;

            return Json(fileBlob);
        }

        [HttpPost("remove-file/{saveLocation}")]
        [ValidateAntiForgeryToken]
        public ActionResult Chunk_Upload_Remove(string saveLocation, [FromForm] string fileName, [FromForm] string fileNames)
        {
            if (fileName != null)
            {
                saveLocation = Encoding.UTF8.GetString(Convert.FromBase64String(saveLocation));

                var physicalPath = Directory.GetCurrentDirectory() + "/wwwroot" + saveLocation + "/" + fileName;

                lock (PendingFileNames.FirstOrDefault(f => f.Key == fileName).Key ?? "")
                {
                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }

                var pendingFileName = PendingFileNames.FirstOrDefault(f => f.Key == fileName).Key;
                if (!string.IsNullOrWhiteSpace(pendingFileName))
                {
                    var isSuccess = PendingFileNames.TryRemove(PendingFileNames.FirstOrDefault(f => f.Key == pendingFileName));
                    if (!isSuccess) throw new Exception("a error happened");
                }
            }

            if (fileNames != null)
            {
                saveLocation = Encoding.UTF8.GetString(Convert.FromBase64String(saveLocation));

                var physicalPath = Directory.GetCurrentDirectory() + "/wwwroot" + saveLocation + "/" + fileNames;

                lock (PendingFileNames.FirstOrDefault(f => f.Key == fileNames).Key ?? "")
                {
                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }

                var pendingFileName = PendingFileNames.FirstOrDefault(f => f.Key == fileNames).Key;
                if (!string.IsNullOrWhiteSpace(pendingFileName))
                {
                    var isSuccess = PendingFileNames.TryRemove(PendingFileNames.FirstOrDefault(f => f.Key == pendingFileName));
                    if (!isSuccess) throw new Exception("a error happened");
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
    }
}
