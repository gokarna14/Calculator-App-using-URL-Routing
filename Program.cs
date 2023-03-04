using Calculator;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<RouteOptions>(options=>{
    options.ConstraintMap.Add("ValidOperators", typeof(ValidOperatorConstraint));
});


WebApplication app = builder.Build();

app.Use(async (context, next)=>{
    Endpoint? endpoint = context.GetEndpoint();

    if (!context.Response.HasStarted)
    {
        context.Response.ContentType = "text/plain";
    }

    if(endpoint != null)
    {
        await  context.Response.WriteAsync($"{endpoint.DisplayName} \n\n");
    }

    await next();
});



app.MapGet("add/{*catchcall}", Add.Endpoint)
.WithDisplayName("Add Operation");

app.MapGet("multiply/{*catchcall}", Multiply.Endpoint)
.WithDisplayName("Multiply operation");

app.MapGet("modulo/{FirstNumber}/{SecondNumber}", Modulo.Endpoint)
.WithDisplayName("Modulo Operation");

app.MapGet("regex(^[a-z])", ()=> "Operator detected")
.WithDisplayName("Simplification");

app.MapGet("/", () => "Please type your query in the URL\nInclude your query in square brackets.\nFor example:\t/[1+1].")
.WithDisplayName("Welcome to calculator !!");

app.MapFallback(async context => {
    await context.Response.WriteAsync($"{Messages.InvalidMessage}");
}).WithDisplayName("");


app.Run();
