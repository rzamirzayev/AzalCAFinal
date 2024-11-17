using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Services;

namespace Services.Implementation
{
    class ServicesService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;

        public ServicesService(IServiceRepository serviceRepository) {
            this.serviceRepository = serviceRepository;
        }

        public async Task<AddServiceResponseDto> AddAsync(AddServiceRequestDto model, CancellationToken cancellationToken = default)
        {
            var entity= new ServiceClass { Name= model.Name ,Title=model.Title,Description=model.Description};
            await serviceRepository.AddAsync(entity, cancellationToken);
            await serviceRepository.SaveAsync();
            return new AddServiceResponseDto { Id=entity.Id,Name=entity.Name,Title=entity.Title,Description=entity.Description };
        }

        public async Task<EditServiceDto> EditAsync(EditServiceDto model,CancellationToken cancellationToken=default)
        {
            var entity = await serviceRepository.GetAsync(m => m.Id == model.Id, cancellationToken);

            entity.Name=model.Name;
            entity.Title = model.Title;
            entity.Description=model.Description;
            serviceRepository.Edit(entity);
            await serviceRepository.SaveAsync();
            return model;
        }

        public async Task<IEnumerable<ServiceGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data= await serviceRepository.GetAll().Select(m=>new ServiceGetAllDto
            {
                Id=m.Id,
                Name=m.Name,
                Title=m.Title,
                Description=m.Description
            }).ToListAsync(cancellationToken);
            return data;
        }

        public async Task<ServiceGetAllDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            var service = await serviceRepository.GetAsync(b => b.Id == id, cancellationToken);

            return new ServiceGetAllDto
            {
                Id = service.Id,
                Title = service.Title,
                Name = service.Name,
                Description = service.Description
            };
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await serviceRepository.GetAsync(m => m.Id == id, cancellationToken);
            serviceRepository.Remove(entity);
            await serviceRepository.SaveAsync(cancellationToken);
        }
    }
}
