using LAB.SERVERS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LAB.MANAGE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailSafetyCheckController : ControllerBase
    {
        private readonly LAB.SERVERS.IDailySafetyCheckService _service;

        public DailSafetyCheckController(IDailySafetyCheckService service)
        {
            _service = service;
        }

        #region 通过实验室id获取日志记录
        [HttpGet("GetByLab")]
        public LAB.MODEL.ApiResp GetByLab(int LID)
        {
            var result = new LAB.MODEL.ApiResp();
            try 
            {
                result.Data = _service.GetByLab(LID);
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
        #endregion

        #region 获取实验室的维修信息
        [HttpGet("GetRepairByLab")]
        public LAB.MODEL.ApiResp GetRepairByLab(int LID)
        {
            var reuslt = new LAB.MODEL.ApiResp();
            try 
            {
                reuslt.Code = 20000;
                reuslt.Msg = "获取列表成功";
                reuslt.Result = true;
                reuslt.Data = _service.GetRepairsByLab(LID);
                return reuslt;
            } 
            catch 
            {
                reuslt.Code = 501;
                reuslt.Msg = "获取列表失败";
                reuslt.Result = false;
                return reuslt;
            }
        }
        #endregion

        #region 获取实验室的异常日志
        [HttpGet("GetHandingByLab")]
        public LAB.MODEL.ApiResp GetHandingByLab(int LID) 
        {
            var result = new LAB.MODEL.ApiResp();
            try 
            {
                result.Data = _service.GetHandingByLab(LID);
                result.Code = 20000;
                result.Msg = "获取实验室日常日志";
                result.Result = true;
                return result;
            }
            catch
            {
                result.Code = 501;
                result.Msg = "获取实验室异常日志失败";
                result.Result = false;
                return result;
            }
        }
        #endregion

        #region 获取所有日志信息
        [HttpGet("GetAllDailyCheck")]
        public LAB.MODEL.ApiResp GetAllDailyCheck()
        {
            var result = new LAB.MODEL.ApiResp();
            try 
            {
                result.Data = _service.GetAllDailyCheck();
                result.Code = 20000;
                result.Msg = "获取日志列表成功";
                result.Result = true;
                return result;
            }
            catch
            {
                result.Code = 501;
                result.Msg = "获取日志列表失败";
                result.Result = false;
                return result;
            }
        }
        #endregion

        #region 改变日志状态
        [HttpPost("ChangeState")]
        public LAB.MODEL.ApiResp ChangeState(int DID) 
        {
            var result = new LAB.MODEL.ApiResp();
            if(DID == 0)
            {
                result.Code = 501;
                result.Msg = "传值错误";
                result.Result = false;
                return result;
            }

            if(_service.ChangeCheck(DID)) 
            {
                result.Code = 20000;
                result.Msg = "修改状态成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "修改失败";
                result.Result = false;
                return result;
            }
        }
        #endregion

        #region 添加检查日志
        [HttpPost("AddCheck")]
        public LAB.MODEL.ApiResp AddCheck([FromBody]LAB.MODEL.DailySafetyCheck check)
        {
            var result = new LAB.MODEL.ApiResp();
            if(check == null)
            {
                result.Code = 501;
                result.Msg = "传值错误";
                result.Result = false;
                return result;
            }

            if (_service.AddCheck(check))
            {
                result.Result=  true;
                result.Msg = "添加成功";
                result.Code = 20000;
                return result;
            }
            else 
            {
                result.Result = false;
                result.Msg = "添加失败";
                result.Code = 501;
                return result;
            }
        }
        #endregion

        #region 分配实验室维修人员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DHLID">日志异常记录表id</param>
        /// <param name="UID">维修人员id</param>
        /// <returns></returns>
        /// 
        [HttpPost("Assignmen")]
        public LAB.MODEL.ApiResp Assignmen(int DHLID,int UID)
        {
            var result = new LAB.MODEL.ApiResp();
            if(DHLID == 0 || UID == 0) 
            {
                result.Code = 501;
                result.Msg = "传值错误";
                result.Result = false;
                return result;
            }

            if (_service.Assignment(DHLID, UID))
            {
                result.Result = true;
                result.Msg = "添加成功";
                result.Code = 20000;
                return result;
            }
            else
            {
                result.Result = false;
                result.Msg = "添加失败";
                result.Code = 501;
                return result;
            }
        }
        #endregion

        #region 填写维修记录
        [HttpPost("UpdataRepairs")]
        public LAB.MODEL.ApiResp UpdataRepairs(LAB.MODEL.LabEquipmentRepairs repairs)
        {
            var result = new LAB.MODEL.ApiResp();
            if (repairs == null)
            {
                result.Code = 501;
                result.Msg = "传值错误";
                result.Result = false;
                return result;
            }

            if (_service.UpdataRepairs(repairs))
            {
                result.Result = true;
                result.Msg = "填写成功";
                result.Code = 20000;
                return result;
            }
            else
            {
                result.Result = false;
                result.Msg = "填写失败";
                result.Code = 501;
                return result;
            }
        }
        #endregion
    }
}
