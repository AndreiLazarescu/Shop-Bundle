using Store.Interfaces.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public class City : EntityBase, INamedEntity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [ForeignKey("City_State_FK")]
        public int StateId { get; set; }

        [DataMember]
        public State State { get; set; }
    }
}
