namespace Calculator
{
    public class Query
    {
        public static async Task Endpoint(HttpContext context)
        {
            await context.Response.WriteAsync("Operator detected");
        } 
    }
}