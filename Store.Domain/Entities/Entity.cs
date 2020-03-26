using System;
using Flunt.Notifications;

namespace Store.Domain.Entities
{
    public class Entity : Notifiable
    {
        public Entity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}