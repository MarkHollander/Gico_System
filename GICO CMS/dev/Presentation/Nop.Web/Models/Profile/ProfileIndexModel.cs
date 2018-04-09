﻿using Nop.Web.Framework.Mvc.Models;

namespace Nop.Web.Models.Profile
{
    public partial class ProfileIndexModel : BaseNopModel
    {
        public string CustomerProfileId { get; set; }
        public string ProfileTitle { get; set; }
        public int PostsPage { get; set; }
        public bool PagingPosts { get; set; }
        public bool ForumsEnabled { get; set; }
    }
}