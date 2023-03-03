using Calculator;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();

app.Use(async (context, next)=>{
    Endpoint? endpoint = context.GetEndpoint();

    if(endpoint != null)
    {
        await  context.Response.WriteAsync($"{endpoint.DisplayName} \n\n");
    }

    await next();
});



app.MapGet("add/{*catchcall}", Add.Endpoint)
.WithDisplayName("Add Operation");




app.MapFallback(async context => {
    await context.Response.WriteAsync($"{Messages.InvalidMessage}");
}).WithDisplayName("");


app.Run();
