using CommentManagement.Data.Models;
using CommentManagement.Data.Models.DataContracts;
using CommentManagement.Data.Models.DataContracts.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Services
{
    public class CommentsService : ICommentService
    {
        MongoClient _mongoClient;
        IMongoDatabase _mongoDatabase;

        public void ConnectToDB(ICommentsDataBaseSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(settings.DatabaseName);
        }

        public List<MessageThreads> GetComments(GetCommentsRequest commentsRequest, ICommentsDataBaseSettings settings)
        {
            ConnectToDB(settings);
            if (commentsRequest.IsPagination)
            {
                String queryString = String.Format(MongoDbQuery.Pagination, commentsRequest.LastCommentID) +
                                     String.Format(MongoDbQuery.PostID, commentsRequest.PostID) +
                                     String.Format(MongoDbQuery.RootId, 0); 
                queryString = String.Format(MongoDbQuery.AndQuery, queryString);
                return _mongoDatabase.GetCollection<MessageThreads>("messagethreads").Find(queryString).Limit(10).ToList<MessageThreads>();
            }else
            {
                String queryString = String.Format(MongoDbQuery.PostID, commentsRequest.PostID) +
                                     String.Format(MongoDbQuery.RootId, 0);
                queryString = String.Format(MongoDbQuery.AndQuery, queryString);
                return _mongoDatabase.GetCollection<MessageThreads>("messagethreads").Find(queryString).Sort("{createdDate:-1}").Limit(10).ToList<MessageThreads>();
            }
            
        }

        public void PostComment(AddCommentRequest addComment, ICommentsDataBaseSettings settings)
        {
            ConnectToDB(settings);
            addComment.ToUserID = addComment.ToUserID.Length > 0 ? addComment.ToUserID : String.Empty;
            addComment.ToUserName = addComment.ToUserName.Length > 0 ? addComment.ToUserName : String.Empty;
            AddComment comment = new AddComment(addComment.ParentID, addComment.FromUserID, addComment.ToUserID, 
                addComment.FromUserName, addComment.ToUserName, addComment.Comment, addComment.PostID, addComment.RootID, DateTime.Parse(addComment.CreatedDate));
            
            _mongoDatabase.GetCollection<AddComment>("messagethreads").InsertOne(comment);

        }

        public void EditComment(EditCommentRequest request, ICommentsDataBaseSettings settings)
        {
            ConnectToDB(settings);
            String queryString = String.Format(MongoDbQuery.ParentID, request.CommentID);
            if (_mongoDatabase.GetCollection<MessageThreads>("messageThreads").Find(queryString).CountDocumentsAsync().Result == 0)
            {
                var builder = Builders<AddComment>.Filter;
               
                var filter = builder.Eq(x => x.FromUserID, "3");
                var builderUpdate = Builders<AddComment>.Update.Set(x => x.Comment, request.Comment);
                var res = _mongoDatabase.GetCollection<AddComment>("messageThreads").UpdateOne(filter, builderUpdate);
            }
        }

        public List<MessageThreads> GetReplies(GetSubCommentsRequest commentsRequest, ICommentsDataBaseSettings settings)
        {
            ConnectToDB(settings);
            if (commentsRequest.IsPagination)
            {
                String queryString = String.Format(MongoDbQuery.Pagination, commentsRequest.LastCommentID) +
                                     String.Format(MongoDbQuery.PostID, commentsRequest.PostID) +
                                     String.Format(MongoDbQuery.RootId, commentsRequest.RootID);
                queryString = String.Format(MongoDbQuery.AndQuery, queryString);
                return _mongoDatabase.GetCollection<MessageThreads>("messagethreads").Find(queryString).Limit(10).ToList<MessageThreads>();
            }
            else
            {
                String queryString = String.Format(MongoDbQuery.PostID, commentsRequest.PostID) +
                                     String.Format(MongoDbQuery.RootId, commentsRequest.RootID);
                queryString = String.Format(MongoDbQuery.AndQuery, queryString);
                return _mongoDatabase.GetCollection<MessageThreads>("messagethreads").Find(queryString).Sort("{createdDate:-1}").Limit(10).ToList<MessageThreads>();
            }
        }
    }
}