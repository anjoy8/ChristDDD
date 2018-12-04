using System;
using Christ3D.Domain.Core.Events;

namespace Christ3D.Domain.Events
{
    public class StudentRemovedEvent : Event
    {
        public StudentRemovedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}