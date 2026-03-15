using DAL.Data;
using DAL.Models;
using DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
