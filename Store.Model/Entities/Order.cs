using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public class Order : EntityBase
    {
        [DataMember]
        public string SKU { get; set; }
        [DataMember]
        public int ProductId { get; set; }
        [DataMember]
        public Product Product { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public Client Client { get; set; }
        [DataMember]
        public int Quantity { get; set; }

        public ICollection<ShipmentOrder> Shipments { get; set; }
    }

    [DataContract]
    public class ShipmentOrder
    {
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public Order Order { get; set; }

        [DataMember]
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
    }
}
