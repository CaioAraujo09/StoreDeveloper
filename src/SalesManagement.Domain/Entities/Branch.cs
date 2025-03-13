using System.Text.Json.Serialization;

namespace SalesManagement.Domain.Entities;

    [JsonSerializable(typeof(Branch))]
    public class Branch
    {
        public Guid Id { get; set; }
        public string BranchName { get; set; }
    }
