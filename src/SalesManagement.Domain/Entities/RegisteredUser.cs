using SalesManagement.Domain.Enums;
using System;

namespace SalesManagement.Domain.Entities
{
    public class RegisteredUser
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Zipcode { get; private set; }
        public string Latitude { get; private set; }
        public string Longitude { get; private set; }
        public string Phone { get; private set; }
        public RegisteredUserStatus Status { get; set; } 
        public RegisteredUserRole Role { get; set; }

        private RegisteredUser() { }

        public RegisteredUser(string email, string username, string passwordHash, string firstName, string lastName,
                              string city, string street, int number, string zipcode, string latitude, string longitude,
                              string phone, RegisteredUserStatus status, RegisteredUserRole role)
        {
            Id = Guid.NewGuid();
            Email = email;
            Username = username;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            Number = number;
            Zipcode = zipcode;
            Latitude = latitude;
            Longitude = longitude;
            Phone = phone;
            Status = status;
            Role = role;
        }
        public void Update(string email, string username, string passwordHash, string firstName, string lastName,
                         string city, string street, int number, string zipcode, string latitude, string longitude,
                         string phone, RegisteredUserStatus status, RegisteredUserRole role)
        {
            Email = email;
            Username = username;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            Number = number;
            Zipcode = zipcode;
            Latitude = latitude;
            Longitude = longitude;
            Phone = phone;
            Status = status;
            Role = role;
        }
    }
}
