using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Repositories
{
    public interface ICartRepository
    {
        int Delete(int id);
        List<Cart> GetAll();
        Cart GetById(int id);
        int Insert(Cart newCart);
        int Update(int id, Cart updatedCart);

        Cart GetByUserId(string userId);
        List<Product> GetCartProducts(int id);
        List<int> GetAllProductIds(int id);
    }
}
