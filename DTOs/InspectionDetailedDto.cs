namespace ShelterApi.DTOs
{
    public class InspectionDetailedDto
    {
        public int inspectionId { get; set; }
        public DateTime inspectionDate { get; set; }
        public int readinessScore { get; set; }
        public bool passed { get; set; }
        public string shelterName { get; set; }
        public string city {  get; set; }
        public string neighborhood {  get; set; }
    }
}
