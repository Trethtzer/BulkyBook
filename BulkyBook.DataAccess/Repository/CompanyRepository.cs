using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company c)
        {
            var objFromDb = _db.Companies.FirstOrDefault(s => s.Id == c.Id);

            if(objFromDb != null)
            {
                objFromDb.Name = c.Name;
                objFromDb.StreetAddress = c.StreetAddress;
                objFromDb.State = c.State;
                objFromDb.PostalCode = c.PostalCode;
                objFromDb.PhoneNumber = c.PhoneNumber;
                objFromDb.IsAuthorizedCompany = c.IsAuthorizedCompany;
                objFromDb.City = c.City;
            }    
            
            // _db.Update(c);
        }
    }
}
