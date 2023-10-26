using System.ComponentModel.DataAnnotations;

namespace Bachelor_API.Model
{
    public class UnitPlan
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Code { get; set; }
    }
}
