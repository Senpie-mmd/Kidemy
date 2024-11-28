namespace Kidemy.Web.Areas.Admin.Models
{
    public class UploadModel
    {
        public UploadModel()
        {
            MaxSizeInKB = long.MaxValue;
            SaveLocation = "/uploads";
        }

        public int Index { get; set; }

        public required string InputName { get; set; }

        public UploadFileType? FileType { get; set; }

        public string SaveLocation { get; set; }

        public long MaxSizeInKB { get; set; }

        public bool IsMultiple { get; set; }

        public List<string> Files { get; set; }
    }

    public enum UploadFileType
    {
        All,
        Video,
        Image,
        Audio,
        Document,
        Archive
    }
}
