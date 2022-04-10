using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Repositories
{
    public interface IOrderRepository
    {
        int Delete(int id);
        List<Order> GetAll();
        Order GetByID(int id);
        int Insert(Order ord);
        int Update(int id, Order ord);
    }
}