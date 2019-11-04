namespace swmc.Models
{
    public enum EmbarkationStatus
    {
        Pending,
        Embarked,
        Disembarked
    }
    public class Embarkation
    {
        public int EmbarkationId { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
        public EmbarkationStatus EmbarkationStatus { get; set; }
    }
}