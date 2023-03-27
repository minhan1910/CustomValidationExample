using Microsoft.AspNetCore.Mvc;
using ModelValidationExample.CustomModelBinders;
using ModelValidationExample.Models;

namespace ModelValidationExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("register")]
        //[Bind(nameof(Person.PersonName), nameof(Person.Email), nameof(Person.Password), nameof(Person.ConfirmPassword))]

        //[ModelBinder(BinderType = typeof(PersonModelBinder))]
        public IActionResult Index([FromForm] Person person, [FromHeader(Name = "User-Agent")] string UserAgent)
        {
            if (!ModelState.IsValid)
            { 

                // shorthand
                var errors = string.Join("\n", ModelState.Values
                    .SelectMany(value => value.Errors)
                    .Select(err => err.ErrorMessage));

                //foreach (var value in ModelState.Values)
                //{
                //    foreach (var error in value.Errors)
                //    {
                //        errorsList.Add(error.ErrorMessage);
                //    }
                //}

                return BadRequest(errors);
            }

            return Content($"{person} \n {UserAgent}");
        }
    }
}
