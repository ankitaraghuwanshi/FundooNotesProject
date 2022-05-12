using CommonLayer;
using RepositoryLayer.Entities;
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
        Task PinNote(int userId, int noteId);
        Task TrashNote(int userId, int noteId);
        Task<Note> UpdateNote(int userId, int noteId, NoteUpdateModel noteUpdateModel);
        Task DeleteNote(int noteId, int userId);
        Task Reminder(int userId, int noteId, DateTime Reminderdate);


    }
}
    

