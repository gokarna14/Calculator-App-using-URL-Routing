namespace Calculator
{
    public class Multiply
    {
        RequestDelegate? next;

        public Multiply(){}

        public Multiply(RequestDelegate next)
        {
            this.next = next;
        }

        public static async Task Endpoint(HttpContext context)
        {
            string? ConcernedString = context.Request.RouteValues["catchcall"] as string;

            if (ConcernedString == null)
            {
                await context.Response.WriteAsync(Messages.InvalidMessage);
                return;
            }

            string FinalMessage = "";
            string[] ListOfNumbers = ConcernedString.Split("/", StringSplitOptions.RemoveEmptyEntries);
            Decimal FinalResult = 1;

            for(int i=0; i<ListOfNumbers.Count(); i++)
            {
                if (Decimal.TryParse(ListOfNumbers[i], out decimal num))
                {
                    FinalResult *= num;
                }
                else if (FinalMessage == "")
                {
                    FinalMessage = "String detected in the input which is ignored !!\n";
                }
            }

            await context.Response.WriteAsync($"{FinalMessage}\nFinal Result = {FinalResult}");
        }
    }
}