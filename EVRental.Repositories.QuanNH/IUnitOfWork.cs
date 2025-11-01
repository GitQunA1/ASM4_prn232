using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Repositories.QuanNH
{
    public interface IUnitOfWork
    {
        SystemUserAccountRepository SystemUserAccountRepository { get; }
        CheckOutQuanNhRepository CheckOutQuanNhRepository { get; }
        ReturnConditionRepository ReturnConditionRepository { get; }

        Task<int> SaveChangesWithTransactionAsync();
        int SaveChangesWithTransaction();
    }
}
