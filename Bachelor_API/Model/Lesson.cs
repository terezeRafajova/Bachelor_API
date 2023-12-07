using System.ComponentModel.DataAnnotations;

namespace Bachelor_API.Model
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }
        public string? Title { get; set; }
        public string? Username { get; set; }
        public int? SharingCode { get; set; }
        public int? SharingTime { get; set; }
        public List<CodeBlock> CodeBlocks { get; set; } = new List<CodeBlock>();
        public List<Description> Descriptions { get; set; } = new List<Description>();
        public List<Title> Titles { get; set; } = new List<Title>();
        public int? NumberOfPages { get; set; }

    }
}
 