using CommentManagement.Data.Models.DataContracts;
using CommentManagement.Data.Models.DataContracts.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models.Query
{
    public class QueryStringHelper
    {
        
        public string GetCommentsQueryString(GetCommentsRequest commentsRequest)
        {
            
            String queryString = String.Format(MongoDbQuery.RootId, commentsRequest.RootID) + String.Format(MongoDbQuery.PostID, commentsRequest.PostID);

            if (commentsRequest.IsDateRange && commentsRequest.StartDate != null && commentsRequest.EndDate!= null 
                && commentsRequest.StartDate.Length > 0 && commentsRequest.EndDate.Length > 0)
            {
                queryString += GetDateRangeQueryString(commentsRequest.StartDate, commentsRequest.EndDate);
            }

            if (commentsRequest.IsPagination && commentsRequest.LastCommentID != null)
            {
                queryString += String.Format(MongoDbQuery.Pagination, commentsRequest.LastCommentID);
            }

            if (commentsRequest.IsKeywords && commentsRequest.Keywords != null && commentsRequest.Keywords.Count > 0)
            {
                queryString += GetKeywordQueryString(commentsRequest.Keywords);
            }

            return String.Format(MongoDbQuery.AndQuery, queryString);
        }

        public String GetRepliesQueryString(GetSubCommentsRequest commentsRequest)
        {
            return GetCommentsQueryString(commentsRequest);
        }

        private String GetDateRangeQueryString(String startDate, String endDate)
        {
            return string.Format(MongoDbQuery.DateFilter, DateTime.Parse(startDate),
                                                DateTime.Parse(endDate));
        }

        private String GetKeywordQueryString(List<String> Keywords)
        {
            var keyword = string.Join(" ", FormatKeyWords(Keywords));
            return string.Format(MongoDbQuery.KeyWord, string.Format("'{0}'", keyword));
        }

        private List<string> FormatKeyWords(List<string> keywords)
        {
            List<string> phrases = new List<string>();
            foreach (var word in keywords)
            {
                var phrase = string.Empty;
                phrase = word.Replace("'", "");
                phrases.Add(phrase);
            }
            return phrases;
        }

        
    }
}
