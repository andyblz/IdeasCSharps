using System;
namespace BletExamIdeas
{
    public abstract class BaseEntity 
    {
     public DateTime CreatedAt { get; set; }
     public DateTime UpdatedAt { get; set; }
    }
}