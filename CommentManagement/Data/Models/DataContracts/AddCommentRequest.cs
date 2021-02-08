using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.DataContracts
{
    public class AddCommentRequest
    {
        public String ParentID { get; set; }

        public String FromUserID { get; set; }

        public String ToUserID { get; set; }

        public String Comment { get; set; }

        public String PostID { get; set; }

        public String CreatedDate { get; set; }

        public String FromUserName { get; set; }

        public String ToUserName { get; set; }

        public String RootID { get; set; }
    }
}
