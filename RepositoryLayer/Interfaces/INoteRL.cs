using CommonLayer;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        Task AddNote(int userId, NotePostModel notePostModel);
        Task ChangeColour(int userId, int NoteId, string colour);
        Task ArchiveNote(int userId, int noteId);
        Task PinNote(int userId, int noteId);
        Task TrashNote(int userId, int noteId);
        Task Reminder(int userId, int noteId, DateTime Reminderdate);
        Task<List<Note>> GetAllNote(int userId);
        Task<Note> UpdateNote(int userId, int noteId, NoteUpdateModel noteUpdateModel);
        Task DeleteNote(int noteId, int userId);
        Task<Note> GetNote(int noteId, int userId);

    }
}

    




    


    

