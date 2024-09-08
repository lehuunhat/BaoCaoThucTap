using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HienTangToc.Models
{
    public class MSalonModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MSalonId { get; set; }
        [Required]
        public int Idnm { get; set; }
        [Required]
        public int Idsl { get; set; }

        [ForeignKey("Idnm")]
        public NguoimuonModel NguoimuonModel { get; set; }

        [ForeignKey("Idsl")]
        public SalonModel SalonModel { get; set; }
    }
}
