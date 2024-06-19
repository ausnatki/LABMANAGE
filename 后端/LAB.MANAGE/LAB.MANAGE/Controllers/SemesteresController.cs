using LAB.SERVERS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace LAB.MANAGE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesteresController : ControllerBase
    {
        private readonly LAB.SERVERS.ISemesteresService semesteresService;
        public SemesteresController(ISemesteresService semesteresService)
        {
            this.semesteresService = semesteresService;
        }

        /// <summary>
        /// 获取所有学期列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllList")]
        public LAB.MODEL.ApiResp GetAllList()
        {
            var result = new LAB.MODEL.ApiResp();
            try 
            {
                result.Data = semesteresService.GetAllList();
                result.Code = 20000;
                result.Msg = "获取列表信息成功";
                result.Result = true;
                return result;
            }
            catch 
            {
                result.Code = 501;
                result.Msg = "获取列表信息失败";
                result.Result = false;
                return result;
            }
        }

        /// <summary>
        /// 添加学期信息
        /// </summary>
        /// <param name="semesters">学期实体</param>
        /// <returns></returns>
        [HttpPost("AddSemesteres")]
        public LAB.MODEL.ApiResp AddSemesteres([FromBody]LAB.MODEL.Semesters semesters)
        {
            var result = new LAB.MODEL.ApiResp();
            if (semesteresService.AddSemesteres(semesters))
            {
                result.Code = 20000;
                result.Msg = "添加学期成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "添加学期失败";
                result.Result = false;
                return result;
            }
        }

        [HttpPost("EditSemesteres")]
        public LAB.MODEL.ApiResp EditSemesteres([FromBody]LAB.MODEL.Semesters semesters)
        {
            var result = new LAB.MODEL.ApiResp();
            if (semesteresService.EditSemesteres(semesters))
            {
                result.Code = 20000;
                result.Msg = "添加学期成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "添加学期失败";
                result.Result = false;
                return result;
            }
        }
    }
}
