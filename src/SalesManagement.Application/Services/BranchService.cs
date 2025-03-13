using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Interfaces;
using SalesManagement.Application.DTOs;

namespace SalesManagement.Application.Services
{
    public class BranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<IEnumerable<BranchDto>> GetAllAsync()
        {
            var branches = await _branchRepository.GetAllAsync();
            return branches.Select(b => new BranchDto { Id = b.Id, BranchName = b.BranchName });
        }

        public async Task<BranchDto?> GetByIdAsync(Guid id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            return branch == null ? null : new BranchDto { Id = branch.Id, BranchName = branch.BranchName };
        }

        public async Task AddAsync(BranchDto branchDto)
        {
            if (string.IsNullOrWhiteSpace(branchDto.BranchName))
                throw new ArgumentException("O nome da filial não pode ser vazio.");

            var branch = new Branch { Id = Guid.NewGuid(), BranchName = branchDto.BranchName };
            await _branchRepository.AddAsync(branch);
        }

        public async Task UpdateAsync(Guid id, BranchDto branchDto)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
                throw new Exception("Filial não encontrada.");

            branch.BranchName = branchDto.BranchName;
            await _branchRepository.UpdateAsync(branch);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _branchRepository.DeleteAsync(id);
        }
    }
}
