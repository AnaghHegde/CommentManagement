using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Data.Models
{
    public class CommentsDataBaseSettings : ICommentsDataBaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }

    public interface ICommentsDataBaseSettings
    {
        string DatabaseName { get; set; }

        string ConnectionString { get; set; }
    }
}
