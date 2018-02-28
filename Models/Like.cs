using System;
using System.ComponentModel.DataAnnotations;
using BletExamIdeas;

namespace BletExamIdeas.Models
{
    public class Like : BaseEntity
    {   
         [Key]
        public int LikesId { get; set; }
      
        public int UserId { get; set; }
        public User User { get; set; }
 
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}