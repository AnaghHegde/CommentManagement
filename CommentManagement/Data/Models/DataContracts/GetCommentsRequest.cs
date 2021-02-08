using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.DataContracts
{
    public class GetCommentsRequest
    {
        public String PostID { get; set; }

        public String LastCommentID { get; set; }

        public bool IsPagination { get; set; }

        public int PageID { get; set; }

        public List<String> Keywords {get; set;}

        public bool IsKeywords { get; set; }

        public bool IsSortByLatest { get; set; }

        public String StartDate { get; set; }

        public String EndDate { get; set; }

        public bool IsDateRange { get; set; }

        public String RootID { get; set; }
    }
}
