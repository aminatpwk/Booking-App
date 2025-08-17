using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Booking.Application.Features.Owners;
using Booking.Domain.Owners;
using FluentValidation;
using Booking.Application.Features.Users;

namespace Booking.Application.Features.Owners.Commands
{
    public class CreateOwnerHandler : IRequestHandler<CreateOwnerCommand, Guid>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserRepository _userRepository;

        public CreateOwnerHandler(IOwnerRepository ownerRepository, IUserRepository userRepository)
        {
            _ownerRepository = ownerRepository;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.OwnerDto;

            var responsibleUser = await _userRepository.GetById(dto.UserId);
            if(responsibleUser == null)
            {
                throw new ValidationException("User not found!");
            }

            var existingOwner = await _ownerRepository.GetByUserId(dto.UserId);
            if(existingOwner != null)
            {
                throw new ValidationException("User already has an Owner profile linked to it!");
            }

            var isIdentityCardUnique = await _ownerRepository.IsUniqueIdentityCardNumber(dto.IdentityCardNumber, cancellationToken);
            if (!isIdentityCardUnique)
            {
                throw new ValidationException("Identity card number already exists!");
            }

            var owner = Owner.CreateOwner(dto.UserId, dto.IdentityCardNumber, dto.BankAccount, dto.PhoneNumber);
            await _ownerRepository.Add(owner);
            return owner.Id;
        }

    }
}
