using ShelterApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ShelterApi.DTOs
{
    public class ShelterDetailDto
    {
        public int AreaId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Street { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string BuildingNumber { get; set; } = string.Empty;
        [Range(1, 10000)]
        public int Capacity { get; set; }
        [Required]
        public bool IsAccessible { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^(PublicBuilding|School|Parking|Residential|Commercial)$")]
        public string? ShelterType { get; set; }
    }
}
