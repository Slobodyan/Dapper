using System.Net;
using System.Web.Mvc;
using DapperORM.Models;

namespace DapperORM.Controllers
{
	public class UserController : Controller
	{
		private readonly UserRepository _userRepository = new UserRepository();
		// GET: User
		public ActionResult Index()
		{
			return View(_userRepository.GetUsers());
		}

		// GET: User/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var user = _userRepository.Get(id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// GET: User/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: User/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,FirstName,LastName")] User user)
		{
			if (ModelState.IsValid)
			{
				_userRepository.Create(user);
				return RedirectToAction("Index");
			}

			return View(user);
		}

		// GET: User/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var user = _userRepository.Get(id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: User/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,FirstName,LastName")] User user)
		{
			if (ModelState.IsValid)
			{
				_userRepository.Update(user);
				return RedirectToAction("Index");
			}
			return View(user);
		}

		// GET: User/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User user = _userRepository.Get(id.Value);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: User/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			_userRepository.Delete(id);
			return RedirectToAction("Index");
		}
	}
}