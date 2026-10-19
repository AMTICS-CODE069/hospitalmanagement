using DoctorApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorApp.Controllers;

public class DoctorsController : Controller
{
    private readonly DoctorService _service;

    public DoctorsController(DoctorService service)
    {
        _service = service;
    }

    public IActionResult Index(string? specialization)
    {
        if (specialization != null)
        {
            HttpContext.Session.SetString("Specialization", specialization);
        }

        var selected = HttpContext.Session.GetString("Specialization") ?? "";
        var all = _service.GetAll();
        var doctors = selected == "" ? all : all.Where(d => d.Specialization == selected).ToList();

        ViewBag.PatientName = Request.Cookies["PatientName"];
        ViewBag.Selected = selected;
        ViewBag.Specializations = all.Select(d => d.Specialization).Distinct().ToList();

        return View(doctors);
    }

    [HttpPost]
    public IActionResult SetName(string patientName)
    {
        Response.Cookies.Append("PatientName", patientName ?? "", new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(7)
        });
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "id" })]
    public IActionResult Details(int id)
    {
        var doctor = _service.GetById(id);
        if (doctor == null)
        {
            return NotFound();
        }
        return View(doctor);
    }
}
