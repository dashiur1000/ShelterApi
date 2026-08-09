using System.ComponentModel.DataAnnotations;

namespace ShelterApi.Models
{
    public class Inspection
    {
        public int Id { get; set; }
        [Required]
        public DateTime InspectionDate { get; set; }
        [Range(0, 100)]
        public int ReadinessScore { get; set; }
        [Required]
        public bool Passed { get; set; }
        [Range(0, 100)]
        public int DefectsCount { get; set; }
        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;
        public Shelter Shelter { get; set; }
    }
}
