using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static implicit operator AuthorViewModel(Author author)
        {
            if (author is null) 
                return null;
            return new()
            {
                Id = author.Id,
                Name = author.Name
            };
        }
    }
}