using Discord;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._3_Infrastructure.Discord.Extensions;

namespace MlkAdmin._2_Application.Managers.Discord
{
    public class ComponentsManager(
        ILogger<ComponentsManager> logger,
        SelectionMenuExtension selectionMenuExtension)
    {
        public Task<MessageComponent> CreateMessageComponent(DynamicMessageType type)
        {
            try
            {
                return Task.FromResult(type switch
                {
                    DynamicMessageType.NameColor => selectionMenuExtension.CreateNameColorSelectionMenu(),

                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Неизвестный DynamicMessageType")
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка в классе ComponentsManager");
                return Task.FromResult<MessageComponent>(new ComponentBuilder().Build());
            }
        }
    }
}
