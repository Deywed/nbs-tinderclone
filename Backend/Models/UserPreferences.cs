using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models
{
    public class UserPreferences
    {
        public int MinAgePref { get; set; }
        public int MaxAgePref { get; set; }
        public string InterestedIn { get; set; } = string.Empty;

    }
}