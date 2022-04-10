using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Repositories
{
    public interface IProductRepositories
    {
        int add(Product p);
        int delete(int id);
        List<Product> get();
        Product getById(int id);
        int update(int id, Product newProduct);
    }
}