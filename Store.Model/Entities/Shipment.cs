using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public class Shipment : EntityBase
    {
        public Shipment()
        {
            Products = new List<ShipmentProduct>();
            Orders = new List<ShipmentOrder>();
        }

        [NotMapped]
        [DataMember]
        public string FirstName { get; set; }

        [NotMapped]
        [DataMember]
        public string LastName { get; set; }

        [NotMapped]
        [DataMember]
        public string Address { get; set; }

        [NotMapped]
        [DataMember]
        public string City { get; set; }

        [NotMapped]
        [DataMember]
        public string State { get; set; }

        [NotMapped]
        [DataMember]
        public string Country { get; set; }

        [NotMapped]
        [DataMember]
        public ICollection<ShipmentProduct> Products { get; set; }

        [DataMember]
        public ICollection<ShipmentOrder> Orders { get; set; }
    }

    [DataContract]
    public class ShipmentProduct
    {
        [DataMember]
        public string SKU { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"SKU: {SKU}, Quantity:{Quantity}";
        }
    }
}
