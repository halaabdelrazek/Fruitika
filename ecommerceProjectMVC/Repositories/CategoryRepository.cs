using ecommerceProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceProjectMVC.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        ContextEntities context;

        public CategoryRepository(ContextEntities _context)
        {
            context = _context;
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category GetByID(int id)
        {
            return context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public int Insert(Category category)
        {
            context.Categories.Add(category);

            int raw = context.SaveChanges();
            return raw;
        }

        public int Update(int id, Category category)
        {
            Category oldCatergory = GetByID(id);
            if (oldCatergory != null)
            {
                oldCatergory.Name = category.Name;
                oldCatergory.Description = category.Description;
                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            context.Categories.Remove(GetByID(id));
            return context.SaveChanges();
        }

    }
}
