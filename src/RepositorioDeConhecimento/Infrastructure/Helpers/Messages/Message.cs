using System.Text.Json;

namespace RepositorioDeConhecimento.Infrastructure.Helpers.Messages
{
    /// <summary>
    /// Enum that represents the type of message.
    /// </summary>
    public enum MessageType
    {
        Success,
        Error,
        Warning,
        Info
    }

    /// <summary>
    /// Class that represents a message to be sent to the client.
    /// </summary>
    public class Message
    {
        public string Text { get; set; }

        public MessageType Type { get; set; }

        public Message(string text, MessageType type = MessageType.Success)
        {
            Text = text;
            Type = type;
        }

        /// <summary>
        /// Serialize a message to JSON.
        /// </summary>
        /// <param name="text">Message content.</param>
        /// <param name="type">Type of message.</param>
        /// <returns>A serialized message.</returns>
        public static string Serialize(string text, MessageType type = MessageType.Success)
        {
            Message message = new Message(text, type);
            return JsonSerializer.Serialize(message);
        }

        /// <summary>
        /// Deserialize a JSON message.
        /// </summary>
        /// <param name="jsonMessage">Message in JSON.</param>
        /// <returns>A Message object.</returns>
        public static Message Deserialize(string jsonMessage)
        {
            return JsonSerializer.Deserialize<Message>(jsonMessage);
        }

        /// <summary>
        /// Create a message.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="type">Message type.</param>
        /// <returns>An new Message object.</returns>
        public static string CreateMessage(string text, MessageType type = MessageType.Success)
        {
            return Serialize(text, type);
        }
    }
}
