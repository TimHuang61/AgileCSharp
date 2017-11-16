using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo_Sprint1.Models;

namespace Demo_Sprint1.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository ?? throw new ArgumentNullException();
        }


        public ActionResult List()
        {
            return View();
        }


        public ActionResult Create()
        {
            return View(new CreateRoomViewModel());
        }


        [HttpPost]
        public ActionResult Create(CreateRoomViewModel model)
        {
            ActionResult result;

            if (ModelState.IsValid)
            {
                roomRepository.CreateRoom(model.NewRoomName);
                result = RedirectToAction("List");
            }
            else
            {
                result = View("Create", model);
            }

            return result;
        }
    }
}