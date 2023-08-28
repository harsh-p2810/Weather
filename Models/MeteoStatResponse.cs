namespace Weather.Models
{
    public class MeteoStatResponse
    {
        public Meta meta { get; set; }
        public List<MeteoStatResponseDatu> data { get; set; }
    }

    public class MeteoStatResponseDatu
    {
        public string time { get; set; }
        public double temp { get; set; }
        public double dwpt { get; set; }
    }

    public class Meta
    {
        public string generated { get; set; }
    }
}
