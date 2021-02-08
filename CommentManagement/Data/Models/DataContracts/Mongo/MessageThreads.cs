using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.DataContracts
{
    [BsonIgnoreExtraElements]
    public class MessageThreads
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("parentID")]
        public String ParentID { get; set; }

        [BsonElement("fromUser")]
        public String FromUserID { get; set; }

        [BsonElement("toUser")]
        public String ToUserID { get; set; }

        [BsonElement("fromUserName")]
        public String FromUserName { get; set; }

        [BsonElement("toUserName")]
        public String ToUserName { get; set; }

        [BsonElement("comment")]
        public String Comment { get; set; }

        [BsonElement("postID")]
        public String PostID { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

    }
}
