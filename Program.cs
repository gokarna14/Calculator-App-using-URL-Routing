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


app.MapGet("{query:regex(^\\[.*\\]$)}", Query.Endpoint)
.WithDisplayName("Simplification");

app.MapGet("/", () => "Please type your query in the URL\nInclude your query in square brackets.\nFor example:\t/[1+1].")
.WithDisplayName("Welcome to calculator !!");

app.MapFallback(async context => {
    await context.Response.WriteAsync($"{Messages.InvalidMessage}");
}).WithDisplayName("");


app.Run();



// abc…	Letters
// 123…	Digits
// \d	Any Digit
// \D	Any Non-digit character
// .	Any Character
// \.	Period
// [abc]	Only a, b, or c
// [^abc]	Not a, b, nor c
// [a-z]	Characters a to z
// [0-9]	Numbers 0 to 9
// \w	Any Alphanumeric character
// \W	Any Non-alphanumeric character
// {m}	m Repetitions
// {m,n}	m to n Repetitions
// *	Zero or more repetitions
// +	One or more repetitions
// ?	Optional character
// \s	Any Whitespace
// \S	Any Non-whitespace character
// ^…$	Starts and ends
// (…)	Capture Group
// (a(bc))	Capture Sub-group
// (.*)	Capture all
// (abc|def)	Matches abc or def