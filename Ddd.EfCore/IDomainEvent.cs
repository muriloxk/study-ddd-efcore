using System;
using System.Collections.Generic;

namespace Ddd.EfCore
{
    public interface IDomainEvent
    {
    }

    public class StudentEmailChangedEvent : IDomainEvent
    {
        public Guid StudentId { get; }
        public Email NewEmail { get; }

        public StudentEmailChangedEvent(Guid studentId, Email newEmail)
        {
            StudentId = studentId;
            NewEmail = newEmail;
        }
    }

    public interface IBus
    {
        void Send(string message);
    }

    public class Bus : IBus
    {
        public void Send(string message)
        {
            // Put the message on a bus instead
            Console.WriteLine($"Message sent: '{message}'");
        }
    }

    public class MessageBus
    {
        private readonly IBus _bus;

        public MessageBus(IBus bus)
        {
            _bus = bus;
        }

        public void SendEmailChangedMessage(Guid studentId, string newEmail)
        {
            _bus.Send("Type: STUDENT_EMAIL_CHANGED; " + $"Id: {studentId}; " + $"NewEmail: {newEmail} ");
        }
    }

    public class EventDispatcher
    {
        private readonly MessageBus _messageBus;

        public EventDispatcher(MessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Dispatch(IEnumerable<IDomainEvent> events)
        {
            foreach (IDomainEvent ev in events)
            {
                Dispatch(ev);
            }
        }

        private void Dispatch(IDomainEvent ev)
        {
            switch (ev)
            {
                case StudentEmailChangedEvent emailChangedEvent:
                    _messageBus.SendEmailChangedMessage(
                        emailChangedEvent.StudentId,
                        emailChangedEvent.NewEmail);
                    break;

                    //new domains...

                default:
                    throw new Exception($"Unknown event type: '{ev.GetType()}'");
            }
        }
    }
}
