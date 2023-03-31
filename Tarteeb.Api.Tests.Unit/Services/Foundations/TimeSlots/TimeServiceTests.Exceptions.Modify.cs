﻿//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Tarteeb.Api.Models.Foundations.Times;
using Tarteeb.Api.Models.Foundations.TimeSlots.Exceptions;
using Xunit;

namespace Tarteeb.Api.Tests.Unit.Services.Foundations.TimeSlots
{
    public partial class TimeServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            // given 
            DateTimeOffset someDateTime = GetRandomDateTime();
            Time randomTime = CreateRandomTime(someDateTime);
            Time someTime = randomTime;
            Guid TimeId = someTime.Id;
            SqlException sqlException = CreateSqlException();

            var failedTimeStorageException =
                new FailedTimeStorageException(sqlException);

            var expectedTimeDependencyException =
                new TimeDependencyException(failedTimeStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
               broker.GetCurrentDateTime()).Throws(sqlException);

            // when 
            ValueTask<Time> modifyTimeTask =
                this.timeService.ModifyTimeAsync(someTime);

            TimeDependencyException actualTimeDependencyException =
                await Assert.ThrowsAsync<TimeDependencyException>(modifyTimeTask.AsTask);

            // then
            actualTimeDependencyException.Should().
                BeEquivalentTo(expectedTimeDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedTimeDependencyException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectTimeByIdAsync(TimeId), Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateTimeAsync(someTime), Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
