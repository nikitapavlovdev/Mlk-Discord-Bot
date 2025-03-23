using Discord.WebSocket;

namespace Discord_Bot.Infrastructure.Cash
{
    public class AuCash
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
        public string GetCodeForUser(SocketGuildUser user, out string def)
        {
            TemporaryCodes.TryGetValue(user, out def);
            return def;
        }
    }
}
