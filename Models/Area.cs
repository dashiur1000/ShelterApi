using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShelterApi.Models
{
    public class Area
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Neighborhood { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string AreaCode { get; set; } = string.Empty;
        [Range(1, 5)]
        public int RiskLevel { get; set; }
        public ICollection<Shelter> Shelters { get; }
    }
}
