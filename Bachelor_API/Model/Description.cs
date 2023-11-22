using System.ComponentModel.DataAnnotations;

namespace Bachelor_API.Model
{
    public class Description
    {
        [Key]
        public int DescriptionId { get; set; }
        public string? Text { get; set; }
        public int? Slot { get; set; }
        public int? PageNumber { get; set; }
    }
}
