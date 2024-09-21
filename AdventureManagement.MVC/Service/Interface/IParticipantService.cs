using AdventureManagement.MVC.Models;

namespace AdventureManagement.MVC.Service.Interface
{
    public interface IParticipantService
    {
        Task<List<ParticipantVM>> GetAllParticipantsAsync();
        Task<ParticipantDetailVM> GetParticipantByIdAsync(int id);
        Task<bool> CreateParticipantAsync(ParticipantCreateVM participant);
        Task<bool> UpdateParticipantAsync(int id, ParticipantUpdateVM participant);
        Task<bool> DeleteParticipantAsync(int id);
    }
}
