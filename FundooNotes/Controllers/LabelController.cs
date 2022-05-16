using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LabelController : ControllerBase
    {
        FundoosContext fundoosContext;
        ILabelBL labelBL;
        public LabelController(FundoosContext fundoosContext,ILabelBL labelBL)
        {
            this.fundoosContext = fundoosContext;
            this.labelBL = labelBL;
        }

        [Authorize]
        [HttpPost("AddLabel/{userId}/{noteId}/{labelName}")]
        public async Task <ActionResult> AddLabel(int userId,int noteId,string labelName)
        {
            try
            {
                await this.labelBL.AddLabel(userId,noteId,labelName);
                return this.Ok(new { success = true, message = "Label Added Successfully" });

            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
        [Authorize]
        [HttpGet("GetLabelByuserId/{userId}")]
        public async Task<ActionResult> GetLabelByuserId()
        {
            try
            {
                List<Label> res = new List<Label>();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                res = await this.labelBL.GetLabelByuserId(userId);
                if (res == null)
                {
                    return this.BadRequest(new { success = false, message = "Soory!! this LabelID doesnt Exits " });
                }
                return this.Ok(new { success = true, message = $"get Label information successfully", data = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetlabelByNoteId/{NoteId}")]
        public async Task<ActionResult> GetLabelByNoteId(int NoteId)
        {
            try
            {
                List<Label> res = new List<Label>();

                res = await this.labelBL.GetLabelByNoteId(NoteId);
                if (res == null)
                {
                    return this.BadRequest(new { success = true, message = "Soory!! this LabelID doesnt Exits <Please> create it" });
                }
                return this.Ok(new { success = true, message = $"here is the Label information", data = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("UpdateLabel/{labelId}/{labelName}")]
        public async Task<ActionResult> UpdateLabel(int labelId,string labelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
               
                var result = await this.labelBL.UpdateLabel(userId, labelId, labelName);
                if (result == null)
                {
                    return this.BadRequest(new { success = true, message = "Soory!! this LabelID doesnt Exits <Please> create it" });
                }
                return this.Ok(new { success = true, message = $"Label updated successfully"});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        [Authorize]
        [HttpDelete("DeleteLabel/{labelId}")]
        public async Task<ActionResult> DeleteLabel(int labelId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.labelBL.DeleteLabel(labelId, userId);
                return this.Ok(new { success = true, message = $"Label Deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
