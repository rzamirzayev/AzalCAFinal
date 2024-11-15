using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ContactPostService : IContactPostService
    {
        private readonly IContactPostRepository contactPostRepository;

        public ContactPostService(IContactPostRepository contactPostRepository) {
            this.contactPostRepository = contactPostRepository;
        }
        public async Task<string> Add(string fullname, string email, string message)
        {
            var post = new ContactPost { FullName = fullname, Email = email, Message = message};
            await contactPostRepository.AddAsync(post);
            await contactPostRepository.SaveAsync();
            return "muraciet qeyde alindi";
        }
    }
}
