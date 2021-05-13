using MounjangFoodTalk.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MounjangFoodTalk.Repositories
{

    public interface ICategoriesRepository
    {
        void InsertCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(int cid);

        List<Category> GetCategories();
        List<Category> GetCategoryByCategoryID(int CategoryID);
    }
    public class CategoriesRepository : ICategoriesRepository
    {

        private readonly MounjangFoodTalkDatabaseDbContext _mounjangFoodTalkDatabaseDbContext;

        public CategoriesRepository()
        {
            _mounjangFoodTalkDatabaseDbContext = new MounjangFoodTalkDatabaseDbContext();
        }



        public void InsertCategory(Category c)
        {
            _mounjangFoodTalkDatabaseDbContext.Categories.Add(c);
            _mounjangFoodTalkDatabaseDbContext.SaveChanges();
        }



        public void UpdateCategory(Category c)
        {
            Category ct = _mounjangFoodTalkDatabaseDbContext.Categories.Where(temp => temp.CategoryID == c.CategoryID).FirstOrDefault();
            if (ct != null)
            {
                ct.CategoryName = c.CategoryName;
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();

            }
        }

        public void DeleteCategory(int cid)
        {
            Category ct = _mounjangFoodTalkDatabaseDbContext.Categories.Where(temp => temp.CategoryID == cid).FirstOrDefault();

            if (ct != null)
            {
                _mounjangFoodTalkDatabaseDbContext.Categories.Remove(ct);
                _mounjangFoodTalkDatabaseDbContext.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> ct = _mounjangFoodTalkDatabaseDbContext.Categories.ToList();
            return ct;
        }

        public List<Category> GetCategoryByCategoryID(int CategoryID)
        {
            List<Category> ct = _mounjangFoodTalkDatabaseDbContext.Categories.Where(temp => temp.CategoryID == CategoryID).ToList();
            return ct;
        }



    }
}
