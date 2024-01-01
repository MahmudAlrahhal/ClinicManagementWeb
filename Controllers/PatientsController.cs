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
    }
}
