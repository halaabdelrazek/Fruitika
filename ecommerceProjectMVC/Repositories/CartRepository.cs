using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Repositories
{
    public class CartRepository : ICartRepository
    {
        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Cart> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public List<int> GetAllProductIds(int id)
        {
            throw new System.NotImplementedException();
        }

        public Cart GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Cart GetByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetCartProducts(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(Cart newCart)
        {
            throw new System.NotImplementedException();
        }

        public int Update(int id, Cart updatedCart)
        {
            throw new System.NotImplementedException();
        }
    }
}
