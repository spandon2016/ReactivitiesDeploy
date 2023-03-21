using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Followers;

namespace API.Controllers
{
    public class FollowController : BaseApiController
    {
        // 224 0:32
        [HttpPost("{username}")]
        public async Task<IActionResult> Follow(string username)
        {

            return HandleResult(
                await Mediator.Send(new FollowToggle.Command
                {TargetUsername = username}));
                
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetFollowings(string username, string predicate)
        {
            return HandleResult(await Mediator.Send(new List.Query{UserName = username, 
            Predicate = predicate}));
        }

    }
}