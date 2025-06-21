using Discord;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._3_Infrastructure.Discord.Extensions;

namespace MlkAdmin._2_Application.Managers.Components
{
    public class ComponentsManager(SelectionMenuExtension selectionMenuExtension)
    {
        public Task<MessageComponent> GetMessageComponent(DynamicMessageType type)
        {
            return Task.FromResult(type switch
            {
                DynamicMessageType.NameColor => selectionMenuExtension.GetColorSwitchSelectionMenu(),
                DynamicMessageType.Features => MessageComponentsExtension.GetServerHubFeatuesButtons(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown type: {type}")
            });
        }
    }
}
