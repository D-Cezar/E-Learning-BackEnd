using AutoMapper;
using E_Learning.DTOs.Responses;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.Queries
{
    public class AuthQuery : IRequest<UserDTO>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class AuthHandler : IRequestHandler<AuthQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Authenticate(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }

            var result = _mapper.Map<UserDTO>(user);

            return result;
        }
    }
}