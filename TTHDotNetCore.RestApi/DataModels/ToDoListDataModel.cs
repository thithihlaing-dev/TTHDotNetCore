namespace TTHDotNetCore.RestApi.DataModels
{
    public class ToDoListDataModel
    {
        public int TaskID { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int? CategoryID { get; set; }
        public int PriorityLevel { get; set; }
        public string Status { get; set; }
        public DateOnly DueDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? CompletedDate { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
