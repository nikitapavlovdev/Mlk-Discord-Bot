using MediatR;
using MlkAdmin._2_Application.DTOs.Responses;

namespace MlkAdmin._2_Application.Commands 
{
    public class LobbyNameCommand : IRequest<LobbyNameResponse>
    {
        public ulong UserId { get; set; }
        public string? LobbyName { get; set; } = string.Empty;
    }
}
