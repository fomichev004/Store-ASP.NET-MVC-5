﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace my_store_project.Models.Data
{
    public class Db : DbContext

    {
        public DbSet<PagesDTO> Pages { get; set; }
        public DbSet<SidebarDTO> Sidebars { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
    }
    
}