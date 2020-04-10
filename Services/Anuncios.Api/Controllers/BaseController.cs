using Microsoft.AspNetCore.Mvc;
using SharedKernel.Domain.Handlers.Shared;
using System.Threading.Tasks;

namespace Anuncios.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public BaseController()
        {
        }
        protected IActionResult Response(object obj)
        {
            return Ok(new
            {
                success = true,
                data = obj
            });

        }


        protected async Task<IActionResult> Response(object obj,
                                            Handler handler)
        {

            if (handler.Valid)
                return Ok(new
                {
                    success = true,
                    data = obj
                });
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = handler.Notifications
                });
            }

        }

        protected IActionResult InvalidModelState()
        {
            return BadRequest(new
            {
                success = false,
                errors = new object[] { new { Property = "Error", Message = "" } }
            });
        }
    }
}
