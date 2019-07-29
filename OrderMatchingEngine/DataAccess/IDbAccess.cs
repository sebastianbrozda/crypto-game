using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrderMatchingEngine.Domain;

namespace OrderMatchingEngine.DataAccess
{
    interface IDbAccess
    {
        Task<Guid> Add(Order order);
        Task<List<Order>> GetAllOrdersByStatus(OrderStatus status);
        Task Update(Order order);
    }
}
