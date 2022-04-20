    namespace ecommerceProjectMVC.Models
{
    public class ProductNameAndImageAndQuantityAndPriceViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
