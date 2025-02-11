using QX_Frame.App.WebApi;
using QX_Frame.Data.DTO;
using QX_Frame.Data.Entities;
using QX_Frame.Data.Options;
using QX_Frame.Data.QueryObject;
using QX_Frame.Data.Service;
using QX_Frame.Data.Service.QX_Frame;
using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Extends;
using QX_Frame.WebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace QX_Frame.WebAPI.Controllers
{
    /// <summary>
    /// copyright qixiao code builder ->
    /// version:4.2.0
    /// author:qixiao(柒小)
    /// time:2017-04-14 16:50:49
    /// </summary>

    /// <summary>
    ///class ComplainController
    /// </summary>
    public class ComplainController : WebApiControllerBase
    {
        // GET: api/Complain //queryId==-1 query all user,queryId==1 query by loginId
        public IHttpActionResult Get(int queryId,int complainStatusId, string loginId, int pageIndex, int pageSize, bool isDesc)
        {
            tb_ComplainQueryObject queryObject = new tb_ComplainQueryObject();

            queryObject.queryId = queryId;
            queryObject.complainStatusId = complainStatusId;
            queryObject.complainUserUid = UserController.GetUserAccountInfoByLoginIdAllowNull(loginId) != null ? UserController.GetUserAccountInfoByLoginIdAllowNull(loginId).uid : default(Guid);
            queryObject.PageIndex = pageIndex;
            queryObject.PageSize = pageSize;
            queryObject.IsDESC = isDesc;

            using (var fact = Wcf<ComplainService>())
            {
                int count = 0;
                var channel = fact.CreateChannel();
                List<tb_Complain> resultList = channel.QueryAllPaging<tb_Complain, DateTime>(queryObject, t => t.complainTime).Cast<List<tb_Complain>>(out count);
                List<ComplainViewModel> complainViewModelList = new List<ComplainViewModel>();
                foreach (var item in resultList)
                {
                    ComplainViewModel complain = new ComplainViewModel();
                    complain.complainUid = item.complainUid;
                    complain.complainContent = item.complainContent;
                    complain.complainUserUid = item.complainUserUid;
                    complain.complainTime = item.complainTime.ToDateTimeString_24HourType();
                    complain.complainStatusId = item.complainStatusId;
                    complain.complainStatusName = item.tb_ComplainStatus.complainStatusName;
                    complainViewModelList.Add(complain);
                }
                return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("get complain list query by messagePushStatus", complainViewModelList, count));
            }
        }
        // GET: api/Complain/id
        public IHttpActionResult Get(string id)
        {
            Guid complainuid = Guid.Parse(id);
            using (var fact = Wcf<ComplainService>())
            {
                var channel = fact.CreateChannel();
                tb_Complain complain = channel.QuerySingle(new tb_ComplainQueryObject { QueryCondition = t => t.complainUid == complainuid }).Cast<tb_Complain>();
                ComplainViewModel result = new ComplainViewModel();
                if (complain != null)
                {
                    result.complainUid = complain.complainUid;
                    result.complainContent = complain.complainContent;
                    result.complainUserUid = complain.complainUserUid;
                    result.complainTime = complain.complainTime.ToDateTimeString_24HourType("-");
                    result.complainStatusId = complain.complainStatusId;
                    result.complainStatusName = complain.tb_ComplainStatus.complainStatusName;
                }
                return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("get complain by complainUid = id", result, 1));
            }
        }

        // POST: api/Complain
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            using (var fact = Wcf<ComplainService>())
            {
                var channel = fact.CreateChannel();
                tb_Complain complain = new tb_Complain();
                complain.complainUid = Guid.NewGuid();
                complain.complainContent = query.complainContent;
                int appKey = query.appKey;
                string token = query.token;
                complain.complainUserUid = AuthenticationController.GetTokenInfoByAppKeyToken(appKey, token).Item1;
                complain.complainTime = DateTime.Now;
                complain.complainStatusId = opt_ComplainStatus.未处理.ToInt();
                channel.Add(complain);
            }
            return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("add succeed"));
        }

        // PUT: api/Complain/id
        public IHttpActionResult Put(string id,[FromBody]dynamic query)
        {
            Guid complainuid = Guid.Parse(id);
            using (var fact = Wcf<ComplainService>())
            {
                var channel = fact.CreateChannel();
                tb_Complain complain = channel.QuerySingle(new tb_ComplainQueryObject { QueryCondition = t => t.complainUid == complainuid }).Cast<tb_Complain>();
                ComplainViewModel result = new ComplainViewModel();
                if (complain != null)
                {
                    complain.complainStatusId = opt_ComplainStatus.已处理.ToInt();
                }
                channel.Update(complain);
                return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("update succeed"));
            }
        }

        // DELETE: api/Complain
        public IHttpActionResult Delete([FromBody]dynamic query)
        {
            Guid uid = Guid.Parse(query.complainUid);
            using (var fact = Wcf<ComplainService>())
            {
                var channel = fact.CreateChannel();
                tb_Complain result = channel.QuerySingle(new tb_ComplainQueryObject { QueryCondition = t => t.complainUid == uid }).Cast<tb_Complain>();
                if (result == null)
                {
                    throw new Exception_DG("no result found by this query condition", 3021);
                }
                channel.Delete(result);
                return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("delete succeed"));
            }
        }
    }
}
