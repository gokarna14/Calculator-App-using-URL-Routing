using System.Data;

namespace Calculator
{
    public class Query
    {
        public static bool HasAlphabet(string s)
        {
            foreach (char c in s)
            {
                if (char.IsLetter(c))
                {
                    return true;
                }
            }

            return false;
        }
        public static bool Invalid(string s)
        {
            char[] valids = {'(', ')', '*', '/', '+', '-', '%'};

            foreach (char c in s)
            {
                if (!(valids.Contains(c) || char.IsNumber(c)))
                {
                    return true;
                }
            }

            return false;
        }
        public static async Task Endpoint(HttpContext context)
        {
            string? QueryString = context.Request.RouteValues["query"] as string;

            if (QueryString == null)
            {
                await context.Response.WriteAsync(Messages.InvalidMessage);
                return;
            }
            if (QueryString == "[]")
            {
                await context.Response.WriteAsync("Empty query detected !!! Please input some query !!!\n");
            }
            QueryString = QueryString.Substring(1, QueryString.Length - 2);
            if (HasAlphabet(QueryString))
            {
                await context.Response.WriteAsync("Your query is invalid because it contains alphabetic characters. Please try again !!!");
                return;
            }
            if (Invalid(QueryString))
            {
                await context.Response.WriteAsync("Your query is invalid because it contains illegal characters. Please try again !!!");
                return;
            }

            double result = Convert.ToDouble((new DataTable()).Compute(QueryString, ""));



            await context.Response.WriteAsync($"Final Result: {QueryString} = {result}");
        }
    }
}