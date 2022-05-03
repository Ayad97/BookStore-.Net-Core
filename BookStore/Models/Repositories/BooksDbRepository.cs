using FirstProject.Models;
using FirstProject.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BooksDbRepository : IBookStoreRepositories<Book> 
    {
        BookStoreDbContext db;
        public BooksDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            db.SaveChanges();

        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(a => a.id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.Author).ToList() ;
        }

        public List<Book> Search(string term)
        {
            return db.Books.Include(a=>a.Author).Where(a => a.Tittle.Contains(term)
                            || a.Description.Contains(term)
                            || a.Author.Name.Contains(term)).ToList(); ;
        }

        public void Update(int id, Book newBook)
        {
            db.Update(newBook);
            db.SaveChanges();

        }
    }
}
