using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Domain
{
    public class UserComment
    {
        [Key]
        public Guid CommentID { get; set; }
        public string? CommenteryUserID { get; set; }
         public string CommentBody { get; set; }
        public int CommentedScore { get; set; }
        public int? IsHelpful { get; set; }
        public int? IsHarmful {  get; set; }

        public DateTime CommentCreatedAt { get; set; }
        public DateTime CommentModifiedAt { get; set; }
        public DateTime CommentDeletedAt { get; set; }

    }
}
