namespace EStore.Repositories
{
    public class IUerOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders();
    }
}
