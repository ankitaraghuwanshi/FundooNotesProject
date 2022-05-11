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
        public bool Ispin { get; set; }
        public bool IsArchieve { get; set; }
        public bool IsRemainder { get; set; }
        public string IsTrash { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime RemainderDate { get; set; }
        public int userId { get; set; }
        public virtual User User { get; set; }

    }
}
