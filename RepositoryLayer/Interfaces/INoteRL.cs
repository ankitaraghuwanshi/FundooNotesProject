﻿using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        Task AddNote(int userId, NotePostModel notePostModel);
        
        Task ChangeColour(int userId, int NoteId,string colour);


    }
}
