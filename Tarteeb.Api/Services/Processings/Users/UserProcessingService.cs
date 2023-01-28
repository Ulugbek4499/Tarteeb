﻿using static Tarteeb.Api.Services.Processings.Users.UserProcessingService;
using System.Linq;
using Tarteeb.Api.Models;
using Tarteeb.Api.Services.Foundations.Users;

namespace Tarteeb.Api.Services.Processings.Users
{
    public class UserProcessingService:IUserProcessingsService
    {
        private readonly IUserService userService;

        public UserProcessingService(IUserService userService) =>
            this.userService = userService;

        public User RetrieveUserByCredentails(string email, string password)
        {
            IQueryable<User> allUser = this.userService.RetrieveAllUsers();

            return allUser
                .FirstOrDefault(retrievedUser => retrievedUser.Email.Equals(email)
                    && retrievedUser.Password.Equals(password));
        }
    }
}
