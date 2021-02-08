using CommentManagement.Data.Models;
using CommentManagement.Data.Models.DataContracts;
using CommentManagement.Data.Models.DataContracts.Mongo;
using CommentManagement.Data.Models.Query;
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
            QueryStringHelper helper = new QueryStringHelper();
            String query = helper.GetCommentsQueryString(commentsRequest);
            if (commentsRequest.IsSortByLatest)
            {
                return _mongoDatabase.GetCollection<MessageThreads>("messagethreads").Find(query).Sort("{createdDate:-1}").Limit(10).ToList<MessageThreads>();
            }else
            {
                return _mongoDatabase.GetCollection<MessageThreads>("messagethreads").Find(query).Sort("{createdDate:1}").Limit(10).ToList<MessageThreads>();
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
            String queryString = String.Format(MongoDbQuery.ParentID, request.Id);
            if (_mongoDatabase.GetCollection<AddComment>("messageThreads").Find(queryString).CountDocumentsAsync().Result == 0)
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(request.Id));
                var builderUpdate = Builders<BsonDocument>.Update.Set("comment", request.Comment);
                /*var updList = new List<UpdateDefinition<AddComment>>
                {
                     Builders<AddComment>.Update.Set(x => x.Comment, request.Comment)
                };
                var update = Builders<AddComment>.Update.Combine(updList);*/
                var collection = _mongoDatabase.GetCollection<BsonDocument>("messageThreads");
                var res = collection.UpdateOne(filter, builderUpdate);
            }
        }

        public List<MessageThreads> GetReplies(GetSubCommentsRequest commentsRequest, ICommentsDataBaseSettings settings)
        {
            ConnectToDB(settings);
            QueryStringHelper helper = new QueryStringHelper();
            String query = helper.GetCommentsQueryString(commentsRequest);
            if (commentsRequest.IsSortByLatest)
            {
                return _mongoDatabase.GetCollection<MessageThreads>("messagethreads").Find(query).Sort("{createdDate:-1}").Limit(10).ToList<MessageThreads>();
            }
            else
            {
                return _mongoDatabase.GetCollection<MessageThreads>("messagethreads").Find(query).Sort("{createdDate:1}").Limit(10).ToList<MessageThreads>();
            }
        }
    }
}