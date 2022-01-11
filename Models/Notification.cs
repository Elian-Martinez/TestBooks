using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBooks.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string BookName { get; set; }
        public string BookCode { get; set; }
        public string OperationType { get; set; }
    }
}
