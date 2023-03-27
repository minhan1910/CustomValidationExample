using ModelValidationExample.CustomModelBinders;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers(options =>
//{
//    // should be the first Default Binder Provider
//    //options.ModelBinderProviders.Insert(0, new PersonBinderProvider());


//});
builder.Services.AddControllers().AddXmlSerializerFormatters();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
