using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HienTangToc.Models
{
    public class HSalonModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HSalonId { get; set; }

        [Required]
        public int Idnh { get; set; }

        [Required]
        public int Idsl { get; set; }

        [ForeignKey("Idnh")]
        public NguoihienModel NguoihienModel { get; set; }

        [ForeignKey("Idsl")]
        public SalonModel SalonModel { get; set; }
    }
}
