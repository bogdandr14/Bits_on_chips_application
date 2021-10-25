using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class ShipmentMethodService : BaseService
    {
        public ShipmentMethodService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<ShipmentMethod> GetCartItems()
        {
            return repositoryWrapper.ShipmentMethod.FindAll().ToList();
        }

        public List<ShipmentMethod> GetShipmentMethodByCondition(Expression<Func<ShipmentMethod, bool>> expression)
        {
            List<ShipmentMethod> shipmentMethods = repositoryWrapper.ShipmentMethod.FindByCondition(expression).ToList();
            return shipmentMethods;
        }

        public ShipmentMethod GetShipmentMethodById(params object[] keyValues)
        {
            ShipmentMethod shipmentMethod = repositoryWrapper.ShipmentMethod.FindById(keyValues);
            return shipmentMethod;
        }

        public void AddShipmentMethod(ShipmentMethod shipmentMethod)
        {
            repositoryWrapper.ShipmentMethod.Create(shipmentMethod);
        }

        public void UpdateShipmentMethod(ShipmentMethod shipmentMethod)
        {
            repositoryWrapper.ShipmentMethod.Update(shipmentMethod);
        }

        public void DeleteShipmentMethod(ShipmentMethod shipmentMethod)
        {
            repositoryWrapper.ShipmentMethod.Delete(shipmentMethod);
        }
    }
}
