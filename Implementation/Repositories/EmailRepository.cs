using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.Context;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Implementation.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationContext _context;

        public EmailRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<EmailDto> CreateAsync(Email email)
        {
            await _context.Emails.AddAsync(email);
            await _context.SaveChangesAsync();
            return new EmailDto
            {
                Id = email.Id,
                Subject = email.Subject,
                Content = email.Content,
                EmailType = email.EmailType.ToString()
            };
        }

        // public async void DeleteAsync(Email email)
        // {
        //     _context.Emails.Remove(email);
        //     await _context.SaveChangesAsync();
        // }

        public async Task<IList<EmailDto>> GetAllAsync()
        {
            var emails = await _context.Emails.Where(a =>  a.IsDeleted == false).ToListAsync();
            if(emails == null)
            {
                return null;
            }
            return emails.Select(a => new EmailDto
            {
                Id = a.Id,
                Subject = a.Subject,
                Content = a.Content,
                EmailType = a.EmailType.ToString()
            }).ToList();
        }

        public async Task<EmailDto> GetEmailByEmailTypeReturningEmailObjectDtoAsync(EmailType emailType)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(a => a.EmailType == emailType && a.IsDeleted == false);
            if(email == null)
            {
                return null;
            }
            return new EmailDto
            {
                Id = email.Id,
                Subject = email.Subject,
                Content = email.Content,
                EmailType = email.EmailType.ToString()
            };
        }

        public async Task<Email> GetEmailByIdReturningEmailObjectAsync(int id)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(email == null)
            {
                return null;
            }
            return email;
           
        }

        public async Task<EmailDto> GetEmailByIdReturningEmailObjectDtoAsync(int id)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(email == null)
            {
                return null;
            }
            return new EmailDto
            {
                Id = email.Id,
                Subject = email.Subject,
                Content = email.Content,
                EmailType = email.EmailType.ToString()
            };
        }

        public async Task<EmailDto> UpdateAsync(Email email)
        {
            _context.Emails.Update(email);
            await _context.SaveChangesAsync();
            return new EmailDto
            {
                Id = email.Id,
                Subject = email.Subject,
                Content = email.Content,
                EmailType = email.EmailType.ToString()
            };
        }
    }
}