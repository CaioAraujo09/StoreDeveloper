using SalesManagement.Application.DTOs;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;

namespace SalesManagement.Application.Services
{
    public class RegisteredUserService
    {
        private readonly IRegisteredUserRepository _repository;

        public RegisteredUserService(IRegisteredUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RegisteredUser>> GetAllAsync(int page, int size, string order)
        {
            return await _repository.GetAllAsync(page, size, order);
        }


        public async Task<RegisteredUser?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(RegisteredUserDto dto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new RegisteredUser(dto.Email, dto.Username, passwordHash, dto.FirstName, dto.LastName,
                                          dto.City, dto.Street, dto.Number, dto.Zipcode, dto.Latitude, dto.Longitude,
                                          dto.Phone, dto.Status, dto.Role);

            await _repository.AddAsync(user);
        }

        public async Task UpdateAsync(Guid id, RegisteredUserDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user != null)
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                user = new RegisteredUser(dto.Email, dto.Username, passwordHash, dto.FirstName, dto.LastName,
                                          dto.City, dto.Street, dto.Number, dto.Zipcode, dto.Latitude, dto.Longitude,
                                          dto.Phone, dto.Status, dto.Role);

                await _repository.UpdateAsync(user);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
