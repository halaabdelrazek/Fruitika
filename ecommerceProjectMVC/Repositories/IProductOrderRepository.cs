using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Repositories
{
    public interface IProductOrderRepository
    {
        List<ProductOrder> GetAll();
        ProductOrder GetById(int ProductOrderId);
        int insert(ProductOrder po);
        int Update(int ProductOrderId, ProductOrder newpo);
        int Delete(int ProductOrderId);
    }
}
