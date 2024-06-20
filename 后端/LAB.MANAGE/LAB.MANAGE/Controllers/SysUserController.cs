using LAB.SERVERS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace LAB.MANAGE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysUserController : ControllerBase
    {

        private readonly LAB.SERVERS.ISysUserService _service;

        public SysUserController(ISysUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllList")]
        public LAB.MODEL.ApiResp GetAllList()
        {
            var result = new LAB.MODEL.ApiResp();
            try
            {
                result.Data = _service.GetAllList();
                result.Code = 20000;
                result.Msg = "获取列表成功";
                result.Result = true;
                return result;
            }
            catch
            {
                result.Code = 510;
                result.Msg = "获取列表错误";
                result.Result = false;
                return result;
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="addUser"></param>
        /// <returns></returns>
        [HttpPost("AddUser")]
        public LAB.MODEL.ApiResp AddUser([FromBody]LAB.MODEL.DoTempClass.AddUser addUser)
        {
            var result = new LAB.MODEL.ApiResp();
            if (_service.AddUser(addUser)) 
            {
                result.Code = 20000;
                result.Msg = "添加成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "添加失败";
                result.Result = false;
                return result;
            }
        }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoleList")]
        public LAB.MODEL.ApiResp GetRoleList()
        {
            var result = new LAB.MODEL.ApiResp();
            try 
            {
                result.Data = _service.GetRoleList();
                result.Code = 20000;
                result.Msg = "获取成功";
                result.Result = true;
                return result;
            } 
            catch 
            {
                result.Code = 501;
                result.Msg = "获取角色列表失败";
                result.Result = false;
                return result;
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="editUser"></param>
        /// <returns></returns>
        [HttpPost("EditUser")]
        public LAB.MODEL.ApiResp EditUser([FromBody] LAB.MODEL.DoTempClass.AddUser editUser)
        {
            var result = new LAB.MODEL.ApiResp();
            if(_service.EditUser(editUser))
            {
                result.Code = 20000;
                result.Msg = "修改成功";
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

        [HttpGet("GetByAcademy")]
        public LAB.MODEL.ApiResp GetByAcademy(int CID)
        {
            var result = new LAB.MODEL.ApiResp();
            try 
            {
                result.Code = 20000;
                result.Msg = "获取成功";
                result.Result = true;
                result.Data = _service.GetByAcademy(CID);
                return result;
            }
            catch 
            {
                result.Code = 501;
                result.Msg = "获取成失败";
                result.Result = false;
                return result;
            }
        }
    }
}
