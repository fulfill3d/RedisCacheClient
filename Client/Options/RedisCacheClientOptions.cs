namespace Client.Options
{
    public class RedisCacheClientOptions
    {
        public string Host { get; set; }
        public string Password { get;  set; }
        public bool Ssl { get;  set; }
        public bool AbortOnConnectFail { get;  set; }
        public int DefaultStringExpiryDay { get; set; }
        public int DefaultHashExpiryDay { get; set; }
    }
}