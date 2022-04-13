using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Repositories
{
    public interface IProductRepository
    {
        int add(Product p);
        int delete(int id);
        List<Product> get();
        List<Product> getByCategoryId(int id);
        Product getById(int id);
        int update(int id, Product newProduct);
    }
}