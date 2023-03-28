﻿//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using System.Threading.Tasks;
using Tarteeb.Api.Models.Foundations.Scores.Exceptions;
using Tarteeb.Api.Models.Foundations.Scores;
using Xeptions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tarteeb.Api.Services.Foundations.Scores
{
    public partial class ScoreService
    {
        private delegate ValueTask<Score> ReturningScoresFunction();
        private async ValueTask<Score> TryCatch(ReturningScoresFunction returningTeamFunction)
        {
            try
            {
                return await returningTeamFunction();
            }
            catch (InvalidScoreException invalidScoreException)
            {
                throw CreateAndLogValidationException(invalidScoreException);
            }
            catch(NotFoundScoreException notFoundScoreException)
            {
                throw CreateAndLogValidationException(notFoundScoreException);
            }
            catch(DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedScoreException = new LockedScoreException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedScoreException);
            }
        }

        private Exception CreateAndLogDependencyValidationException(LockedScoreException lockedScoreException)
        {
            var scoreDependencyValidationException = new ScoreDependencyValidationException(lockedScoreException);
            this.loggingBroker.LogError(scoreDependencyValidationException);

            return scoreDependencyValidationException;
        }

        private ScoreValidationException CreateAndLogValidationException(Xeption exception)
        {
            var scoreValidationExpcetion = new ScoreValidationException(exception);
            this.loggingBroker.LogError(scoreValidationExpcetion);

            return scoreValidationExpcetion;
        }
    }
}
