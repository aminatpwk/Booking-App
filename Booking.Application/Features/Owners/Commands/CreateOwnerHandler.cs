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
        private readonly CreateOwnerValidations _validations;

        public CreateOwnerHandler(IOwnerRepository ownerRepository, CreateOwnerValidations validations)
        {
            _ownerRepository = ownerRepository;
            _validations = validations;
        }

        public async Task<Guid> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var isValidResult = await _validations.ValidateAsync(request, cancellationToken);
            if (!isValidResult.IsValid)
            {
                var errors = string.Join(", ", isValidResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }
            var owner = Owner.CreateOwner(request.OwnerDto);
            await _ownerRepository.Add(owner);
            return owner.Id;
        }

    }
}
