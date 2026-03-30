var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<webbir.Data.JsonStore>();
builder.Services.AddScoped<webbir.Interfaces.IExerciseService, webbir.Services.ExerciseService>();
builder.Services.AddSingleton<webbir.Interfaces.IBlogService, webbir.Services.BlogService>();
builder.Services.AddSingleton<webbir.Interfaces.IAuthService, webbir.Services.AuthService>();
builder.Services.AddSingleton<webbir.Interfaces.ICommentService, webbir.Services.CommentService>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
