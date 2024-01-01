using ClinicManagementWeb.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly MustafaClinicDbContext _db;
        private readonly IUnitofWork _unitofWork;
        public AppointmentsController(MustafaClinicDbContext db, IUnitofWork unitofWork)
        {
            _db = db;
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            List<Appointment> objAppointments = _db.Appointments
            .Where(x => x.AppointmentDate == DateOnly.FromDateTime(DateTime.Now))
            .ToList();
            return View(objAppointments);
        }
        [HttpPost]
        public IActionResult Index1(DateTime date)
        {
            List<Appointment> objAppointments = _db.Appointments
            .Where(x => x.AppointmentDate == DateOnly.FromDateTime(date))
            .ToList();
            return View("Index",objAppointments);
        }
        [HttpPost]
        public IActionResult Select(Appointment appointment)
        {
            return View(appointment);
        }
        [HttpPost]
        public IActionResult Preserve(Appointment aptmt)
        {
            int procedure = _db.ExecuteAppointmentUpdateStoredProcedure(aptmt.AppointmentId, aptmt.AppointmentDate,
                aptmt.AppointmentTime, aptmt.TotalPrice, aptmt.PaidAmount, aptmt.RemainingAmount,
                aptmt.PatientId, aptmt.DoctorId, aptmt.Note, true);

            List<Appointment> objAppointments = _db.Appointments
            .Where(x => x.AppointmentDate == DateOnly.FromDateTime(DateTime.Now))
            .ToList();
            return View("Index", objAppointments);
        }
        public IActionResult Delete(int id)
        {
            Appointment appointment = _unitofWork.AppointmentRepository.find(id);
            _unitofWork.AppointmentRepository.upDelete(appointment);
            return RedirectToAction("Index");
        }
    }
}
