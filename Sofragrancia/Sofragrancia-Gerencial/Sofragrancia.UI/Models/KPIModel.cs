namespace Sofragrancia.UI.Models
{
    public class KpiModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
        public string ColorClass { get; set; } 
        public bool HasProgress { get; set; }
        public double ProgressPercentage { get; set; }
        public string ProgressTextLeft { get; set; }
        public string ProgressTextRight { get; set; }
        public string ChangeText { get; set; } 
        public string ChangeClass { get; set; } 
    }
}
