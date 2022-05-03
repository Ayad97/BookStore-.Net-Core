using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models.Repositories
{
    public class AuthorRepositories : IBookStoreRepositories<Author> 
    {
        IList<Author> authors;
        public AuthorRepositories()
        {
            authors = new List<Author>()
            {
                new Author
                {
                    id=1,Name="Mahmoud"
                },
                new Author
                {
                    id=2,Name="Mohamed"
                },
            };
        }
        public void Add(Author entity)
        {
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author); 
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.id == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a => a.Name.Contains(term)).ToList(); ;
        }

        public void Update(int id, Author newauthor)
        {
            var author = Find(id);
            author.Name = newauthor.Name;
        }
    }
}
