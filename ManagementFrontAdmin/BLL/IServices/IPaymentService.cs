using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IPaymentService
    {
        public Task<IEnumerable<Payment>> GetAllPayments();
        public Task<Payment> GetPaymentById(int id);
        public Task<Payment> InsertPayment(Payment obj);
        public Task<Payment> UpdatePayment(Payment obj);
        public Task<Payment> DeletePayment(int id);
    }
}
