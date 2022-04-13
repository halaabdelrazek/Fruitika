using ecommerceProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceProjectMVC.Repositories
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        ContextEntities DB;

        public ProductOrderRepository(ContextEntities _DB)
        {
            DB = _DB;
        }

        public int Delete(int ProductOrderId)
        {
            ProductOrder po=DB.ProductOrders.FirstOrDefault(pror => pror.ProductOrderId == ProductOrderId);
            DB.ProductOrders.Remove(po);
            DB.SaveChanges();
            return 1;
            
        }

        public List<ProductOrder> GetAll()
        {
            return DB.ProductOrders.Include(po=>po.Product).Include(po => po.Order).ToList();
            
        }

        public ProductOrder GetById(int ProductOrderId)
        {
            return DB.ProductOrders.FirstOrDefault(pror => pror.ProductOrderId == ProductOrderId);

        }

        public int insert(ProductOrder po)
        {
            DB.Add(po);
            DB.SaveChanges();
            return 1;
            
        }

        public int Update(int ProductOrderId,ProductOrder newpo)
        {
            ProductOrder oldpo = DB.ProductOrders.FirstOrDefault(pror => pror.ProductOrderId == ProductOrderId);
            oldpo.ProductId = newpo.ProductId;
            oldpo.OrderId = newpo.OrderId;
            oldpo.Quantity= newpo.Quantity;
            DB.SaveChanges();
            return 1;
        }
    }
}
