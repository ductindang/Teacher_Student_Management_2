using AdminWeb.Helper.Middleware;
using BLL.IServices;
using BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache(); // use for session


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddHttpClient<IClassService, ClassService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddHttpClient<ITeacherService, TeacherService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddHttpClient<ICourseService, CourseService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddHttpClient<IEnrollmentService, EnrollmentService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddHttpClient<IPaymentService, PaymentService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddHttpClient<IScheduleService, ScheduleService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddHttpClient<ITeacherReviewService, TeacherReviewService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});
builder.Services.AddHttpClient<IRoleService, RoleService>(client =>
{
    client.BaseAddress = new Uri(baseUrl!);
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseSession();

app.UseMiddleware<SessionAuthMiddleware>(); // Auth middleware -> Usually after UseSession

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
