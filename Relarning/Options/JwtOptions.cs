namespace Relarning.Options
{
    public class JwtOptions
    {
        public string Issuere { get; set; }
        public string Audiance { get; set; }
        public int lifeTime { get; set; }
        public string SigningKey { get; set; }
    }
}
