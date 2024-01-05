using Models;
using DataAccess;
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
            foreach (Appointment objAppointment in objAppointments)
            {
                if (objAppointment.IsPreserved == true)
                {
                    objAppointment.Patient = _unitofWork.PatientRepository.Get(p => p.PhoneNumber == objAppointment.PatientId);
                }
            }

            return View(objAppointments);
        }
        [HttpPost]
        public IActionResult Index1(DateTime date)
        {
            List<Appointment> objAppointments = _db.Appointments
            .Where(x => x.AppointmentDate == DateOnly.FromDateTime(date))
            .ToList();
            foreach(Appointment objAppointment in objAppointments)
            {
                if(objAppointment.IsPreserved == true)
                {
                    objAppointment.Patient = _unitofWork.PatientRepository.Get(p => p.PhoneNumber == objAppointment.PatientId);
                }
            }
            return View("Index",objAppointments);
        }
        [HttpPost]
        public IActionResult Select(Appointment appointment)
        {
            
            if (appointment.IsPreserved == true)
            {
                appointment.Patient = _unitofWork.PatientRepository.Get(p => p.PhoneNumber == appointment.PatientId);
            }
            
            return View(appointment);
        }
        [HttpPost]
        public IActionResult Preserve(Appointment aptmt)
        {
            int remainingAmount = Convert.ToInt32(aptmt.TotalPrice) - Convert.ToInt32(aptmt.PaidAmount);
            int procedure = _db.ExecuteAppointmentUpdateStoredProcedure(aptmt.AppointmentId, aptmt.AppointmentDate,
                aptmt.AppointmentTime, aptmt.TotalPrice, aptmt.PaidAmount, remainingAmount.ToString(),
                aptmt.PatientId, aptmt.DoctorId, aptmt.Note, true);

            List<Appointment> objAppointments = _db.Appointments
            .Where(x => x.AppointmentDate == DateOnly.FromDateTime(DateTime.Now))
            .ToList();
            foreach (Appointment objAppointment in objAppointments)
            {
                if (objAppointment.IsPreserved == true)
                {
                    objAppointment.Patient = _unitofWork.PatientRepository.Get(p => p.PhoneNumber == objAppointment.PatientId);
                }
            }

            return View("Index", objAppointments);
        }
        public IActionResult Delete(int AppointmentId)
        {
            Appointment appointment = _unitofWork.AppointmentRepository.find(AppointmentId);
            _unitofWork.AppointmentRepository.upDelete(appointment);
            return RedirectToAction("Index");
        }
    }
}
