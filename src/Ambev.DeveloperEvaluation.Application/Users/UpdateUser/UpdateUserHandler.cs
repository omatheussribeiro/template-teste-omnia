﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Common.Security;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Handler for processing UpdateUserCommand requests
    /// </summary>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        /// Initializes a new instance of UpdateUserHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for UpdateUserCommand</param>
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Handles the UpdateUserCommand request
        /// </summary>
        /// <param name="command">The UpdateUser command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated user details</returns>
        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingUser = await _userRepository.GetByIdAsync(command.Id, cancellationToken);
            if(existingUser is null)
                throw new InvalidOperationException($"User with id ID does not yet exist");

            var existingUserEmail = await _userRepository.GetByEmailAsync(existingUser.Email, cancellationToken);
            if (existingUserEmail is null)
                throw new InvalidOperationException($"User with email {command.Email} already exists");

            existingUser.Username = command.Username;
            existingUser.Email = command.Email;
            existingUser.Phone = command.Phone;
            existingUser.Role = command.Role;
            existingUser.Status = command.Status;
            existingUser.UpdatedAt = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(command.Password))
                existingUser.Password = _passwordHasher.HashPassword(command.Password);

            var updatedUser = await _userRepository.UpdateAsync(existingUser, cancellationToken);
            var result = _mapper.Map<UpdateUserResult>(updatedUser);
            return result;
        }
    }
}
