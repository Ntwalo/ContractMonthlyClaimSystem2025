using System;
using System.Collections.Generic;
using ContractMonthlyClaimSystem.Models.Domain;

namespace ContractMonthlyClaimSystem.Services.Interfaces
{
    public interface ILecturerService
    {
        Lecturer SeedDefaultLecturer(); // simple seeding for demo
        Lecturer? GetById(Guid id);
        IEnumerable<Lecturer> GetAll();
    }
}
