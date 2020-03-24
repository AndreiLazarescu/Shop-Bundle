using Store.Interfaces.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public class State : EntityBase, INamedEntity
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [ForeignKey("State_Contry_FK")]
        public int CountryId { get; set; }

        [DataMember]
        public Country Country { get; set; }
    }
}
