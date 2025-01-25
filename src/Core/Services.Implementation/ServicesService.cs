using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Repositories;
using Services.Services;

namespace Services.Implementation
{
    class ServicesService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IHostEnvironment env;

        public ServicesService(IServiceRepository serviceRepository,IHostEnvironment env) {
            this.serviceRepository = serviceRepository;
            this.env = env;
        }

        public async Task<AddServiceResponseDto> AddAsync(AddServiceRequestDto model, CancellationToken cancellationToken = default)
        {
            var entity= new ServiceClass { Name= model.Name ,Title=model.Title,Description=model.Description};

            var extension = Path.GetExtension(model.Image.FileName);
           entity.ImagePath = $"{Guid.NewGuid()}{extension}";
            string fullPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", entity.ImagePath);
            using (var fs = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write))
            {
                await model.Image.CopyToAsync(fs);
            }
            await serviceRepository.AddAsync(entity, cancellationToken);
            await serviceRepository.SaveAsync();
            return new AddServiceResponseDto { Id = entity.Id, Name = entity.Name, Title = entity.Title, Description = entity.Description, ImagePath = entity.ImagePath };
        }

        public async Task<EditServiceDto> EditAsync(EditServiceDto model,CancellationToken cancellationToken=default)
        {
            var entity = await serviceRepository.GetAsync(m => m.Id == model.Id, cancellationToken);

            entity.Name=model.Name;
            entity.Title = model.Title;
            entity.Description=model.Description;
            //entity.ImagePath=model.ImagePath;
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
                Description=m.Description,
                ImagePath=m.ImagePath

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
                Description = service.Description,
                ImagePath=service.ImagePath
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
