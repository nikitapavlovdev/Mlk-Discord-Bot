using MediatR;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin._2_Application.DTOs;

namespace MlkAdmin._2_Application.Commands
{
    public class LobbyNameHandler(IUserRepository userRepository) : IRequestHandler<LobbyNameCommand, LobbyNameResponse>
    {
        public async Task<LobbyNameResponse> Handle(LobbyNameCommand request, CancellationToken cancellationToken)
        {
			try
			{
                User? user = await userRepository.GetDbUserAsync(request.UserId);

                await userRepository.UpsertUserAsync(user);

                return new()
                {
                    IsSuccess = true,
                    Error = string.Empty,
                    LobbyName = request.LobbyName,
                    Message = "Лобби успешно сохранено",
                    Status = "Успех",
                };
            }
			catch (Exception ex)
			{
                return new()
                {
                    IsSuccess = false,
                    Error = ex.Message,
                    LobbyName = string.Empty,
                    Message = string.Empty,
                    Status = "Ошибка"
                };
			}
        }
    }
}
