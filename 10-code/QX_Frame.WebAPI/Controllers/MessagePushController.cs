using QX_Frame.App.Web;
using QX_Frame.Data.DTO;
using QX_Frame.Data.Entities;
using QX_Frame.Data.Options;
using QX_Frame.Data.QueryObject;
using QX_Frame.Data.Service;
using QX_Frame.Data.Service.QX_Frame;
using QX_Frame.Helper_DG_Framework;
using QX_Frame.Helper_DG_Framework.Extends;
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
    /// time:2017-04-14 12:55:03
    /// </summary>

    /// <summary>
    ///class MessagePushController
    /// </summary>
    public class MessagePushController : WebApiControllerBase
    {
        // GET: api/MessagePush messagePushStatusId 0=未读 1=已读 2=全部 loginId="" 全部
        public IHttpActionResult Get(int messagePushStatusId, string loginId, int pageIndex, int pageSize, bool isDesc)
        {
            tb_MessagePushQueryObject queryObject = new tb_MessagePushQueryObject();

            queryObject.messagePushStatusId = messagePushStatusId;
            queryObject.pushToUserUid = UserController.GetUserAccountInfoByLoginIdAllowNull(loginId)!=null? UserController.GetUserAccountInfoByLoginIdAllowNull(loginId).uid:default(Guid);
            queryObject.PageIndex = pageIndex;
            queryObject.PageSize = pageSize;
            queryObject.IsDESC = isDesc;

            using (var fact = Wcf<MessagePushService>())
            {
                int count = 0;
                var channel = fact.CreateChannel();
                List<tb_MessagePush> resultList = channel.QueryAllPaging<tb_MessagePush, DateTime>(queryObject, t => t.messagePushTime).Cast<List<tb_MessagePush>>(out count);
                List<MessagePushViewModel> messagePushList = new List<MessagePushViewModel>();
                foreach (var item in resultList)
                {
                    MessagePushViewModel messagePushViewModel = new MessagePushViewModel();
                    messagePushViewModel.messageUid = item.messageUid;
                    messagePushViewModel.messageContent = item.messageContent;
                    messagePushViewModel.messagePusher = item.messagePusher;
                    messagePushViewModel.messagePushTime = item.messagePushTime;
                    messagePushViewModel.messageCategoryId = item.messageCategoryId;
                    messagePushViewModel.messagePushCategory = item.tb_MessagePushCategory;
                    messagePushViewModel.messagePushStatusId = item.messagePushStatusId;
                    messagePushViewModel.messagePushStatus = item.tb_MessagePushStatus;
                    messagePushViewModel.pushToUserUid = item.pushToUserUid;

                    messagePushList.Add(messagePushViewModel);
                }
                return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("get messagePushList query by messagePushStatus", messagePushList, count));
            }
        }

        // GET: api/MessagePush/id
        public IHttpActionResult Get(string id)
        {
            Guid uid = Guid.Parse(id);
            using (var fact = Wcf<MessagePushService>())
            {
                var channel = fact.CreateChannel();
                tb_MessagePush result = channel.QuerySingle(new tb_MessagePushQueryObject { QueryCondition = t => t.messageUid == uid }).Cast<tb_MessagePush>();
                MessagePushViewModel messagePush = new MessagePushViewModel();
                if (result!=null)
                {
                    messagePush.messageUid = result.messageUid;
                    messagePush.messageContent = result.messageContent;
                    messagePush.messagePusher = result.messagePusher;
                    messagePush.messagePushTime = result.messagePushTime;
                    messagePush.messageCategoryId = result.messageCategoryId;
                    messagePush.messagePushCategory = result.tb_MessagePushCategory;
                    messagePush.messagePushStatusId = result.messagePushStatusId;
                    messagePush.messagePushStatus = result.tb_MessagePushStatus;
                    messagePush.pushToUserUid = result.pushToUserUid;
                }
                result.messagePushStatusId = opt_MessageStatus.已读.ToInt();
                channel.Update(result);
                return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("get messagePush query by messageUid", messagePush, 1));
            }
        }

        // POST: api/MessagePush
        public IHttpActionResult Post([FromBody]dynamic query)
        {
            if (query == null)
            {
                throw new Exception_DG("arguments must be provide", 1001);
            }

            using (var fact = Wcf<MessagePushService>())
            {
                var channel = fact.CreateChannel();
                tb_MessagePush messagePush = new tb_MessagePush();
                messagePush.messageUid = Guid.NewGuid();
                messagePush.messageContent = query.messageContent;
                messagePush.messagePusher = query.messagePusher;
                messagePush.messagePushTime = DateTime.Now;
                messagePush.messageCategoryId = query.messageCategoryId;
                messagePush.messagePushStatusId = opt_MessageStatus.未读.ToInt();
                string loginId = query.loginId;
                messagePush.pushToUserUid = UserController.GetUserAccountInfoByLoginId(loginId).uid;

                channel.Add(messagePush);
                return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("add succeed"));
            }
        }

        // PUT: api/MessagePush
        public IHttpActionResult Put([FromBody]dynamic query)
        {
            throw new Exception_DG("The interface is not available", 9999);
        }

        // DELETE: api/MessagePush
        public IHttpActionResult Delete([FromBody]dynamic query)
        {
            if (query == null)
            {
                throw new Exception_DG("arguments must be provide", 1001);
            }
            Guid uid = Guid.Parse(query.messagePushUid);
            using (var fact = Wcf<MessagePushService>())
            {
                var channel = fact.CreateChannel();
                tb_MessagePush result = channel.QuerySingle(new tb_MessagePushQueryObject { QueryCondition = t => t.messageUid == uid }).Cast<tb_MessagePush>();
                if (result==null)
                {
                    throw new Exception_DG("no result found by this query condition",3021);
                }
                channel.Delete(result);
                return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("delete succeed"));
            }
        }
    }
}