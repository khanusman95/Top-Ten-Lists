using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateTest.Models
{
    public class TopTenList
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public DateTime? PostedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool isVotable { get; set; }        

        public virtual ApplicationUser AspNetUser { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual IEnumerable<ListItem> ListItems { get; set; }
    }

    public class ListItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int Rating { get; set; }
        public int TotalRaters { get; set; }

        public virtual TopTenList TTList { get; set; }
    }

    public class Rated
    {
        public int ID { get; set; }       
        public virtual ApplicationUser User { get; set; }
        public virtual ListItem ListItem { get; set; }
    }

    public class ListUserHasRated
    {
        public int ID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual TopTenList List { get; set; }
    }

    public class Comment
    {
        public int ID { get; set; }
        public string CommentContent { get; set; }
        public Nullable<System.DateTime> SharedOn { get; set; }
        public Nullable<System.DateTime> UpdateOn { get; set; }
        public Nullable<bool> isPublished { get; set; }        

        public virtual ApplicationUser AspNetUser { get; set; }
        public virtual TopTenList TTList { get; set; }
       
    }
}