using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Repositories
{
    public interface ICartRepository
    {
        int Delete(int id);
        List<Cart> GetAll();
        Cart GetById(int id);
        List<Cart> GetCartContent(string _applicationUserId);
        List<Product> GetCartProducts(List<int> productsIds);
        double GetCartTotalPrice(string applicationUser);
        List<int> GetProductsIds(List<Cart> cartContent);
        int Insert(Cart newCart);
        public int UpdateProductQuantityAndPrice(int quantity, int productId, string userId);
        public int ClearOutCart(string userId);
    }
}