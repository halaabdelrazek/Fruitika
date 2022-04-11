using ecommerceProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceProjectMVC.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ContextEntities db;

        public ProductRepository(ContextEntities _db)
        {
            db = _db;
        }

        public List<Product> get()
        {
            return db.Products.ToList();
        }

        public Product getById(int id)
        {
            return db.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public int add(Product p)
        {
            db.Products.Add(p);
            int raw = db.SaveChanges();
            return raw;
        }

        public int delete(int id)
        {
            db.Products.Remove(getById(id));
            int raw = db.SaveChanges();
            return raw;
        }

        public int update(int id, Product newProduct)
        {
            Product old = getById(id);
            if (old != null)
            {
                newProduct.ProductName = old.ProductName;
                newProduct.ProductOrders = old.ProductOrders;
                newProduct.Category = old.Category;
                newProduct.Carts = old.Carts;
                newProduct.CategoryId = old.CategoryId;
                newProduct.Description = old.Description;
                newProduct.Image = old.Image;
                newProduct.Price = old.Price;
                return db.SaveChanges();
            }

            int raw = db.SaveChanges();
            return raw;
        }

    }
}
