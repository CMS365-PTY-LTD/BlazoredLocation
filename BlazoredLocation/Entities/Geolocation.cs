namespace BlazoredLocation.Entities
{
    public class Geolocation
    {
        public LocationErrorsEnum Code { get; set; }
        public string Message { get; set; }
        public GeolocationPosition GeolocationPosition { get; set; }
    }
}
