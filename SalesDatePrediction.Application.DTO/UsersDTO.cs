﻿namespace SalesDatePrediction.Application.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
