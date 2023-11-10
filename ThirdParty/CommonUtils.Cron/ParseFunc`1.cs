namespace CommonUtils.Cron
{
    public delegate bool ParseFunc<T>(string s, out T parsed);
}