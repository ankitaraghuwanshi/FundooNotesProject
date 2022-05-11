using CommonLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
                note.userId = userId;
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
        //method for changing colour of note
        public async Task ChangeColour(int userId, int NoteId, string colour)
        {
            try
            {
                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == userId && e.NoteId == NoteId);//mathcing
                if (note != null)
                {
                    note.Colour = colour;
                    await fundoosContext.SaveChangesAsync();
                }
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
                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == userId && e.NoteId == noteId);
                if (note != null)
                {
                    if (note.IsArchieve == true)
                    {
                        note.IsArchieve = false;
                    }

                    if (note.IsArchieve == false)
                    {
                        note.IsArchieve = true;
                    }
                }

                await fundoosContext.SaveChangesAsync();

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
                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == userId && e.NoteId == noteId);
                if (note != null)
                {
                    note.Title = noteUpdateModel.Title;
                    note.Description = noteUpdateModel.Description;
                    note.IsArchieve = noteUpdateModel.IsArchieve;
                    note.Colour = noteUpdateModel.Colour;
                    note.Ispin = noteUpdateModel.Ispin;
                    note.IsTrash = noteUpdateModel.IsTrash;
                    note.IsRemainder = noteUpdateModel.IsRemainder;

                    await fundoosContext.SaveChangesAsync();

                }
                return await fundoosContext.Notes
                .Where(u => u.userId == u.userId && u.NoteId == noteId)
                .Include(u => u.User)
                .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteNote(int noteId, int userId)
        {
            try
            {
                var note = fundoosContext.Notes.FirstOrDefault(u => u.NoteId == noteId && u.userId == userId);
                fundoosContext.Notes.Remove(note);
                await fundoosContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}        




    

