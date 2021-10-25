using Bits_on_chips_application.Data;
using Bits_on_chips_application.Models;
using Bits_on_chips_application.Repositories.Repository_interfaces;
using Repositories;

namespace Bits_on_chips_application.Repositories.Repository_classes
{
    public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(BitsOnChipsDbContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
