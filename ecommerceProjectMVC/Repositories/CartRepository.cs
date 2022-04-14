using ecommerceProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceProjectMVC.Repositories
{
    public class CartRepository : ICartRepository
    {
        ContextEntities context;
        ProductRepository productRepository;
        public CartRepository(ContextEntities _context, ProductRepository _productRepository)
        {
            context = _context;
            productRepository = _productRepository;
        }
        public int Delete(int id)
        {
            context.Carts.Remove(GetById(id));
            return context.SaveChanges();
        }

        public List<Cart> GetAll()
        {
            return context.Carts.ToList();
        }

        public List<Cart> GetCartContent(string _applicationUserId)
        {
            return (List<Cart>)context.Carts.Where(cart => cart.ApplicationUserId == _applicationUserId);
        }

        public List<int> GetProductsIds(List<Cart> cartContent)
        {
            return cartContent.Select(item => item.ProductId).ToList();
        }

        public Cart GetById(int id)
        {
            return context.Carts.FirstOrDefault(cart => cart.CartId == id);
        }


        public List<Product> GetCartProducts(List<int> productsIds)
        {
            List<Product> cartProducts = new List<Product>();
            foreach (int productId in productsIds)
            {
                cartProducts.Add(productRepository.getById(productId));
            }
            return cartProducts;
        }

        public int Insert(Cart newCart)
        {
            newCart.Price = productRepository.getById(newCart.ProductId).Price * newCart.Quantity;
            context.Carts.Add(newCart);
            return context.SaveChanges();
        }
        public double GetCartTotalPrice(string applicationUser)
        {
            double totalPrice = 0;
            List<Product> products = GetCartProducts(GetProductsIds(GetCartContent(applicationUser)));
            foreach (Product product in products)
            {
                totalPrice += product.Price;
            }
            return totalPrice;

        }

        public int UpdateProductQuantityAndPrice(int quantity, int productId, string userId)
        {
            List<Cart> userCarts=GetCartContent(userId);
            Cart cartItem=userCarts.FirstOrDefault(cart => cart.ProductId == productId);
            cartItem.Price=(cartItem.Price/cartItem.Quantity)*quantity;
            cartItem.Quantity = quantity;
            return context.SaveChanges();
            
        }

    }
}
