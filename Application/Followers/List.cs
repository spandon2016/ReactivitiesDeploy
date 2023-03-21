using MediatR;
using Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Application.Core;
using Application.Interfaces;
using Application.Profiles;
using Profile = Application.Profiles.Profile;

namespace Application.Followers
{
    public class List
    {


        public class Query : IRequest<Result<List<Profile>>>
        {

            public string Predicate { get; set; }
            public string UserName { get; set; }

            public class Handler : IRequestHandler<Query, Result<List<Profiles.Profile>>>
            {
                DataContext _context;
                IMapper _mapper;

                IUserAccessor _userAccessor; 
                public Handler(DataContext context, IMapper mapper,
                IUserAccessor userAccessor)
                {
                    _context = context;
                    _mapper = mapper;
                    _userAccessor = userAccessor;

                }
                public async Task<Result<List<Profiles.Profile>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    var profiles  = new List<Profiles.Profile>();

                    switch (request.Predicate)
                    {
                        case "followers":
                            profiles =
                                await _context.UserFollowings.Where(
                                    x => x.Target.UserName == request.UserName      
                                )
                                .Select(u => u.Observer)
                                .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider,
                                     new {currentUsername = _userAccessor.GetUsername()})
                                .ToListAsync();
                            break;
                               case "following":
                            profiles =
                                await _context.UserFollowings.Where(
                                    x => x.Observer.UserName == request.UserName      
                                )
                                .Select(u => u.Target)
                                .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider, 
                                     new {currentUsername = _userAccessor.GetUsername()})
                                .ToListAsync();
                            break;
                    }

                    return Result<List<Profiles.Profile>>.Success(profiles);
                    

                }
            }
        }
        
    }
}