using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Domain;
using Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Core;
using AutoMapper;

namespace Application.Comments
{
    public class Create
    {
        public class Command : IRequest<Core.Result<CommentDto>>
        {
            public string Body { get; set; }
            public Guid ActivityId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Body).NotEmpty();
            }
        }

        public class Handler: IRequestHandler<Command, Result<CommentDto>>
        {

    
            private readonly DataContext _context ; 
            private readonly IUserAccessor _userAccessor;

            private readonly IMapper _mapper; 

            public Handler(DataContext context, IMapper mapper,

            IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
                _mapper = mapper ; 
                

            }
            public async Task<Result<CommentDto>> Handle(Command request, CancellationToken 
            cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.ActivityId);

                if (activity == null) return null;

                var user = await  _context.Users 
                    .Include(p => p.Photos)
                    .SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                var comment = new Comment
                {
                    Author = user,
                    Activity = activity,
                    Body = request.Body
                };
                
                activity.Comments.Add(comment);

                var success = await _context.SaveChangesAsync() > 0;
                
                if (success) return Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));

                return Result<CommentDto>.Failure("Failed to add comment");

            
// ading a list handler start on 211

            }
        }


    }

      
}