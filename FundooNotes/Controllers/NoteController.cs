using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        

        

        FundoosContext fundoosContext;
        INoteBL noteBL;
        public NoteController(FundoosContext fundoosContext, INoteBL noteBL)
        {
            this.fundoosContext = fundoosContext;
            this.noteBL = noteBL;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                await this.noteBL.AddNote(UserId,notePostModel);
                return this.Ok(new { success = true, message = "Note Added Successfully " });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ChangeColour/{noteId}/{colour}")]

        public async Task<ActionResult> ChangeColour(int noteId, string colour)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == UserId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "SOORY! Note does nor Exist" });
                }
                await this.noteBL.ChangeColour( UserId,noteId,colour);
                return this.Ok(new { success = true, message = "Note Colour Changed Successfully " });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }     
}
