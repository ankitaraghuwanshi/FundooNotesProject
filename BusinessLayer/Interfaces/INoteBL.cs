using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        Task AddNote(int userId, NotePostModel notePostModel);
        Task ChangeColour(int userId, int NoteId, string colour);
        Task ArchiveNote(int userId, int noteId);


    }
}
