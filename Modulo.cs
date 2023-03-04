namespace Calculator
{
    public class Modulo
    {
        RequestDelegate? next;

        public Modulo()
        {
            
        }
        public Modulo(RequestDelegate next)
        {
            this.next = next;
        }

        public static async Task Endpoint(HttpContext context)
        {
            int FinalResult = 0;

            if(int.TryParse(context.Request.RouteValues["FirstNumber"] as string, out int FirstNumber) && int.TryParse(context.Request.RouteValues["SecondNumber"] as string, out int SecondNumber))
            {
                FinalResult = FirstNumber%SecondNumber;
            }
            else
            {
                await context.Response.WriteAsync(Messages.InvalidMessage);
                return;
            }

            await context.Response.WriteAsync($"Final result: {FirstNumber}%{SecondNumber} = {FinalResult}\n");

        }
    }
}