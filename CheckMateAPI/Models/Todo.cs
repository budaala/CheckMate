namespace CheckMateAPI.Models {
    public class Todo {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}