namespace ShelterApi.DTOs
{
    public class FailedInspectionDto
    {
        public int inspectionId {  get; set; }
        public DateTime inspectionDate { get; set; }
        public int readinessScore { get; set; }
        public int defectsCount { get; set; }
        public string shelterName { get; set; }
        public string city {  get; set; }

    }
}
