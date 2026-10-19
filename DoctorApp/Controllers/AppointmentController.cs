using DoctorApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorApp.Controllers;

public class AppointmentController : Controller
{
    private readonly DoctorService _service;

    public AppointmentController(DoctorService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Book(int doctorId, DateTime appointmentDate)
    {
        var patientName = Request.Cookies["PatientName"];
        if (string.IsNullOrEmpty(patientName))
        {
            return RedirectToAction("Index", "Doctors");
        }

        var doctor = _service.GetById(doctorId);
        if (doctor == null)
        {
            return NotFound();
        }

        ViewBag.PatientName = patientName;
        ViewBag.Specialization = HttpContext.Session.GetString("Specialization");
        ViewBag.Date = appointmentDate;
        return View(doctor);
    }
}
