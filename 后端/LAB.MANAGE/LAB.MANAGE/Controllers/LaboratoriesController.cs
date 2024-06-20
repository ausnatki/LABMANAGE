using LAB.MODEL;
using LAB.MODEL.DoTempClass;
using LAB.SERVERS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAB.MANAGE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoriesController : ControllerBase
    {
        private readonly ILaboratoriesService laboratoriesService;

        public LaboratoriesController(ILaboratoriesService laboratoriesService)
        {
            this.laboratoriesService = laboratoriesService;
        }

        [HttpGet("GetALlList")]
        public LAB.MODEL.ApiResp GetALlList()
        {
            var result = new LAB.MODEL.ApiResp();
            try
            {
                result.Data = laboratoriesService.GetAllList();
                result.Msg = "获取列表成功";
                result.Result = true;
                result.Code = 20000;
                return result;
            }
            catch
            {
                result.Msg = "获取列表失败";
                result.Result = false;
                result.Code = 501;
                return result;
            }
        }

        [HttpPost("AddLab")]
        public LAB.MODEL.ApiResp AddLab([FromBody] LAB.MODEL.DoTempClass.LaboratoriesAdd laboratoriesAdd)
        {
            var result = new LAB.MODEL.ApiResp();
            if (laboratoriesService.AddLab(laboratoriesAdd))
            {
                result.Code = 20000;
                result.Msg = "添加实验室成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "添加实验室失败";
                result.Result = false;
                return result;
            }
        }

        [HttpPost("EditLab")]
        public LAB.MODEL.ApiResp EditLab([FromBody] LAB.MODEL.Laboratories laboratories)
        {
            var result = new LAB.MODEL.ApiResp();
            if (laboratoriesService.EditLab(laboratories))
            {
                result.Code = 20000;
                result.Msg = "修改实验室成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "修改实验室失败";
                result.Result = false;
                return result;
            }
        }

        [HttpPost("ChangeState")]
        public LAB.MODEL.ApiResp ChangeState(int LID)
        {
            var result = new LAB.MODEL.ApiResp();
            if (laboratoriesService.ChangeState(LID))
            {
                result.Code = 20000;
                result.Msg = "修改状态成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "修改状态失败";
                result.Result = false;
                return result;
            }

        }

        [HttpGet("GetByFloor")]
        public LAB.MODEL.ApiResp GetByFloor(int FID)
        {
            var result = new LAB.MODEL.ApiResp();
            try
            {
                result.Data = laboratoriesService.GetByFloor(FID);
                result.Msg = "查询成功";
                result.Result = true;
                result.Code = 20000;
                return result;
            }
            catch
            {
                result.Msg = "查询列表失败";
                result.Code = 501;
                result.Result = false;
                return result;
            }
        }

        [HttpPost("AssignMent")]
        public LAB.MODEL.ApiResp AssignMent(int LID, int UID)
        {
            var result = new LAB.MODEL.ApiResp();
            if (LID == 0 || UID == 0)
            {
                result.Code = 501;
                result.Msg = "数据传输错误";
                result.Result = true;
                return result;
            }

            if (laboratoriesService.AssignMent(LID, UID))
            {
                result.Code = 20000;
                result.Msg = "分配成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "服务器错误";
                result.Result = true;
                return result;
            }
        }
    }
}
