using LAB.SERVERS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace LAB.MANAGE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademyController : ControllerBase
    {
        private readonly LAB.SERVERS.IAcademyService _service;

        public AcademyController(IAcademyService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取所有学院信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetList")]
        public LAB.MODEL.ApiResp GetList()
        {
            var result = new LAB.MODEL.ApiResp();
            try 
            {
                result.Data = _service.GetList();
                result.Code = 20000;
                result.Msg = "获取列表成功";
                result.Result = true;
                return result;
            }
            catch
            {
                result.Code = 501;
                result.Result = false;
                result.Msg = "获取列表失败";
                return result;
            }
        }

        /// <summary>
        /// 添加学院信息
        /// </summary>
        /// <param name="academy"></param>
        /// <returns></returns>
        [HttpPost("AddAcademy")]
        public LAB.MODEL.ApiResp AddAcademy([FromBody]LAB.MODEL.Academy academy)
        {
            var result = new LAB.MODEL.ApiResp();
            if(_service.AddAcademy(academy))
            {
                result.Code = 20000;
                result.Msg = "添加成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Result = false;
                result.Msg = "添加失败";
                return result;
            }
        }
        
        /// <summary>
        /// 获取多选框列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSelectCheckList")]
        public LAB.MODEL.ApiResp GetSelectCheckList()
        {
            var result = new LAB.MODEL.ApiResp();
            try 
            {
                result.Data = _service.GetSelectCheckList();
                result.Code = 20000;
                result.Msg = "获取列表成功";
                result.Result = true;
                return result;
            }
            catch 
            {
                result.Code = 501;
                result.Result = false;
                result.Msg = "获取列表失败";
                return result;
            }
        }

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="CID">学院id</param>
        /// <returns></returns>
        [HttpPost("ChangeState")]
        public LAB.MODEL.ApiResp ChangeState(int CID)
        {
            var result = new LAB.MODEL.ApiResp();
            if (_service.ChangeState(CID))
            {
                result.Code = 20000;
                result.Msg = "改变状态成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Result = false;
                result.Msg = "改变状态失败";
                return result;
            }
        }

        /// <summary>
        /// 修改学院信息
        /// </summary>
        /// <param name="academy"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public LAB.MODEL.ApiResp Edit([FromBody] LAB.MODEL.Academy academy)
        {
            var result = new LAB.MODEL.ApiResp();
            if (_service.Edit(academy))
            {
                result.Code = 20000;
                result.Result = true;
                result.Msg = "修改成功";
                return result;
            }
            else
            {
                result.Code = 501;
                result.Result = false;
                result.Msg = "修改失败";
                return result;
            }
        }
    }
}
