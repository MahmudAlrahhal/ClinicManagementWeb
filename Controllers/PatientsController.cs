using ClinicManagementWeb.Models;
using DataAccess.Data;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementWeb.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IUnitofWork _IunitofWork;
        public PatientsController(IUnitofWork IunitofWork)
        {
            _IunitofWork = IunitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterPage()
        {
            return View();
        }
        public IActionResult Register(Patient patient) 
        {
            _IunitofWork.PatientRepository.Add(patient);
            _IunitofWork.save();
            return RedirectToAction("Index", "Appointments");
        }
        public IActionResult ShowPatient(string phoneNumber)
        {
            Patient patient = null;
            if (phoneNumber != null) 
            {
                patient = _IunitofWork.PatientRepository.Get(u => u.PhoneNumber.Equals(phoneNumber));
            }
            if (patient != null)
            {
                List<Appointment> patientAppointments = _IunitofWork.AppointmentRepository.GetAll(a => a.PatientId.Equals(phoneNumber)).ToList();
                patient.Appointments = patientAppointments;
                return PartialView(patient);
            }

            else
            {
                return null;//RedirectToAction("Index");
            }

        }
        public IActionResult DeleteAppointment(int id, string phoneNumber)
        {
            Appointment appointment = _IunitofWork.AppointmentRepository.find(id);
            _IunitofWork.AppointmentRepository.upDelete(appointment);
            return RedirectToAction("Index");
        }

        

        }
    }

