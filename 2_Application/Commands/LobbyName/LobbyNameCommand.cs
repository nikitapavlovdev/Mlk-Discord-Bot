using MediatR;
using MlkAdmin._2_Application.DTOs.Discord.Responses;

namespace MlkAdmin._2_Application.Commands.LobbyName 
{
    public class LobbyNameCommand : IRequest<LobbyNameResponse>
    {
        public ulong UserId { get; set; }
        public string? LobbyName { get; set; } = string.Empty;
    }
}
