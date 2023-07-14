using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Use(async (context, next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        await context.Response.WriteAsync($"EndPoint:{endpoint.DisplayName}\n");
        await next(context);
    }
});
//enable routing 
app.UseRouting();
//create end points
app.Use(async(context, next)=>{
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    if(endpoint!= null)
    {
     await context.Response.WriteAsync($"EndPoint:{endpoint.DisplayName}\n");
        await next(context);
    }
});
app.UseEndpoints(endpoints =>
{
    //add your end points 
    endpoints.MapGet("map1", async (context) => {
        await context.Response.WriteAsync("In Map 1");
    });
    endpoints.MapPost("map2", async (context) => {
        await context.Response.WriteAsync("In Map 2");
    });
});
app.Run(async (context) => {
    await context.Response.WriteAsync($"Request received at{context.Request.Path}");
});
app.MapGet("/", () => "Hello World!");

app.Run();
