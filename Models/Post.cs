using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BletExamIdeas;

namespace BletExamIdeas.Models
{
    public class Post : BaseEntity
    {
        [Key]
        public int PostId { get; set; }
        public string Massage { get; set; }
    


        [ForeignKey("PostCreater")]
        public int PostCreaterId {get; set;}
    
        public User PostCreater {get;set;}

        public List<Like> PostLikedByUser { get; set; }
 
        public Post()
        {
            PostLikedByUser  = new List<Like>();
        }
    }
}