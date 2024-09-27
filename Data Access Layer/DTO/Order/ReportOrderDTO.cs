using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO.Order
{
    public class ReportOrderDTO
    {
        public int OrderID { get; set; }
        public string OrderStatusName { get; set; }
        public string SellerName { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string ClientGover { get; set; }
        public string ClientCity { get; set; }
        public int OrderCost { get; set; }
        public int AmountRecive { get; set; } = 0; 
        public decimal ChargeCost { get; set; }
        public decimal PaidCharge { get; set; } = 0; 
        public decimal? CompanyAmount { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
