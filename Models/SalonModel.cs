using System.ComponentModel.DataAnnotations;

namespace HienTangToc.Models
{
    public class SalonModel
    {
        [Key]
        public int Idsl { get; set; }

        [Required]
        public string Ten { get; set; }

        [Required]
        public String Image { get; set; }

        [Required]
        public string Diachi { get; set; }

        [Required]
        public string Facebook { get; set; }

        [Required]
        public string Thoigianhd { get; set; }

        [Required]
        public string Uudai { get; set; }


        public ICollection<HSalonModel> HSalonModels { get; set; } = new List<HSalonModel>();
        public ICollection<MSalonModel> MSalonModels { get; set; } = new List<MSalonModel>();
    }
}
