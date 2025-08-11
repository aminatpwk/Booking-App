using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Booking.Application.Features.Owners;
using Booking.Domain.Owners;
using FluentValidation;

namespace Booking.Application.Features.Owners.Commands
{
    public class CreateOwnerHandler : IRequestHandler<CreateOwnerCommand, Guid>
    {
        private readonly IOwnerRepository _ownerRepository;

        public CreateOwnerHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Guid> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = Owner.CreateOwner(request.OwnerDto);
            await _ownerRepository.Add(owner);
            return owner.Id;
        }

    }
}
