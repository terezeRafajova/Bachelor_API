using System.ComponentModel.DataAnnotations;

namespace Bachelor_API.Model
{
    public class CodeBlock
    {
        [Key]
        public int CodeBlockId { get; set; }
        public int? Slot { get; set; }
        public int? PageNumber { get; set; }
        public string? JsonBlocks { get; set; }
    }
}
