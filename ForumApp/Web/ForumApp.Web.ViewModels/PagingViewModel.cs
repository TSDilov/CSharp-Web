﻿namespace ForumApp.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumer => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.TopicsCount / 12);

        public int TopicsCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
