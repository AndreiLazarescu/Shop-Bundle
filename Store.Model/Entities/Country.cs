using Store.Interfaces.Entity;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public class Country : EntityBase, INamedEntity
    {
        [DataMember]
        public string Name { get; set; }
    }
}
