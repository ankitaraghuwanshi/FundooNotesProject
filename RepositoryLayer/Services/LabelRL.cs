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
    public class LabelRL : ILabelRL
    {
        FundoosContext fundoosContext;
        public IConfiguration Configuration { get; }
        public LabelRL(FundoosContext fundoosContext, IConfiguration configuration)
        {
            this.fundoosContext = fundoosContext;
            this.Configuration = Configuration;
        }

        public async Task AddLabel(int userId, int noteId, string labelName)
        {
            try
            {
                Label label = new Label();
                label.labelName = labelName;
                label.userId = userId;
                label.NoteId = noteId;
                fundoosContext.Add(label);
                await fundoosContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Label>> GetLabelByuserId(int userId)
        {
            try
            {
                List<Label> reuslt = await fundoosContext.Label.Where(u => u.userId == userId).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Label>> GetlabelByNoteId(int NoteId)
        {
            try
            {
                List<Label> reuslt = await fundoosContext.Label.Where(u => u.NoteId == NoteId).Include(u => u.User).Include(u => u.Note).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
        
    

