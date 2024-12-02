using Services.Passanger;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Services.Implementation
{
    class PassangerService : IPassangerService
    {
        private readonly IPassangersRepository passangersRepository;
        public PassangerService(IPassangersRepository passangersRepository) {
            this.passangersRepository = passangersRepository;
        }
        public async Task<AddPassangerResponseDto> AddAsync(AddPassangerRequestDto model, CancellationToken cancellationToken = default)
        {
            var entity = new Passenger
            {
                FinCode = model.FinCode,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                Gender = model.Gender,
                Name = model.Name,
                Surname = model.Surname,
                Phone= model.Phone,
            };
            await passangersRepository.AddAsync(entity,cancellationToken);
            await passangersRepository.SaveAsync();
            return new AddPassangerResponseDto { PassangerId = entity.PassangerId, FinCode = entity.FinCode, Email = entity.Email, Gender = entity.Gender, Name = entity.Name, Surname = entity.Surname, Phone = entity.Phone,DateOfBirth=entity.DateOfBirth.ToString(), };

        }
    }
}
