using Discord.WebSocket;

namespace MlkAdmin.Infrastructure.Cache
{
    public class AutorizationCache
    {
        private readonly Dictionary<SocketGuildUser, string> TemporaryCodes = [];
        public void SetTemporaryCodes(SocketGuildUser user, string code)
        {
            TemporaryCodes.TryAdd(user, code);
        }
        public void RemoveCodeFromDict(SocketGuildUser user)
        {
            TemporaryCodes.Remove(user);
        }
        public string GetCodeForUser(SocketGuildUser user, out string? def)
        {
            TemporaryCodes.TryGetValue(user, out def);
            if (def != null)
            {
                return def;
            }

            return "";
            
        }
    }
}
