using System.Web.Mvc;
using Dapper.Models;

namespace Dapper.Controllers
{
	public class UserController : Controller
	{
		private readonly UserRepository _userRepository = new UserRepository();

		public ActionResult Index()
		{
			return View(_userRepository.GetUsers());
		}

		public ActionResult Details(int id)
		{
			var user = _userRepository.Get(id);
			if (user != null)
				return View(user);
			return HttpNotFound();
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(User user)
		{
			_userRepository.Create(user);
			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			var user = _userRepository.Get(id);
			if (user != null)
				return View(user);
			return HttpNotFound();
		}

		[HttpPost]
		public ActionResult Edit(User user)
		{
			_userRepository.Update(user);
			return RedirectToAction("Index");
		}

		[HttpGet]
		[ActionName("Delete")]
		public ActionResult ConfirmDelete(int id)
		{
			var user = _userRepository.Get(id);
			if (user != null)
				return View(user);
			return HttpNotFound();
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			_userRepository.Delete(id);
			return RedirectToAction("Index");
		}
	}
}