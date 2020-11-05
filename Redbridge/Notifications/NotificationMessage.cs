using System;
using Redbridge.Data;

namespace Redbridge.Notifications
{
    [Serializable]
    public abstract class NotificationMessage : IPayloadDescriptor
    {
        protected NotificationMessage()
        {
            Raised = DateTime.UtcNow;
            Metadata = new MetadataCollection();
        }

        protected void AddMetadata(string key, object value)
        {
            Metadata.Add(new MetadataItem() {Id = key, Value = value});
        }

        public Guid? SenderUserId { get; set; }
        public Guid? TargetId { get; set; }
        public NotificationTargetType? TargetType { get; set; }
        public bool IsBroadcastMessage => !TargetId.HasValue;
        public virtual string NotificationId => string.Empty;
        public MetadataCollection Metadata { get; }

        public DateTime Raised { get; private set; }

        public virtual bool SupportsHtmlBody => false;

        public virtual string CreateMessage(BodyType bodyType = BodyType.Html)
        {
            return string.Empty;
        }

        public string Subject { get; set; }

        public virtual Category Category => Category.Notifications;
    }
}
