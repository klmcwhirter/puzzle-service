using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace puzzles.Models
{
    [DataContract]
    public class Topic
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
