using System;
using System.Collections.Generic;

namespace WebApp.Domain
{
    public class Question
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string Title { get; set; }
        public string ClientTitle { get; set; }

        public int MaxCustomAnswers { get; set; }
        public string ClientMaxCustomAnswers { get; set; }

        public DateTime ExpiresDate { get; set; }
        public string ClientExpiresDate { get; set; }

        public DateTime PublishDate { get; set; }
        
        public List<Answer> Answers { get; set; }
        public List<Answer> ClientAnswers { get; set; }

        public List<Vote> Votes { get; set; }
        public bool Editable { get; set; }

        public bool ClientPublish { get; set; }

        public bool IsPublished()
        {
            return PublishDate != null; //why always not null????
        }
    }
}