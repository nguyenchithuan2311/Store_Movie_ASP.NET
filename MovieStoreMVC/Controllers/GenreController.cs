using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreMvc.Models.Domain;
using MovieStoreMvc.Repositories.Abstract;

namespace MovieStoreMvc.Controllers
{
    //Controller vs controllerbase
    /*
     * Controllerbase hỗ trợ cho web api
     * Controller có chứa các phương thức hỗ trợ view
     */
    [Authorize]
    public class GenreController : Controller
    {
        // readonly chỉ có thể được khởi tạo vào thời điểm khai báo hoặc trong constructer của class đó
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        //Action Result trả về ViewResult: HTML response, RedirectResult: chuyển hướng đến 1 URL khác, Content Result trả về 1 chuỗi văn bản
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
        {
            /*
             * ModelState sẽ ghi nhận thông tin điền trong form và validate theo model
             */
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreService.Add(model);
            if (result)
            {
                //TempData["msg"] dùng để thông báo thành công hoặc thất bại ở trang kế tiếp
                TempData["msg"] = "Added Successfully";
                //
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = _genreService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(GenreList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult GenreList()
        {
            var data = this._genreService.List().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _genreService.Delete(id);
            return RedirectToAction(nameof(GenreList));
        }



    }
}