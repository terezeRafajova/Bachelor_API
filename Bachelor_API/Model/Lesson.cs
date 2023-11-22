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
        public CodeBlock[] CodeBlocks { get; set; } = new CodeBlock[0];
        public Description[] Descriptions { get; set; } = new Description[0];
        public int? NumberOfPages { get; set; }

    }
}
 