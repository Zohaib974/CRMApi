﻿using System;
using System.Collections.Generic;

namespace CRMModels.Common
{
    public class CommmonListResponse<T> where T : class
    {
        public CommmonListResponse(List<T> list, int count, int pageNumber, int pageSize)
        {
            items = list;
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        public List<T> items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
