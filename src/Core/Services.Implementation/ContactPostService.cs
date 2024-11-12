using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ContactPostService : IContactPostService
    {
        private readonly DbContext db;

        public ContactPostService(DbContext db) {
            this.db = db;
        }
        public string Add(string fullname, string email, string message)
        {
            var post = new ContactPost { FullName = fullname, Email = email, Message = message, CreatedAt = DateTime.Now };
            db.Set<ContactPost>().Add(post);
            db.SaveChanges();
            return "muraciet qeyde alindi";
        }
    }
}
