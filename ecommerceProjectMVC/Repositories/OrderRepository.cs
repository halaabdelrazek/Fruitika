using ecommerceProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace ecommerceProjectMVC.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        ContextEntities context;

        public OrderRepository(ContextEntities _context)
        {
            context = _context;
        }

        public List<Order> GetAll()
        {
            return context.Orders.ToList();
        }

        public Order GetByID(int id)
        {
            return context.Orders.FirstOrDefault(o => o.OrderId == id);

        }

        public int Insert(Order ord)
        {
            context.Orders.Add(ord);

            int raw = context.SaveChanges();
            return raw;
        }

        public int Update(int id, Order ord)
        {
            Order oldOrder = GetByID(id);
            if (oldOrder != null)
            {
                oldOrder.Address = ord.Address;
                oldOrder.TotalPrice = ord.TotalPrice;
                oldOrder.Status = ord.Status;
                oldOrder.Date = ord.Date;
                oldOrder.ApplicationUserId = ord.ApplicationUserId;
                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            context.Orders.Remove(GetByID(id));
            return context.SaveChanges();
        }
        public List<int> GetOrderIdsByUser(string userId)
		{
            return context.Orders.Where(o => o.ApplicationUserId == userId).Select(o => o.OrderId).ToList();
        }
        public List<Order>GetOrdersByUser(string userId)
		{
            return context.Orders.Where(o => o.ApplicationUserId == userId).ToList();
        }


    }
}
