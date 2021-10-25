using Bits_on_chips_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bits_on_chips_application.Services
{
    public class PaymentMethodService : BaseService
    {
        public PaymentMethodService(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper)
        {
        }

        public List<PaymentMethod> GetCartItems()
        {
            return repositoryWrapper.PaymentMethod.FindAll().ToList();
        }

        public List<PaymentMethod> GetPaymentMethodByCondition(Expression<Func<PaymentMethod, bool>> expression)
        {
            List<PaymentMethod> paymentMethods = repositoryWrapper.PaymentMethod.FindByCondition(expression).ToList();
            return paymentMethods;
        }

        public PaymentMethod GetPaymentMethodById(params object[] keyValues)
        {
            PaymentMethod paymentMethod = repositoryWrapper.PaymentMethod.FindById(keyValues);
            return paymentMethod;
        }

        public void AddPaymentMethod(PaymentMethod paymentMethod)
        {
            repositoryWrapper.PaymentMethod.Create(paymentMethod);
        }

        public void UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            repositoryWrapper.PaymentMethod.Update(paymentMethod);
        }

        public void DeletePaymentMethod(PaymentMethod paymentMethod)
        {
            repositoryWrapper.PaymentMethod.Delete(paymentMethod);
        }
    }
}
