﻿//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tarteeb.Api.Models.Foundations.Milestones;

namespace Tarteeb.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Milestone> Milestones { get; set; }

        public IQueryable<Milestone> SelectAllMilestones() =>
            SelectAll<Milestone>();
    }
}
