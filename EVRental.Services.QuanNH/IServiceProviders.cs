using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Services.QuanNH
{
    public interface IServiceProviders
    {
        SystemUserAccountService SystemUserAccountService { get; }
        ICheckOutQuanNhService ICheckOutQuanNhService { get; }
        ReturnConditionService ReturnConditionService { get; }
    }
}
