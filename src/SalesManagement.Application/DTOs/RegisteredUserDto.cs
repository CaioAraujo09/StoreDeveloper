using SalesManagement.Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace SalesManagement.Application.DTOs
{
    public class RegisteredUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Zipcode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Phone { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public RegisteredUserStatus Status { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RegisteredUserRole Role { get; set; } 
    }
}
