using MediatR;
using Application.Core;
using Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Application.Profiles
{
    public class Details
    {
        public class Query : IRequest<Result<Profile>>
        {
            public string UserName { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<Profile>>
        {

            private readonly DataContext _context;
            private readonly IMapper _mapper;

            private readonly IUserAccessor _userAccessor; 
            public Handler(DataContext context, IMapper mapper,
            IUserAccessor userAccessor)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;


            }
            public async Task<Result<Profile>> Handle(Query request, 
            CancellationToken cancellationToken)
            {
                var user = await _context.Users
                .ProjectTo<Profile>(_mapper.ConfigurationProvider, 
                new {currentUsername =_userAccessor.GetUsername()})
                .SingleOrDefaultAsync(x  => x.Username == request.UserName);

                return Result<Profile>.Success(user);
            }
        }
    }
}