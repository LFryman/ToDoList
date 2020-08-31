using System;
using System.Collections.Generic;

namespace ToDoList_08272020.Models
{
    public partial class ToDoList
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
