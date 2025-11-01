using EVRental.Repositories.QuanNH.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Repositories.QuanNH
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FA25_PRN232_SE1717_G6_EVRentalContext _context;
        private readonly CheckOutQuanNhRepository _checkOutQuanNhRepository;
        private readonly ReturnConditionRepository _returnConditionRepository;
        private readonly SystemUserAccountRepository _systemUserAccountRepository;

        public UnitOfWork() => _context = new FA25_PRN232_SE1717_G6_EVRentalContext();
        public CheckOutQuanNhRepository CheckOutQuanNhRepository
        {
            get { return _checkOutQuanNhRepository ?? new CheckOutQuanNhRepository(_context); }
        }

        public ReturnConditionRepository ReturnConditionRepository
        {
            get { return _returnConditionRepository ?? new ReturnConditionRepository(_context); }
        }

        public SystemUserAccountRepository SystemUserAccountRepository
        {
            get { return _systemUserAccountRepository ?? new SystemUserAccountRepository(_context); }
        }

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
    }
}
