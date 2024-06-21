using LAB.SERVERS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAB.MANAGE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairsController : ControllerBase
    {
        private readonly IRepairsService repairsService;

        public RepairsController(IRepairsService repairsService)
        {
            this.repairsService = repairsService;
        }

        #region 获取所有的维修信息
        [HttpGet("GetAllRepairs")]
        public LAB.MODEL.ApiResp GetAllRepairs()
        {
            var result= new LAB.MODEL.ApiResp();
            try 
            {
                result.Code = 20000;
                result.Msg = "获取列表成功";
                result.Result = true;
                result.Data = repairsService.GetAllRepairs();
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

        #region 通过维修人员获取维修信息
        [HttpGet("GetRepairsByUser")]
        public LAB.MODEL.ApiResp GetRepairsByUser(int UID)
        {
            var result = new LAB.MODEL.ApiResp();
            try
            {
                result.Code = 20000;
                result.Msg = "获取列表成功";
                result.Result = true;
                result.Data = repairsService.GetRepairsByUser(UID);
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

        #region 填写维修信息
        [HttpPost("UpdataRepairs")]
        public LAB.MODEL.ApiResp UpdataRepairs([FromBody]LAB.MODEL.LabEquipmentRepairs repairs)
        {
            var result = new LAB.MODEL.ApiResp();

            if (repairsService.UpdataRepairs(repairs))
            {
                result.Code = 20000;
                result.Msg = "填写成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "填写失败";
                result.Result = false;
                return result;
            }
           
        }
        #endregion
    }
}
