using Application.Interfaces.Persistence;
using Application.Interfaces.Persistence.Auths;
using Application.Interfaces.Persistence.Billings;
using Application.Interfaces.Persistence.Products;
using Application.Interfaces.Persistence.SalesOrders;
using Application.Interfaces.Wrappers;
using Domain.Entities;
using Infrastructure.Repositories.Auths;
using Infrastructure.Repositories.Billings;
using Infrastructure.Repositories.Products;
using Infrastructure.Repositories.SalesOrders;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Wrappers
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private IAuthRepository _auth;
        private IRoleRepository _role;
        private IUserRoleRepository _userRole;
        private IProductRepository _product;
        private ISalesOrderRepository _salesOrder;
        private IBillingRepository _billing;

        public RepositoryWrapper(IApplicationContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public IAuthRepository Auth
        {
            get
            {
                _auth ??= new AuthRepository(_context, _userManager, _configuration);
                return _auth;
            }
        }

        public IRoleRepository Role
        {
            get
            {
                _role ??= new RoleRepository(_context);
                return _role;
            }
        }

        public IUserRoleRepository UserRoles
        {
            get
            {
                _userRole ??= new UserRoleRepository(_context);
                return _userRole;
            }
        }

        public IProductRepository Product
        {
            get
            {
                _product ??= new ProductRepository(_context);
                return _product;
            }
        }

        public ISalesOrderRepository SalesOrder
        {
            get
            {
                _salesOrder ??= new SalesOrderRepository(_context);
                return _salesOrder;
            }
        }

        public IBillingRepository Billing
        {
            get
            {
                _billing ??= new BillingRepository(_context);
                return _billing;
            }
        }

        public void Save() => _context.SaveChanges();

        public async Task SaveAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
    }
}
