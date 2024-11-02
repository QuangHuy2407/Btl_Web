namespace FASTFOOD.Models.ViewModels
{
    public class TimKiemMonAnViewModel
    {
        public string Keyword { get; set; }
        public List<MonAn> MonAn { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
