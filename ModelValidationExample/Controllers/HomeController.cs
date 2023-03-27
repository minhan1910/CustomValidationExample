using Microsoft.AspNetCore.Mvc;
using ModelValidationExample.Models;

namespace ModelValidationExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("register")]
        public IActionResult Index(Person person)
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

            return Content($"{person}");
        }
    }
}
