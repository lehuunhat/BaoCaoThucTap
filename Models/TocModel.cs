using System.ComponentModel.DataAnnotations;

namespace HienTangToc.Models
{
    public class TocModel
    {
        [Key]
        public int Idtoc { get; set; }
        [Required]
        public string Loaitoc { get; set; }
        [Required]
        public int Soluong { get; set; }
        [Required]
        public string Dodaitoc { get; set; }

    }
}
