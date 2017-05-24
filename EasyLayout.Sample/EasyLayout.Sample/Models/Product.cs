namespace EasyLayout.Droid.Sample.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Image => "keyboard_key_" + CategoryId + ".png";
    }
}