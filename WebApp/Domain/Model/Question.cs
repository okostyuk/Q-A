using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApp.Domain
{
    public class Question
    {
        //[JsonIgnore]
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string ClientTitle { get; set; }

        public int MaxCustomAnswers { get; set; }
        public int ClientMaxCustomAnswers { get; set; }

        //[JsonIgnore]
        public DateTime? ExpiresDate { get; set; }
        public string ClientExpiresDate { get; set; }

        //[JsonIgnore]
        public DateTime? PublishDate { get; set; }
        
        public List<Answer> Answers { get; set; }
        public List<Answer> ClientAnswers { get; set; }

        public List<Vote> Votes { get; set; }
        public bool Editable { get; set; }

        public bool ClientPublish { get; set; }

        public bool IsPublished()
        {
            return PublishDate != null;
        }
    }
}