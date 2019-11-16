using System.Collections.Generic;
using DataContext.Models;

namespace Web.Models.Products
{
    public class ProductModel : Product
    {
        public List<TaskModel> NewTasks { get; set; } = new List<TaskModel>();
        public new List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }
}