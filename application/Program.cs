using application.Components;

var builder = WebApplication.CreateBuilder(args);

//Api
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://techdocs.gbif.org/v1/") });


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    { 
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    });
        });

var app = builder.Build();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

