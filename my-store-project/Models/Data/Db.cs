using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace my_store_project.Models.Data
{
    public class Db : DbContext

    {
        public DbSet<PagesDTO> Pages { get; set; }
        // 6
        public DbSet<SidebarDTO> Sidebars { get; set; }
        // 8
        public DbSet<CategoryDTO> Categories { get; set; }
        // 11
        public DbSet<ProductDTO> Products { get; set; }
        // 22
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }
        //23
        public DbSet<UserRoleDTO> UserRoles { get; set; }
    }
    
}