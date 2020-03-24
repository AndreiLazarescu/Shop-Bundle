using Store.Interfaces.Entity;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public abstract class EntityBase : IEntity
    {
        [DataMember]
        public int Id { get; set; }
    }
}
