﻿using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Services.Customers;
using Nop.Services.Forums;
using Nop.Services.Helpers;
using Nop.Web.Models.Common;
using Nop.Web.Models.PrivateMessages;

namespace Nop.Web.Factories
{
    /// <summary>
    /// Represents the private message model factory
    /// </summary>
    public partial class PrivateMessagesModelFactory : IPrivateMessagesModelFactory
    {
        #region Fields

        private readonly IForumService _forumService;
        private readonly IWorkContext _workContext;
        
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ForumSettings _forumSettings;
        private readonly CustomerSettings _customerSettings;

        #endregion

        #region Ctor

        public PrivateMessagesModelFactory(IForumService forumService,
            IWorkContext workContext, 
            
            IDateTimeHelper dateTimeHelper,
            ForumSettings forumSettings,
            CustomerSettings customerSettings)
        {
            this._forumService = forumService;
            this._workContext = workContext;
            
            this._dateTimeHelper = dateTimeHelper;
            this._forumSettings = forumSettings;
            this._customerSettings = customerSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare the private message index model
        /// </summary>
        /// <param name="page">Number of items page; pass null to disable paging</param>
        /// <param name="tab">Tab name</param>
        /// <returns>Private message index model</returns>
        public virtual PrivateMessageIndexModel PreparePrivateMessageIndexModel(int? page, string tab)
        {
            var inboxPage = 0;
            var sentItemsPage = 0;
            var sentItemsTabSelected = false;

            switch (tab)
            {
                case "inbox":
                    if (page.HasValue)
                    {
                        inboxPage = page.Value;
                    }
                    break;
                case "sent":
                    if (page.HasValue)
                    {
                        sentItemsPage = page.Value;
                    }
                    sentItemsTabSelected = true;
                    break;
                default:
                    break;
            }

            var model = new PrivateMessageIndexModel
            {
                InboxPage = inboxPage,
                SentItemsPage = sentItemsPage,
                SentItemsTabSelected = sentItemsTabSelected
            };

            return model;
        }

        /// <summary>
        /// Prepare the inbox model
        /// </summary>
        /// <param name="page">Number of items page</param>
        /// <param name="tab">Tab name</param>
        /// <returns>Private message list model</returns>
        public virtual PrivateMessageListModel PrepareInboxModel(int page, string tab)
        {
            if (page > 0)
            {
                page -= 1;
            }

            var pageSize = _forumSettings.PrivateMessagesPageSize;

            var messages = new List<PrivateMessageModel>();

            var list = _forumService.GetAllPrivateMessages(0,
                string.Empty, _workContext.CurrentCustomer.CustomerId, null, null, false, string.Empty, page, pageSize);
            foreach (var pm in list)
                messages.Add(PreparePrivateMessageModel(pm));

            var pagerModel = new PagerModel
            {
                PageSize = list.PageSize,
                TotalRecords = list.TotalCount,
                PageIndex = list.PageIndex,
                ShowTotalSummary = false,
                RouteActionName = "PrivateMessagesPaged",
                UseRouteLinks = true,
                RouteValues = new PrivateMessageRouteValues { pageNumber = page, tab = tab }
            };

            var model = new PrivateMessageListModel
            {
                Messages = messages,
                PagerModel = pagerModel
            };

            return model;
        }

        /// <summary>
        /// Prepare the sent model
        /// </summary>
        /// <param name="page">Number of items page</param>
        /// <param name="tab">Tab name</param>
        /// <returns>Private message list model</returns>
        public virtual PrivateMessageListModel PrepareSentModel(int page, string tab)
        {
            if (page > 0)
            {
                page -= 1;
            }

            var pageSize = _forumSettings.PrivateMessagesPageSize;

            var messages = new List<PrivateMessageModel>();

            var list = _forumService.GetAllPrivateMessages(0,
                _workContext.CurrentCustomer.CustomerId, string.Empty, null, false, null, string.Empty, page, pageSize);
            foreach (var pm in list)
                messages.Add(PreparePrivateMessageModel(pm));

            var pagerModel = new PagerModel
            {
                PageSize = list.PageSize,
                TotalRecords = list.TotalCount,
                PageIndex = list.PageIndex,
                ShowTotalSummary = false,
                RouteActionName = "PrivateMessagesPaged",
                UseRouteLinks = true,
                RouteValues = new PrivateMessageRouteValues { pageNumber = page, tab = tab }
            };

            var model = new PrivateMessageListModel
            {
                Messages = messages,
                PagerModel = pagerModel
            };

            return model;
        }

        /// <summary>
        /// Prepare the send private message model
        /// </summary>
        /// <param name="customerTo">Customer, recipient of the message</param>
        /// <param name="replyToPM">Private message, pass if reply to a previous message is need</param>
        /// <returns>Send private message model</returns>
        public virtual SendPrivateMessageModel PrepareSendPrivateMessageModel(Customer customerTo, PrivateMessage replyToPM)
        {
            if (customerTo == null)
                throw new ArgumentNullException(nameof(customerTo));

            var model = new SendPrivateMessageModel
            {
                ToCustomerId = customerTo.CustomerId,
                CustomerToName = customerTo.FormatUserName(),
                AllowViewingToProfile = _customerSettings.AllowViewingProfiles && !customerTo.IsGuest()
            };

            if (replyToPM == null)
                return model;

            if (replyToPM.ToCustomerId == _workContext.CurrentCustomer.CustomerId ||
                replyToPM.FromCustomerId == _workContext.CurrentCustomer.CustomerId)
            {
                model.ReplyToMessageId = replyToPM.Id;
                model.Subject = $"Re: {replyToPM.Subject}";
            }

            return model;
        }

        /// <summary>
        /// Prepare the private message model
        /// </summary>
        /// <param name="pm">Private message</param>
        /// <returns>Private message model</returns>
        public virtual PrivateMessageModel PreparePrivateMessageModel(PrivateMessage pm)
        {
            if (pm == null)
                throw new ArgumentNullException(nameof(pm));

            var model = new PrivateMessageModel
            {
                Id = pm.Id,
                FromCustomerId = pm.FromCustomer.CustomerId,
                CustomerFromName = pm.FromCustomer.FormatUserName(),
                AllowViewingFromProfile = _customerSettings.AllowViewingProfiles && pm.FromCustomer != null && !pm.FromCustomer.IsGuest(),
                ToCustomerId = pm.ToCustomer.CustomerId,
                CustomerToName = pm.ToCustomer.FormatUserName(),
                AllowViewingToProfile = _customerSettings.AllowViewingProfiles && pm.ToCustomer != null && !pm.ToCustomer.IsGuest(),
                Subject = pm.Subject,
                Message = pm.FormatPrivateMessageText(),
                CreatedOn = _dateTimeHelper.ConvertToUserTime(pm.CreatedOnUtc, DateTimeKind.Utc),
                IsRead = pm.IsRead,
            };

            return model;
        }

        #endregion
    }
}
