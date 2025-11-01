using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRental.Repositories.QuanNH.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
    }

    public class CheckOutQuanNhSearchRequest : SearchRequest
    {
        public string? note { get; set; }
        public decimal? cost { get; set; }
        public string? name { get; set; }
    }
}
