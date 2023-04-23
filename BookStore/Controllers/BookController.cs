using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;

        public BookController(IBookService bookService, IGenreService genreService, IAuthorService authorService, IPublisherService publisherService)
        {
            _bookService = bookService;
            _genreService = genreService;
            _authorService = authorService;
            _publisherService = publisherService;
        }

        public IActionResult Add()
        {
            var model = new Book();
            model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem
            {
                Text = a.AuthorName,
                Value = a.Id.ToString()
            }).ToList();
            model.GenreList = _genreService.GetAll().Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.Id.ToString()
            }).ToList();
            model.PublisherList = _publisherService.GetAll().Select(p => new SelectListItem
            {
                Text = p.PublisherName,
                Value = p.Id.ToString()
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Book model)
        {
            model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem
            {
                Text = a.AuthorName,
                Value = a.Id.ToString(),
                Selected = a.Id == model.AuthorId
            }).ToList();
            model.GenreList = _genreService.GetAll().Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.Id.ToString(),
                Selected = g.Id == model.GenreId
            }).ToList();
            model.PublisherList = _publisherService.GetAll().Select(p => new SelectListItem
            {
                Text = p.PublisherName,
                Value = p.Id.ToString(),
                Selected = p.Id == model.PublisherId
            }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _bookService.Add(model);

            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(("GetAll"));
            }

            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

        public IActionResult Update(int id)
        {
            var model = _bookService.FindById(id);
            model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem
            {
                Text = a.AuthorName,
                Value = a.Id.ToString(),
                Selected = a.Id == model.AuthorId
            }).ToList();
            model.GenreList = _genreService.GetAll().Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.Id.ToString(),
                Selected = g.Id == model.GenreId
            }).ToList();
            model.PublisherList = _publisherService.GetAll().Select(p => new SelectListItem
            {
                Text = p.PublisherName,
                Value = p.Id.ToString(),
                Selected = p.Id == model.PublisherId
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Book model)
        {
            model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem
            {
                Text = a.AuthorName,
                Value = a.Id.ToString(),
                Selected = a.Id == model.AuthorId
            }).ToList();
            model.GenreList = _genreService.GetAll().Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.Id.ToString(),
                Selected = g.Id == model.GenreId
            }).ToList();
            model.PublisherList = _publisherService.GetAll().Select(p => new SelectListItem
            {
                Text = p.PublisherName,
                Value = p.Id.ToString(),
                Selected = p.Id == model.PublisherId
            }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _bookService.Update(model);

            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction(("GetAll"));
            }

            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return RedirectToAction(("GetAll"));
        }

        public IActionResult GetAll()
        {
            var data = _bookService.GetAll();
            return View(data);
        }
    }
}
