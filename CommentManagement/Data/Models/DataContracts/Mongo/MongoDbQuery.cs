using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.DataContracts.Mongo
{
    public static class MongoDbQuery
    {
        public static string LimitQuery => @"{{$limit: {0} }}";

        public static string Pagination => @"{{ '_id': {{$lt: ObjectId('{0}') }} }}";

        public static string SortWithFind => @"{ createdDate : -1}";

        public static string AndQuery => @"{{$and: [{0}] }}";

        //Fields
        public static string PostID => @"{{'postID': '{0}'}}";

        public static string RootId => @"{{'rootID': '{0}'}}";

        public static string ParentID => @"{{'parentID': '{0}'}}";
    }
}
