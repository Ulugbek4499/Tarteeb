﻿//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using System;
<<<<<<< Updated upstream:Tarteeb.Api/Models/Tickets/Ticket.cs
=======
using System.Collections.Generic;
using Tarteeb.Api.Models.Foundations.Tickets;
using Tarteeb.Api.Models.Foundations.Users;
>>>>>>> Stashed changes:Tarteeb.Api/Models/Foundations/Milestones/Milestone.cs

namespace Tarteeb.Api.Models.Tickets
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public Guid? AssigneeId { get; set; }
        public TicketStatus Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
<<<<<<< Updated upstream:Tarteeb.Api/Models/Tickets/Ticket.cs
        public Guid CreatedUserId { get; set; }
        public Guid UpdatedUserId { get; set; }
=======

        public Guid AssigneeId { get; set; }
        public User Assignee { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
>>>>>>> Stashed changes:Tarteeb.Api/Models/Foundations/Milestones/Milestone.cs
    }
}