namespace SignalR.Server.Hubs
{
    public class Params
    {
        public const string CorsPolicyName = "Cors";

        public static string[] Origins = { "http://localhost:2023", "https://localhost:2023" };

        public static string[] Methods= { "GET", "POST" };
    }
}
