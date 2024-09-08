using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HienTangToc.Models
{
    public class NguoimuonModel
    {
        [Key]
        public int Idnm { get; set; }

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
        public DateTime Ngaysinh { get; set; }
        [Required]
        public String Nguyennhan { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Ngaydangky { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Ngaytra { get; set; }

        public ICollection<MSalonModel> MSalonModels { get; set; } = new List<MSalonModel>();
    }
}
