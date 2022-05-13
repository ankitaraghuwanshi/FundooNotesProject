using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        Task AddLabel(int userId, int noteId, string labelName);
        Task<List<Label>> GetLabelByuserId(int userId);
        Task<List<Label>> GetlabelByNoteId(int NoteId);
        Task<Label> UpdateLabel(int userId, int labelId, string labelName);
        Task DeleteLabel(int labelId, int userId);
    }
}
