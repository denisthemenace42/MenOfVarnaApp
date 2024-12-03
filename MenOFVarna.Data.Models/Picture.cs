﻿namespace MenOFVarna.Data.Models
{
    public class Picture
    {

        public Picture()
        {
            this.Id = Guid.NewGuid(); 
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Author { get; set; } = null!;
    }
}
