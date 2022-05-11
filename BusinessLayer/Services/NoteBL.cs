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

    }
    
}
