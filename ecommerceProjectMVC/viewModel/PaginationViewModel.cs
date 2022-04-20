using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.viewModel
{
    public class PaginationViewModel
    {
        public List<Product> products { get; set; }
        public int totalProducts { get; set; }
        public int pageNumber { get; set; }

        public int pageSize { get; set; }
    }
}
