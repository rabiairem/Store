using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreServiceAPI.Entities
{
    public class Store
    {
        [Required]
        [Range(1, 500)]
        public string Name { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SapNumber_id { get; set; }

        public string Abbreviation { get; set; }

        public int SmsStoreNumber { get; set; }

        public bool IsFranchise { get; set; }

        public string PostalCode { get; set; }

        public string Province { get; set; }
        public int FlowersModule { get; set; }

        public bool IsOpenOnSunday { get; set; }
    }
}
