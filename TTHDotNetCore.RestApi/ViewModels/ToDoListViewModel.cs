namespace TTHDotNetCore.RestApi.ViewModels
{
    public class ToDoListViewModel
    {
        public int Id{ get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CategoryID { get; set; }
        public int? PriorityLevel { get; set; }
        public string? Status { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? CompletedDate { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
