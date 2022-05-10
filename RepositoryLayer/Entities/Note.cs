using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public string Ispin { get; set; }
        public string IsArchieve { get; set; }
        public string IsRemainder { get; set; }
        public string IsTrash { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime RemainderDate { get; set; }
        public int Id { get; set; }
       public virtual User User { get; set; }

    }
}
