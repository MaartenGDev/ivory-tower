using System.Collections.Generic;
using DataContext.Models;

namespace Web.Models.Products
{
    public class ManageProductModel
    {
        public Product Product { get; set; }
        public List<TaskStatus> TaskStatuses { get; set; }
    }
}