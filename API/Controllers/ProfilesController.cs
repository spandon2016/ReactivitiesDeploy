using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Profiles;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController 
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(
                new Details.Query{UserName = username}
                ));

        }

        //Update the ProfilesController and add an endpoint for editing the profile
        [HttpPut]
        public async Task<IActionResult> Edit(EditProfile.Command command)
        {
            
            return HandleResult(await Mediator.Send(command));

        }

        [HttpGet("{username}/activities")]
        public async Task<IActionResult> GetUserActivities(string username,
        string predicate)
        {
            return HandleResult(await Mediator.Send(new 
            ListActivities.Query
            {Username = username, Predicate = predicate}));

        }
        
    }
}