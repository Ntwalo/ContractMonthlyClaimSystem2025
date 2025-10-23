using System;
using System.Collections.Generic;
using System.Linq;
using ContractMonthlyClaimSystem.Models.Domain;
using ContractMonthlyClaimSystem.Services.Interfaces;

namespace ContractMonthlyClaimSystem.Services.InMemory
{
    public class InMemoryLecturerService : ILecturerService
    {
        private readonly List<Lecturer> _lecturers = new();

        public Lecturer SeedDefaultLecturer()
        {
            if (_lecturers.Any()) return _lecturers.First();
            var lec = new Lecturer
            {
                Name = "Demo Lecturer",
                Email = "lecturer@example.com",
                HourlyRate = 500,
                BankDetails = "ABSA-123456"
            };
            _lecturers.Add(lec);
            return lec;
        }

        public Lecturer? GetById(Guid id) => _lecturers.FirstOrDefault(l => l.LecturerId == id);
        public IEnumerable<Lecturer> GetAll() => _lecturers;
    }
}
