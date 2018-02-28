using System;
using System.Collections.Generic;
using BletExamIdeas;

namespace BletExamIdeas.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        

        public List<Post> MyPost { get; set; }
        public List<Like> UserLikedPost{ get; set;}
        public User()
        {
            MyPost = new List<Post>();
            UserLikedPost =new List<Like>();
        }

       
    

    }
}