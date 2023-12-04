namespace Bachelor_API.Model
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string? Username { get;set; }
        public string? Password { get; set; }
        public string Role { get; set; } = "teacher";
    }
}
