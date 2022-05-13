using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
           this.labelRL = labelRL;
        }
        public async Task AddLabel(int userId, int noteId, string labelName)
        {
            try
            {
               await labelRL.AddLabel(userId, noteId, labelName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteLabel(int labelId, int userId)
        {
            try
            {
                await this.labelRL.DeleteLabel(labelId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Label>> GetLabelByNoteId(int noteId)
        {
            try
            {
                return await this.labelRL.GetlabelByNoteId(noteId);
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
                return await this.labelRL.GetLabelByuserId(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Label> UpdateLabel(int userId, int labelId, string labelName)
        {
            try
            {
                return await this.labelRL.UpdateLabel(userId, labelId, labelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
