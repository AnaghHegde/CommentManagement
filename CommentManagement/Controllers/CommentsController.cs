using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentManagement.Data.Models;
using CommentManagement.Data.Models.DataContracts;
using CommentManagement.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommentManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService _commentService;

        private ICommentsDataBaseSettings _commentsDataBaseSettings;

        public CommentsController(ICommentService commentService, ICommentsDataBaseSettings dataBaseSettings)
        {
            _commentService = commentService;
            _commentsDataBaseSettings = dataBaseSettings;
        }


        //On Page load fetches the comments
        [Route("getComments")]
        [HttpGet]
        public List<MessageThreads> GetComments(GetCommentsRequest commentsRequest)
        {
            return _commentService.GetComments(commentsRequest, _commentsDataBaseSettings);
        }

        //New comments and replies for existing comments use this method to store the data to db
        [Route("postComment")]
        [HttpPost]
        public String PostComment(AddCommentRequest request)
        {
            _commentService.PostComment(request, _commentsDataBaseSettings);
            return "Success";
        }

        [Route("editComment")]
        [HttpPost]
        public void EditComment(EditCommentRequest request)
        {
            _commentService.EditComment(request, _commentsDataBaseSettings);  
        }

        [Route("getSubComments")]
        [HttpPost]
        public List<MessageThreads> GetReplies(GetSubCommentsRequest commentsRequest)
        {
            return _commentService.GetReplies(commentsRequest, _commentsDataBaseSettings);
        }
    }
}
