using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.DataContracts
{
    public class CommentsResponse
    {
        public String ParentID { get; set; }

        public String FromUserName { get; set; }

        public String ToUserName { get; set; }

        public String Comment { get; set; }

        public String PostID { get; set; }

        public String RootID { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
