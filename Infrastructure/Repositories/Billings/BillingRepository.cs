using Application.Interfaces.Persistence;
using Application.Interfaces.Persistence.Billings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Billings
{
    public class BillingRepository : RepositoryBase<Billing>, IBillingRepository
    {
        public BillingRepository(IApplicationContext context) : base(context)
        {
        }
    }
}
