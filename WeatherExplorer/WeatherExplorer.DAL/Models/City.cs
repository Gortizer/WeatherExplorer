namespace WeatherExplorer.DAL.Models
{
    public abstract class Base
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public Base()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now.ToUniversalTime();
        }
    }
    public class City : Base
    {
        public string Name { get; set; }
    }

    public class Weather : Base
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public float Temprature { get; set; }
        public byte vlazhnost { get; set; }
        public float tochkarosy { get; set; }
        public string WindDestination { get; set; }
        public byte WindSpeed { get; set; }
        public byte oblachnost { get; set; }
        public short nizhnyaagranitsaOblachnosty { get; set; }
        public byte horizontalVidimost { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }

    }

    public class WeatherCondition : Base
    {
        public string Name { get; set; }
    }
}