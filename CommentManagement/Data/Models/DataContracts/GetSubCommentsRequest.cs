using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.DataContracts
{
    public class GetSubCommentsRequest
    {
        public String PostID { get; set; }

        public String LastCommentID { get; set; }

        public String RootID { get; set; }

        public bool IsPagination { get; set; }

        public int PageID { get; set; }
    }
}
