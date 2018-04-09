﻿using System;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Web.Models.News
{
    public partial class NewsCommentModel : BaseNopEntityModel
    {
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAvatarUrl { get; set; }

        public string CommentTitle { get; set; }

        public string CommentText { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool AllowViewingProfiles { get; set; }
    }
}