using LAB.SERVERS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace LAB.MANAGE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly LAB.SERVERS.IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        /// <summary>
        /// 获取所有楼层列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllList")]
        public LAB.MODEL.ApiResp GetAllList()
        {
            var result = new LAB.MODEL.ApiResp();
            try 
            {
                result.Code = 20000;
                result.Data = _floorService.GetAllList();
                result.Result = true;
                result.Msg = "获取列表成功";
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


        /// <summary>
        /// 改变楼层状态
        /// </summary>
        /// <param name="FID">楼层id</param>
        /// <returns></returns>
        [HttpPost("ChangeState")]
        public LAB.MODEL.ApiResp ChangeState(int FID)
        {
            var result = new LAB.MODEL.ApiResp();
            if(FID == 0 || !_floorService.ChangeState(FID))
            {
                result.Code = 501;
                result.Msg = "状态改变失败";
                result.Result = false;
                return result;
            }else
            {
                result.Code = 20000;
                result.Msg = "状态改变成功";
                result.Result = true;
                return result;
            }
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="BID">房楼id 根据房楼的层数自动往上面加一层</param>
        /// <returns></returns>
        [HttpPost("AddFloor")]
        public LAB.MODEL.ApiResp AddFloor(int BID) 
        {
            var result = new LAB.MODEL.ApiResp();
            if (BID == 0 || !_floorService.AddFloor(BID))
            {
                result.Code = 501;
                result.Msg = "添加失败";
                result.Result = false;
                return result;
            }
            else
            {
                result.Code = 20000;
                result.Msg = "添加成功";
                result.Result = true;
                return result;
            }
        }

    }
}
