using DoctorApp.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DoctorApp.Services;

public class DoctorService
{
    private readonly IMemoryCache _cache;

    public DoctorService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public List<Doctor> GetAll()
    {
        return _cache.GetOrCreate("DoctorList", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3);
            return new List<Doctor>
            {
                new Doctor { DoctorId = 1, DoctorName = "Dr. Mehta", Specialization = "Cardiology", Experience = 12, ConsultationFee = 600 },
                new Doctor { DoctorId = 2, DoctorName = "Dr. Shah", Specialization = "Dermatology", Experience = 8, ConsultationFee = 400 },
                new Doctor { DoctorId = 3, DoctorName = "Dr. Patel", Specialization = "Neurology", Experience = 15, ConsultationFee = 800 },
                new Doctor { DoctorId = 4, DoctorName = "Dr. Desai", Specialization = "Cardiology", Experience = 5, ConsultationFee = 500 },
                new Doctor { DoctorId = 5, DoctorName = "Dr. Joshi", Specialization = "Pediatrics", Experience = 10, ConsultationFee = 350 }
            };
        })!;
    }

    public Doctor? GetById(int id)
    {
        return GetAll().FirstOrDefault(d => d.DoctorId == id);
    }
}
