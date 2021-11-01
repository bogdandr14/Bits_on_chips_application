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
        private ICaseRepository _case;
        private ICoolerRepository _cooler;
        private ICpuRepository _cpu;
        private IGpuRepository _gpu;
        private IHddRepository _hdd;
        private IMotherboardRepository _motherboard;
        private IRamRepository _ram;
        private ISourceRepository _source;
        private ISsdRepository _ssd;


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

        public ICaseRepository Case
        {
            get
            {
                if (_case == null)
                {
                    _case = new CaseRepository(_repoContext);
                }
                return _case;
            }
        }
        public ICoolerRepository Cooler
        {
            get
            {
                if (_cooler == null)
                {
                    _cooler = new CoolerRepository(_repoContext);
                }
                return _cooler;
            }
        }
        public ICpuRepository Cpu
        {
            get
            {
                if (_cpu == null)
                {
                    _cpu = new CpuRepository(_repoContext);
                }
                return _cpu;
            }
        }
        public IGpuRepository Gpu
        {
            get
            {
                if (_gpu == null)
                {
                    _gpu = new GpuRepository(_repoContext);
                }
                return _gpu;
            }
        }
        public IHddRepository Hdd
        {
            get
            {
                if (_hdd == null)
                {
                    _hdd = new HddRepository(_repoContext);
                }
                return _hdd;
            }
        }
        public IMotherboardRepository Motherboard
        {
            get
            {
                if (_motherboard == null)
                {
                    _motherboard = new MotherboardRepository(_repoContext);
                }
                return _motherboard;
            }
        }
        public IRamRepository Ram
        {
            get
            {
                if (_ram == null)
                {
                    _ram = new RamRepository(_repoContext);
                }
                return _ram;
            }
        }
        public ISourceRepository Source
        {
            get
            {
                if (_source == null)
                {
                    _source = new SourceRepository(_repoContext);
                }
                return _source;
            }
        }
        public ISsdRepository Ssd
        {
            get
            {
                if (_ssd == null)
                {
                    _ssd = new SsdRepository(_repoContext);
                }
                return _ssd;
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
