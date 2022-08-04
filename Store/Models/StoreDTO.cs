using System.ComponentModel.DataAnnotations;

namespace StoreServiceAPI.Models
{
    public class StoreDTO
    {
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Store Name Is Too Long")]
        public string Name { get; set; }

        [Required]
        public int SapNumber { get; set; }

        public string Abbreviation { get; set; }

        public int SmsStoreNumber { get; set; }

        public bool IsFranchise { get; set; }

        [Required]
        public string PostalCode { get; set; }

        public string Province { get; set; }
        public int FlowersModule { get; set; }

        public bool IsOpenOnSunday { get; set; }
    }
}
