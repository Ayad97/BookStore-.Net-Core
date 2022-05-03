using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models.Repositories
{
    public class BookRepositories : IBookStoreRepositories<Book> 
    {
        List<Book> books;
        public BookRepositories()
        {
            books = new List<Book>()
            {
                new Book
                {
                    id=1,Tittle="Core Book",Description="Development Books",Author=new Author{ id=1,Name="Mahmoud"},ImageUpload="contact_form_bg.png"
                },
                new Book
                {
                    id=2,Tittle="SQL Book",Description="Development Books",Author=new Author{ id=2,Name="Mohamed"},ImageUpload="contact_form_bg.png"
                },
                new Book
                {
                    id=3,Tittle="Java Book",Description="Development Books",Author=new Author{ id=2,Name="Mohamed"},ImageUpload="contact_form_bg.png"
                },
            };
        }
        public void Add(Book entity)
        {
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
           
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(a => a.id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            return books.Where(a => a.Tittle.Contains(term) 
                            || a.Description.Contains(term)
                            ||a.Author.Name.Contains(term)).ToList();
        }

        public void Update(int id, Book newBook)
        {
            var book = Find(id);
            book.Tittle = newBook.Tittle;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.ImageUpload = newBook.ImageUpload;
        }
    }
}
