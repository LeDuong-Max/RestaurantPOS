using BusinessObject;

namespace Restaurant
{
    public static class AppSession
    {
        public static Account? CurrentUser { get; set; }
    }
}
