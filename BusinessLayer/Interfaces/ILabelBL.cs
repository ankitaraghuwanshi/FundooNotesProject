using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task AddLabel(int userId, int noteId, string labelName);
        Task<List<Label>> GetLabelByuserId(int userId);
        Task<List<Label>> GetLabelByNoteId(int noteId);
        Task<Label> UpdateLabel(int labelId, int userId, string labelName);
        Task DeleteLabel(int labelId, int userId);
    }
}
