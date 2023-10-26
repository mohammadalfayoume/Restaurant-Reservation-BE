using RestaurantsReservation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // OR builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;

//try
//{
//    var context = services.GetRequiredService<DataBaseContext>();
//    var userManager = services.GetRequiredService<UserManager<AppUser>>();
//    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
//    await context.Database.MigrateAsync();
//    await Seed.SeedUsers(userManager, roleManager);
//}
//catch (Exception ex)
//{
//    var logger = services.GetRequiredService<ILogger<Program>>();
//    logger.LogError(ex, "An error occurred during migration");
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Added Authentication middleware
app.UseAuthentication(); // Do you have a valid token
app.UseAuthorization(); // Ok, you have a valid token, what are you allow to do?

app.MapControllers();

app.Run();
