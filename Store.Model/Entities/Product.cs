using Store.Interfaces.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public class Product : EntityBase, INamedEntity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        [ForeignKey("Product_Category_FK")]
        public int CategoryId { get; set; }

        [DataMember]
        public ProductCategory Category { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
