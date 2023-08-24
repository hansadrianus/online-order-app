using Application.Interfaces.Persistence.Auths;
using Application.Interfaces.Persistence.Billings;
using Application.Interfaces.Persistence.Products;
using Application.Interfaces.Persistence.SalesOrders;

namespace Application.Interfaces.Wrappers
{
    public interface IRepositoryWrapper
    {
        IAuthRepository Auth { get; }
        IRoleRepository Role { get; }
        IUserRoleRepository UserRoles { get; }
        IProductRepository Product { get; }
        ISalesOrderRepository SalesOrder { get; }
        IBillingRepository Billing { get; }

        void Save();
        Task SaveAsync(CancellationToken cancellationToken);
    }
}