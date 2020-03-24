using Store.Interfaces.Entity;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public class ProductCategory : EntityBase, INamedEntity
    {
        [DataMember]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
