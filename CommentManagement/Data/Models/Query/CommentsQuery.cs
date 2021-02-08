using CommentManagement.Data.Models.DataContracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.Query
{
    public class CommentsQuery
    {
        MongoClient _mongoClient;
        IMongoDatabase _mongoDatabase;

        public CommentsQuery(ICommentsDataBaseSettings settings)
        {
            _mongoClient = new MongoClient(settings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(settings.DatabaseName);
        }

        //public List<CommentsResponse> GetComments(GetCommentsRequest commentsRequest)
        //{

        //}

        public String GetTest(ICommentsDataBaseSettings settings)
        {
            return _mongoDatabase.GetCollection<UserInfo>("userInfo").Find("{userName:'Anagh'}").ToList<UserInfo>().ElementAt(0).UserName;
        }
    }
}
