using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Store.Model.Entities
{
    [DataContract]
    public class Client : EntityBase
    {
        [NotMapped]
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }

        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        [ForeignKey("Client_Address_FK")]
        public int AddressId { get; set; }

        [DataMember]
        public Address Address { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}
