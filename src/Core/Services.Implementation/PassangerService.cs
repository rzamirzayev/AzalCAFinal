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
                Name = model.Name.ToUpper(),
                Surname = model.Surname.ToUpper(),
                Phone= model.Phone,
            };
            await passangersRepository.AddAsync(entity,cancellationToken);
            await passangersRepository.SaveAsync();
            return new AddPassangerResponseDto { PassangerId = entity.PassangerId, FinCode = entity.FinCode, Email = entity.Email, Gender = entity.Gender, Name = entity.Name, Surname = entity.Surname, Phone = entity.Phone,DateOfBirth=entity.DateOfBirth.ToString(), };

        }

        public async Task<PassangerGetAllDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            var data=await passangersRepository.GetAsync(p=>p.PassangerId== id);
            return new PassangerGetAllDto
            {
                PassangerId=data.PassangerId,
                Name=data.Name,
                Surname=data.Surname,
                DateOfBirth=data.DateOfBirth,
                Phone=data.Phone,
                Email=data.Email,
                Gender=data.Gender,
                FinCode=data.FinCode,

            };
        }
    }
}
