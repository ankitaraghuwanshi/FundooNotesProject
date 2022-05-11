using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        FundoosContext fundoosContext;
        public IConfiguration Configuration { get; }
        public NoteRL(FundoosContext fundoosContext, IConfiguration configuration)
        {
            this.fundoosContext = fundoosContext;
            this.Configuration = Configuration;
        }

        public async Task AddNote(int userId, NotePostModel notePostModel)
        {
            try
            {
                Note note = new Note();
                note.userId=userId;
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.Colour = notePostModel.Colour;
                note.Ispin = false;
                note.IsArchieve = false;
                note.IsRemainder = false;
                note.RegisterDate = DateTime.Now;
                note.ModifyDate = DateTime.Now;
                fundoosContext.Add(note);
                await fundoosContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }






        }
    }
}
