namespace WeatherExplorer.DAL.Models
{
    public class Base
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public Base()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now.ToUniversalTime();
        }
    }
}