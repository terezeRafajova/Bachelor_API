using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bachelor_API.Model
{
    public class Title
    {
        [Key]
        public int TitleId { get; set; }
        public string? Text { get; set; }
        public int? Slot { get; set; }
        public int? PageNumber { get; set; }
    }
}
