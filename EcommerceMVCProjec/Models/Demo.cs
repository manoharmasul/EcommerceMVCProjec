namespace EcommerceProject.Models
{
    public class Demo
    {
        public int Id { get; set; }
        public string DemoName { get; set; }
        public double Price { get; set; }
        public List<Imagesclass> Images { get; set; }
    }
    public class Imagesclass
    {
        public string ImageUrsl { get; set; }
    }
}
