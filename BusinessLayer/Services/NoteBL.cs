using BusinessLayer.Interfaces;
using CommonLayer;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public async Task AddNote(int userId, NotePostModel notePostModel)
        {
            try
            {
                await noteRL.AddNote(userId,notePostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ArchiveNote(int userId, int noteId)
        {
            try
            {
                await noteRL.ArchiveNote(userId, noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task ChangeColour(int userId, int NoteId, string colour)
        {
            try
            {
                await noteRL.ChangeColour(userId, NoteId, colour);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteNote(int noteId, int userId)
        {
            try
            {
                await noteRL.DeleteNote(noteId, userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task PinNote(int userId, int noteId)
        {
            try
            {
                await noteRL.PinNote(userId, noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task TrashNote(int userId, int noteId)
        {
            try
            {
                await noteRL.TrashNote(userId, noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Note> UpdateNote(int userId, int noteId, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                return await this.noteRL.UpdateNote(userId, noteId, noteUpdateModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
    
}
