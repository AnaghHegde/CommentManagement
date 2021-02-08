using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.DataContracts.Mongo
{
    [BsonIgnoreExtraElements]
    public class UpdateComment 
    {

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

        [BsonElement("rootID")]
        public String RootID { get; set; }

        public UpdateComment(String parentID, String fromUserID, String toUserID, String fromUserName,
           String toUserName, String comment, String postID, String rootID, DateTime createdDate)
        {
            
            ParentID = parentID;
            FromUserID = fromUserID;
            ToUserID = toUserID;
            FromUserName = fromUserName;
            ToUserName = toUserName;
            Comment = comment;
            PostID = postID;
            CreatedDate = createdDate;
            RootID = rootID;
        }
    }
}
