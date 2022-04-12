using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Repositories
{
    public interface ICategoryRepository
    {
        int Delete(int id);
        List<Category> GetAll();
        Category GetByID(int id);
        int Insert(Category category);
        int Update(int id, Category category);
    }
}