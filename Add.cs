using System;

namespace Calculator
{
    public class Add
    {
        RequestDelegate? next;

        public Add()
        {

        }

        public Add(RequestDelegate next)
        {
            this.next = next;
        }

        public static async Task Endpoint(HttpContext context)
        {
            string? ToAddString = context.Request.RouteValues["catchcall"] as string;

            if(ToAddString == null)
            {
                await context.Response.WriteAsync(Messages.InvalidAdd);
                return;
            }

            string FinalMessage = "";

            string[] ListOfNumbers = ToAddString.Split("/", StringSplitOptions.RemoveEmptyEntries);

            decimal FinalResult = 0m;
            bool notAnyNumber = true;

            for (int i=0; i<ListOfNumbers.Count(); i++)
            {
                if (decimal.TryParse(ListOfNumbers[i], out decimal num)){
                    FinalResult += num;
                    notAnyNumber = false;
                }
                else if(FinalMessage == "")
                {
                    FinalMessage += "Non-number input found which are ignored !!";
                }
            }

            if (notAnyNumber)
            {
                await context.Response.WriteAsync("No Number Input Found !!!");
                return;
            }

            await context.Response.WriteAsync($"Final Result = {FinalResult}\n{FinalMessage}");

        }
    }
}