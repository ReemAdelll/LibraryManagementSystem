﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Shared
{
    public class BookEditDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishedYear { get; set; }
    }
}