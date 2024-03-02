using NuGet.Protocol.Plugins;

namespace Sclms.DTOS.Events
{
    public class EventRequestDTO
    {
        public long ID { get; set; }
        public string? EventTitle { get; set; }
        public long Start { get; set; }
        public long End { get; set; }
    }

    public class ConversationMessageDTO
    {
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsReceived { get; set; }

    }
    public class ConversationDTO
    {
        public List<ConversationMessageDTO> Messages { get; set; }
        public string ReceiverName   { get; set; }
        public string ReceiverID { get; set; }
        public string ReceiverAvatar { get; set; }

    }
    public class SendMessageRequestDTO
    {
        public string ReceiverID { get; set; }
        public string SenderID { get; set; }
        public string Text { get; set; }

    }
    public class GetConversationRequestDTO
    {
         
        public string ?ReceiverID { get; set; }
        public string? SenderID { get; set; } 

    }

    public class ConversationFilters
    {
        ////////// Add Filters in future if required 
    }

    public class ConversationList
    {
        public string UserName { get; set; }
        public string Email  { get; set; }
        public string UserId { get; set; }
        public DateTime LastCommunicatedAt { get; set; }
    }


    public class ConversationListAdmin
    {
        public ConversationList Sender { get; set; }
        public ConversationList Receiver { get; set; }
    }

}
