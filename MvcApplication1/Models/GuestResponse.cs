using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Mail;

namespace MvcApplication1.Models
{
    public class GuestResponse : IDataErrorInfo
    {
        public string Name {get;set;}
        public string Email {get; set;}
        public string Phone { get; set; }
        public bool? WillAttend { get; set; }
        public string Error { get { return null; } }
        public string this[string propName]
        {
            get 
            {
                if ((propName == "Name") && string.IsNullOrEmpty(Name)) return "Please enter you'r Name";
                if ((propName == "Email") && (string.IsNullOrEmpty(Name) || !Regex.IsMatch(Email, ".+\\@.+\\..+"))) return "Plese enter a valid email";
                if ((propName == "Phone") && string.IsNullOrEmpty(Phone)) return "Please enter you't phone number";
                if ((propName == "WillAttend") && !WillAttend.HasValue) return "Please specify whether you'll attend";
                return null;
            }
        }

        public void Submit()
        {
            var message = new StringBuilder();
            message.AppendFormat("Date: {0:yyyy-MM-dd hh:mm}\n", DateTime.Now);
            message.AppendFormat("RSVP From: {0}\n", Name);
            message.AppendFormat("Email: {0}\n", Email);
            message.AppendFormat("Phone: {0}\n", Phone);
            message.AppendFormat("Can come: {0}\n", WillAttend.Value ? "Yes" : "No");

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(new MailMessage("rsvp@example.com", "organizator@example.com", Name + (WillAttend.Value ? "will attend" : "won't attend"), message.ToString()));
        }

        private void EnsureCurrentlyValid()
        { 
            var propsToValid = new[] {"Name", "Email", "Phone", "WillAttend"};
            bool IsValid = propsToValid.All(x=> this[x] == null);
            if (!IsValid)
            {
                throw new InvalidOperationException("Can't submit Invalid GuestResponse");
            }
        }
    }

    
}