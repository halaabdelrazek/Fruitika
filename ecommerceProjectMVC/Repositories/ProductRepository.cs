using ecommerceProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
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
        
        public List<Product> GetAllIncludeCategory()
        {
            return db.Products.Include("Category").ToList();
        }

        public Product getById(int id)
        {
            return db.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public List<Product>getByCategoryId(int id)
        {
            return db.Products.Where(p=>p.CategoryId == id).ToList();
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
                old.ProductName = newProduct.ProductName;
                old.CategoryId = newProduct.CategoryId;
                old.Description = newProduct.Description;
                old.Image = newProduct.Image;
                old.Price = newProduct.Price;
                return db.SaveChanges();
            }

            int raw = db.SaveChanges();
            return raw;
        }

    }
}
