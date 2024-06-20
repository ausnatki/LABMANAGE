using LAB.MODEL;
using LAB.SERVERS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace LAB.MANAGE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuilderController : ControllerBase
    {
        private readonly IBuilderService builderService;

        public BuilderController(IBuilderService builderService)
        {
            this.builderService = builderService;
        }

        [HttpGet("GetAllList")]
        public LAB.MODEL.ApiResp GetAllList()
        {
            var result= new LAB.MODEL.ApiResp();
            try 
            {
                result.Code = 20000;
                result.Data = builderService.GetAllList();
                result.Result = true;
                result.Msg = "查询列表数据成功";
                return result;
            }
            catch 
            {
                result.Code = 501;
                result.Msg = "查询列表失败";
                result.Result = false;
                return result;  
            }
        }

        [HttpPost("AddBuilder")]
        public LAB.MODEL.ApiResp AddBuilder([FromBody]LAB.MODEL.SingleBuilding singleBuilding)
        {
            var result = new LAB.MODEL.ApiResp();
            if (builderService.AddBuilder(singleBuilding))
            {
                result.Code = 20000;
                result.Msg = "添加楼层成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "添加楼层失败";
                result.Result = false;
                return result;
            }
        }

        [HttpPost("EditBuilder")]
        public LAB.MODEL.ApiResp EditBuilder([FromBody]LAB.MODEL.SingleBuilding singleBuilding)
        {
            var result = new LAB.MODEL.ApiResp();
            if (builderService.EditBuilder(singleBuilding))
            {
                result.Code = 20000;
                result.Msg = "修改楼层信息成功";
                result.Result = true;
                return result;
            }
            else
            {
                result.Code = 501;
                result.Msg = "修改楼层信息失败";
                result.Result = false;
                return result;
            }
        }

        [HttpPost("ChangeState")]
        public LAB.MODEL.ApiResp ChangeState(int BID)
        {
            var result = new LAB.MODEL.ApiResp();
            if (builderService.ChangeState(BID))
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
    }
}
