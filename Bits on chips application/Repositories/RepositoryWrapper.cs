using Bits_on_chips_application;
using Bits_on_chips_application.Data;
using Bits_on_chips_application.Repositories.Repository_classes;
using Bits_on_chips_application.Repositories.Repository_interfaces;

namespace Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BitsOnChipsDbContext _repoContext;
        private ICategoryRepository _category;
        private IComponentRepository _component;
        private ICartItemRepository _cartItem;
        private IOrderRepository _order;
        private IApplicationUserRepository _applicationUser;
        private IWishItemRepository _wishItem;
        private IPaymentMethodRepository _paymentMethod;
        private IShipmentMethodRepository _shipmentMethod;

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_repoContext);
                }
                return _category;
            }
        }
        public IComponentRepository Component
        {
            get
            {
                if (_component == null)
                {
                    _component = new ComponentRepository(_repoContext);
                }
                return _component;
            }
        }
        public ICartItemRepository CartItem
        {
            get
            {
                if (_cartItem == null)
                {
                    _cartItem = new CartItemRepository(_repoContext);
                }
                return _cartItem;
            }
        }
        public IOrderRepository Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrderRepository(_repoContext);
                }
                return _order;
            }
        }
        public IApplicationUserRepository ApplicationUser
        {
            get
            {
                if (_applicationUser == null)
                {
                    _applicationUser = new ApplicationUserRepository(_repoContext);
                }
                return _applicationUser;
            }
        }

        public IWishItemRepository WishItem
        {
            get
            {
                if (_wishItem == null)
                {
                    _wishItem = new WishItemRepository(_repoContext);
                }
                return _wishItem;
            }
        }
        public IPaymentMethodRepository PaymentMethod
        {
            get
            {
                if (_paymentMethod == null)
                {
                    _paymentMethod = new PaymentMethodRepository(_repoContext);
                }
                return _paymentMethod;
            }
        }
        public IShipmentMethodRepository ShipmentMethod
        {
            get
            {
                if (_shipmentMethod == null)
                {
                    _shipmentMethod = new ShipmentMethodRepository(_repoContext);
                }
                return _shipmentMethod;
            }
        }
        public RepositoryWrapper(BitsOnChipsDbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
