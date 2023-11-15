using Newtonsoft.Json;

namespace fluXis.Game.Online.Fluxel.Packets.Chat;

public class ChatMessagePacket : Packet
{
    public override int ID => 10;

    [JsonProperty("content")]
    public string Content { get; init; }

    [JsonProperty("channel")]
    public string Channel { get; init; }

    public ChatMessagePacket(string content, string channel)
    {
        Content = content;
        Channel = channel;
    }
}
