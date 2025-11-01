using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Services.QuanNH
{
    public class ServiceProviders : IServiceProviders
    {
        CheckOutQuanNhService _checkOutQuanNhService;
        ReturnConditionService _returnConditionService;
        SystemUserAccountService _systemUserAccountService;

        public ServiceProviders() { }
        public ICheckOutQuanNhService ICheckOutQuanNhService
        {
            get { return _checkOutQuanNhService ?? new CheckOutQuanNhService(); }
        }

        public ReturnConditionService ReturnConditionService
        {
            get { return _returnConditionService ?? new ReturnConditionService(); }
        }

        public SystemUserAccountService SystemUserAccountService
        {
            get { return _systemUserAccountService ?? new SystemUserAccountService(); }
        }
    }
}
