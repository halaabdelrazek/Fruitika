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
            return context.Category.ToList();
        }

        public Category GetByID(int id)
        {
            return context.Category.FirstOrDefault(c => c.CategoryId == id);
        }

        public int Insert(Category category)
        {
            context.Category.Add(category);

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
                oldCatergory.Image = category.Image;
                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            context.Category.Remove(GetByID(id));
            return context.SaveChanges();
        }
    }
}
