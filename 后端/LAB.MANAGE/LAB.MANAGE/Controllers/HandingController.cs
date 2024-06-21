using LAB.SERVERS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAB.MANAGE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandingController : ControllerBase
    {
        private readonly IHandingService handingService;

        public HandingController(IHandingService handingService)
        {
            this.handingService = handingService;
        }

        #region 获取所有异常列表
        [HttpGet("GetAllHanding")]
        public LAB.MODEL.ApiResp GetAllHanding()
        {
            var result= new LAB.MODEL.ApiResp();
            try 
            {
                result.Code = 20000;
                result.Data= handingService.GetAllHanding();
                result.Msg = "获取列表成功";
                result.Result = true;
                return result;
            }
            catch 
            {
                result.Code = 501;
                result.Msg = "获取列表失败";
                result.Result = false;
                return result;
            }
        }
        #endregion

        #region 通过实验室管理人员获取异常列表
        [HttpGet("GetHandingByUser")]
        public LAB.MODEL.ApiResp GetHandingByUser(int UID)
        {
            var result = new LAB.MODEL.ApiResp();
            try
            {
                result.Code = 20000;
                result.Data = handingService.GetHandingByUser(UID);
                result.Msg = "获取列表成功";
                result.Result = true;
                return result;
            }
            catch
            {
                result.Code = 501;
                result.Msg = "获取列表失败";
                result.Result = false;
                return result;
            }
        }
        #endregion

        #region 分配维修人员
        [HttpPost("AssignRepair")]
        public LAB.MODEL.ApiResp AssignRepair(int HID,int UID)
        {
            var result = new LAB.MODEL.ApiResp();
            if (handingService.AssignRepair(HID, UID)) 
            {
                result.Code = 20000;
                result.Msg = "分配人员成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "分配人员失败";
                result.Result = false;
                return result;
            }
        }

        #endregion
    }
}
