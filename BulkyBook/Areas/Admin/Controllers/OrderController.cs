using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {

        // Repository pattern
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetOrderList(string status)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<OrderHeader> orderHeaderList;

            if(User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                orderHeaderList = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == claim.Value , includeProperties: "ApplicationUser");
            }

            // Now filter depending on type of orders requested.
            switch (status)
            {
                case "rejected":
                    orderHeaderList = orderHeaderList.Where(o => o.OrderStatus == SD.StatusCancelled
                                                                 || o.OrderStatus == SD.StatusRefunded
                                                                 || o.PaymentStatus == SD.PaymentStatusRejected);
                    break;
                case "pending":
                    orderHeaderList = orderHeaderList.Where(o => o.PaymentStatus == SD.PaymentStatusDelayedPayment);
                    break;              
                case "inprocess":
                    orderHeaderList = orderHeaderList.Where(o => o.OrderStatus == SD.PaymentStatusPending
                                                                 || o.OrderStatus == SD.StatusApproved
                                                                 || o.OrderStatus == SD.StatusPending);
                    break;
                case "completed":
                    orderHeaderList = orderHeaderList.Where(o => o.OrderStatus == SD.StatusShipped);
                    break;
                default:
                    break;
            }

            return Json(new { data = orderHeaderList });
        }
        #endregion
    }
}
