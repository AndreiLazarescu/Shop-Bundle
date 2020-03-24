using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public class Address : EntityBase
    {
        [DataMember]
        public string Street { get; set; }

        [DataMember]
        [ForeignKey("Address_City_FK")]
        public int CityId { get; set; }

        [DataMember]
        public City City { get; set; }

        public override string ToString()
        {
            if (City != null && City.State != null && City.State.Country != null)
            {
                return $"{Street}, {City.Name}\n{City.State.Name}, {City.State.Country.Name}";
            }

            return Street;
        }
    }
}
