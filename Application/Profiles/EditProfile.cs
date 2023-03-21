using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Domain;
using Persistence;
using AutoMapper;
using FluentValidation;
using Application.Core;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Profiles
{
    public class EditProfile
    {
          public class Command: IRequest<Result<Unit>>
        {
            // put the stuff you want to change
            public string DisplayName { get; set; }
            public string Bio { get; set; }


        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAcessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAcessor = userAccessor;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, 
            CancellationToken cancellationToken)
            {
                var user =await _context.Users.FirstOrDefaultAsync(
                    x => x.UserName == _userAcessor.GetUsername());

                    user.Bio = request.Bio ?? user.Bio;
                    user.DisplayName = request.DisplayName ?? user.DisplayName;

                    _context.Entry(user).State = EntityState.Modified;

                    var success = await _context.SaveChangesAsync() > 0;

                    if (success) return Result<Unit>.Success(Unit.Value);

                    return Result<Unit>.Failure("Problem updating Profile");

    
            }

        }
    }

}