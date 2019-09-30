using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace my_store_project.Models.Data
{
    [Table("tblSidebar")]
    public class SidebarDTO
    {
        [Key]
        public int Id { get; set; }        
        public string Body { get; set; }
    }
}
