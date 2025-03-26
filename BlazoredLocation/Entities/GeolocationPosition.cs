using System.Text.Json.Serialization;

namespace BlazoredLocation.Entities
{
    public class GeolocationPosition
    {
        public long Timestamp { get; set; }
        [JsonPropertyName("Coords")]
        public GeolocationCoordinates GeolocationCoordinates { get; set; }
    }
}
