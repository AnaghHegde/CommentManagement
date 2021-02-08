using CommentManagement.Data.Models;
using CommentManagement.Data.Models.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Services
{
    public interface ICommentService
    {
        void PostComment(AddCommentRequest addComment, ICommentsDataBaseSettings settings);

        List<MessageThreads> GetComments(GetCommentsRequest commentsRequest, ICommentsDataBaseSettings settings);

        void EditComment(EditCommentRequest request, ICommentsDataBaseSettings settings);

        List<MessageThreads> GetReplies(GetSubCommentsRequest commentsRequest, ICommentsDataBaseSettings settings);
    }
}
