using System.ComponentModel.DataAnnotations;

namespace FINAL_TEST.Models
{
    public class TheLoai
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
