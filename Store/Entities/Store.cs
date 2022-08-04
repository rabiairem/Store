using System.ComponentModel.DataAnnotations;

namespace StoreServiceAPI.Entities
{
    public class Store
    {
        [Required]
        [Range(1, 500)]
        public string Name { get; set; }

        [Key]
        public int SapNumber { get; set; }

        public string Abbreviation { get; set; }

        public int SmsStoreNumber { get; set; }

        public bool IsFranchise { get; set; }

        public string PostalCode { get; set; }

        public string Province { get; set; }
        public int FlowersModule { get; set; }

        public bool IsOpenOnSunday { get; set; }
    }
}
