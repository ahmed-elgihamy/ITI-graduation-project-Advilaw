﻿using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Entites.SessionUtilities.MessageSection;
using AdviLaw.Domain.Entites.SessionUtilities.ReportSection;
using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using AdviLaw.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Entities.UserSection
{
    public class User : IdentityUser
    {

        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public long NationalityId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }


        public decimal? Balance { get; set; } = 0; // بدل الـ Account
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

        //Navigation Properties

        // Payment Section
        public List<Payment> SentPayments { get; set; } = new();
        public List<Payment> ReceivedPayments { get; set; } = new();
        public List<EscrowTransaction> EscrowTransactions { get; set; } = new();

        //Session => Reviews Section
        public List<Review> SentReviews { get; set; } = new();
        public List<Review> ReceivedReviews { get; set; } = new();

        //Session => Messages Section
        public List<Message> SentMessages { get; set; } = new();
        public List<Message> ReceivedMessages { get; set; } = new();

        //Session => Reports Section
        public List<Report> SentReports { get; set; } = new();
        public List<Report> ReceivedReports { get; set; } = new();

        // Navigation Properties
        public Lawyer? Lawyer { get; set; }
        public Client? Client { get; set; }
        public Admin? Admin { get; set; }

        public Roles Role { get; set; }  // This can be "Lawyer", "Client", or "Admin"
    }
}
