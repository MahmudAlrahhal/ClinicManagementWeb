using DataAccess;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ClinicManagementWeb.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly MustafaClinicDbContext _db;
        private readonly IUnitofWork _unitofWork;
        public PaymentsController(MustafaClinicDbContext db, IUnitofWork unitofWork)
        {
            _db = db;
            _unitofWork = unitofWork;
        }
        public IActionResult Index(int AppointmentId)
        {
            Payments payments = new Payments();
            payments.AppointmentId = AppointmentId;
            payments.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
            return View("Index", payments);
        }
        public IActionResult AddPayment(Payments payment)
        {
            int ExecuteAddPaymentProcedure = _db.ExecuteAddPaymentStoredProcedure(payment.PaymentDate, payment.Amount, payment.PaymentMethod, payment.AppointmentId);
            Payments _payment = _unitofWork.PaymentsRepository.Get(p=>p.AppointmentId == payment.AppointmentId);
            return RedirectToAction("Index", "Payments", _payment);
        }
        public IActionResult GetPayments(int AppointmentId)
        {
            IEnumerable<Payments> paymentsList = _unitofWork.PaymentsRepository.GetAll(p=>p.AppointmentId == AppointmentId);
            return View(paymentsList);
        }
    }
}
