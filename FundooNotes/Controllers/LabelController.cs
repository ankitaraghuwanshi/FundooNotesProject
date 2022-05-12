using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
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
       
    }
}
