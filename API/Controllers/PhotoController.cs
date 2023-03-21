using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Photos;


namespace API.Controllers
{
    public class PhotosController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Add.Command command)
        {
           // Console.WriteLine("photo add sos1=" + command.ToString());
            return HandleResult(await Mediator.Send(command));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
         return HandleResult(await Mediator.Send(new DeletePhoto.Command{Id = id}));
         //return null;

        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(string id)
        {
            return HandleResult(await Mediator.Send(
                new SetMain.Command{Id = id}
            ));

        }

        
    }
}