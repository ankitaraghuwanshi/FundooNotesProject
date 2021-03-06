using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        FundoosContext fundoosContext;
        INoteBL noteBL;

        private readonly IMemoryCache memoryCache;  //for redis cache
        private readonly IDistributedCache distributedCache;  //for redis cache

        public NoteController(FundoosContext fundoosContext, INoteBL noteBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.fundoosContext = fundoosContext;
            this.noteBL = noteBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                await this.noteBL.AddNote(UserId, notePostModel);
                return this.Ok(new { success = true, message = "Note Added Successfully " });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ArchieveNote/{noteId}")]
        public async Task<ActionResult> IsArchieveNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == userId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to archieve note" });
                }
                await this.noteBL.ArchiveNote(userId, noteId);
                return this.Ok(new { success = true, message = "Note Archieved successfully" });
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
                await this.noteBL.ChangeColour(UserId, noteId, colour);
                return this.Ok(new { success = true, message = "Note Colour Changed Successfully " });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("Delete/{noteId}")]
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == UserId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Deletion Failed" });
                }
                await this.noteBL.DeleteNote(noteId, UserId);
                return this.Ok(new { success = true, message = "Note Deleted Successfully" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpPut("Update/{noteId}")]
        public async Task<ActionResult> UpdateNote(int noteId, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == UserId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to Update note" });
                }
                await this.noteBL.UpdateNote(UserId, noteId, noteUpdateModel);
                return this.Ok(new { success = true, message = "Note Updated successfully" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpPut("PinNote/{noteId}")]
        public async Task<ActionResult> IsPinNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == userId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to Pin note" });
                }
                await this.noteBL.ArchiveNote(userId, noteId);
                return this.Ok(new { success = true, message = "Note Pinned successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("TrashNote/{noteId}")]
        public async Task<ActionResult> IsTrashNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == userId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to Trash note" });
                }
                await this.noteBL.ArchiveNote(userId, noteId);
                return this.Ok(new { success = true, message = "Note Trash successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("Reminder/{noteId}/{Reminderdate}")]
        public async Task<ActionResult> ReminderDate(int noteId,DateTime Reminderdate)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);

                var note = fundoosContext.Notes.FirstOrDefault(e => e.userId == userId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Reminder Date does not set successfully" });
                }
                await this.noteBL.Reminder(userId, noteId, Reminderdate);
                return this.Ok(new { success = true, message = "Reminder date set successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
        [Authorize]
        [HttpGet("GetAllNotes")]
        public async Task<ActionResult> GetAllNote()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                List<Note> result = new List<Note>();
                result = await this.noteBL.GetAllNote(userId);
                return this.Ok(new { success = true, message = $"here is your all notes", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllNotesByRedis")]
        public async Task<ActionResult> GetAllNotesByRedis()
        {
            try
            {
                
                string key = "Ankita";
                string serializeNoteList;

                var noteList = new List<Note>();
                var redisNoteList = await distributedCache.GetAsync(key);
                if (redisNoteList != null)
                {
                    serializeNoteList = Encoding.UTF8.GetString(redisNoteList);
                    noteList = JsonConvert.DeserializeObject<List<Note>>(serializeNoteList);
                }
                else
                {
                    var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                    int userId = Int32.Parse(userid.Value);
                    noteList = await this.noteBL.GetAllNote(userId);
                    serializeNoteList = JsonConvert.SerializeObject(noteList);
                    redisNoteList = Encoding.UTF8.GetBytes(serializeNoteList);
                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    await distributedCache.SetAsync(key, redisNoteList, option);
                }
                return this.Ok(new { success = true, message = "Get note successful!!!", data = noteList });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}      

