using BusinessLayer.Interfaces;
using CommonLayer;
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
            catch (Exception)
            {

                throw;
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
    }
    
}
