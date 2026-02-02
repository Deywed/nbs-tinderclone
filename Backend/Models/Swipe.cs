using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public enum SwipeType
    {
        Like,
        Dislike,
        SuperLike
    }

    public class Swipe
    {
        public Guid SwiperId { get; set; }
        public Guid SwipeeId { get; set; }
        public SwipeType Type { get; set; }
    }   
}