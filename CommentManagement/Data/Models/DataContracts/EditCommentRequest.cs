using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.DataContracts
{
    public class EditCommentRequest : AddCommentRequest
    {
        public String Id { get; set; }

        public int CommentId { get; set; }

    }
}
