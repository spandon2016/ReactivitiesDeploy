using MediatR;
using Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Comments
{
    public class List
    {
        public class Query: IRequest<Result<List<CommentDto>>>
        {

            public Guid ActivityId { get; set; }

            public class Handler : IRequestHandler<Query, Result<List<CommentDto>>>
            {

                IMapper _mapper; 
                DataContext _context; 
                public Handler(DataContext context, IMapper mapper)
                {
                   _mapper = mapper;
                   _context = context;

                }

                public async Task<Result<List<CommentDto>>> Handle(Query request, CancellationToken cancellationToken)
                {

                    var comments = await _context.Comments
                        .Where(x => x.Activity.Id == request.ActivityId)
                        .OrderByDescending(x => x.CreatedAt)
                        .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();

                    return Result<List<CommentDto>>.Success(comments);

                }

            }
        }

    }
}