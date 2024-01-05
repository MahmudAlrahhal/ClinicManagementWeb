using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ClinicManagementWeb.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly MustafaClinicDbContext _db;
        public PaymentsController(MustafaClinicDbContext db)
        {
            _db = db;
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
            return View("Index", payment.AppointmentId);
        }
    }
}
