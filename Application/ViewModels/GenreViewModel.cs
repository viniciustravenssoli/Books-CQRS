using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BooksQuantity { get; set; }

        public static implicit operator GenreViewModel(Genre genre)
        {
            if (genre is null) 
                return null;
            return new()
            {
                Id = genre.Id,
                Name = genre.Name,
                BooksQuantity = genre.CountBooks(),
            };
        }
    }
}