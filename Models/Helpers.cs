public static class Helpers
{
    public static string GetUserId(HttpRequest request)
    {
        if (request.Headers.TryGetValue("gameUserId", out var values))
            return values.First();

        return "test-user";
    }
}
