﻿using System;
using System.Collections.Generic;

namespace KitchenService.Api.Model
{
    public class Note
    {
        public int Id { get; set; }

        public User Author { get; set; }

        public bool IsPublic { get; set; }

        public string Text { get; set; }

        public DateTime DateModified { get; set; } = DateTime.Now;

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
