using BookStore.ViewModel;
using FirstProject.Models;
using FirstProject.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepositories<Book> BookRepository;
        private readonly IBookStoreRepositories<Author> AuthorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookStoreRepositories<Book> BookRepository,IBookStoreRepositories<Author>AuthorRepository,IHostingEnvironment hosting)
        {
            this.BookRepository = BookRepository;
            this.AuthorRepository = AuthorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var Books = BookRepository.List();
            return View(Books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var Books = BookRepository.Find(id);
            return View(Books);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookRepositoryViewModel {
                author = fillAuthor()

            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookRepositoryViewModel Books)
        {
            if (ModelState.IsValid)
            {
                string FileName = UploadFile(Books.file) ?? string.Empty;
                try
                {
                  
                    if (Books.AuthorId == -1)
                    {
                        ViewBag.AlertMessage = "Please Select Author Name from List";
                        var model1 = new BookRepositoryViewModel
                        {
                            author = fillAuthor()

                        };
                        return View(model1);
                    }
                    Book book = new Book
                    {
                        id = Books.id,
                        Tittle = Books.Tittle,
                        Description = Books.Description,
                        Author = AuthorRepository.Find(Books.AuthorId),
                        ImageUpload=FileName,
                      

                    };
                    BookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            var model = new BookRepositoryViewModel
            {
                author = fillAuthor()

            };
            ModelState.AddModelError("", "You have to Fill All Requirment");
            return View();
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var Books = BookRepository.Find(id);

            var book = new BookRepositoryViewModel
            {
                 id = Books.id,
                Tittle = Books.Tittle,
                Description = Books.Description,
                AuthorId = Books.Author.id,
                author =fillAuthor(),
                imgView=Books.ImageUpload
                

            };
            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookRepositoryViewModel Books)
        {
            string FileName =UploadFile(Books.file , Books.imgView )?? string.Empty;
            try
            {
               
                Book Book = new Book
                {
                    id = Books.id,
                    Tittle=Books.Tittle,
                    Description=Books.Description,
                    Author=AuthorRepository.Find(Books.AuthorId),
                    ImageUpload=FileName
                    
                   
                };
                BookRepository.Update(id,Book);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var Books = BookRepository.Find(id);
            return View(Books);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book Books)
        {
            try
            {
                BookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string term)
        {
            var result = BookRepository.Search(term);
            return View("Index", result);
        }
        List<Author> fillAuthor()
        {
            var author = AuthorRepository.List().ToList();
            author.Insert(0, new Author { id = -1, Name = "-----------Please Select Author Name----------" });
            return author;
        }
        string UploadFile(IFormFile File)
        {
            if (File != null)
            {

                string Upload = Path.Combine(hosting.WebRootPath, "Upload");
                string FullPath = Path.Combine(Upload, File.FileName);
                File.CopyTo(new FileStream(FullPath, FileMode.Create));

                return File.FileName;
            }
            return null;
        } 
        string UploadFile(IFormFile File,string ImgUrl)
        {
            if (File != null)
            {
                string Upload = Path.Combine(hosting.WebRootPath, "Upload");
              
                string NewFullPath = Path.Combine(Upload, File.FileName);

                //delete old img

               
                string OldPath = Path.Combine(Upload, ImgUrl);
                if (NewFullPath != OldPath)
                {
                    System.IO.File.Delete(OldPath);
                    //save new img
                    File.CopyTo(new FileStream(NewFullPath, FileMode.Create));
                }

                return File.FileName;
            }
            return ImgUrl;
        }
    }
}
