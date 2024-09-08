using System.ComponentModel.DataAnnotations;

namespace HienTangToc.Models
{
    public class NguoihienModel
    {
        [Key]
        public int Idnh { get; set; }

        [Required]
        public string Hoten { get; set; }

        [Required]
        public string Sdt { get; set; }

        [Required]
        public string Diachi { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Gioitinh { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Ngayhien { get; set; }

        public ICollection<HSalonModel> HSalonModels { get; set; } = new List<HSalonModel>();
    }
}
