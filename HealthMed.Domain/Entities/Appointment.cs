﻿using HealthMed.Domain.Entities.Enums;

namespace HealthMed.Domain.Entities
{
    public class Appointment : EntityBase
    {
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public DateTime ScheduledTime { get; private set; }
        public AppointmentStatus Status { get; private set; }
        public string? CancellationReason { get; private set; }
        public decimal Price { get; private set; }

        public Appointment(Guid doctorId, Guid patientId, DateTime scheduledTime, decimal price)
        {
            Id = Guid.NewGuid();
            DoctorId = doctorId;
            PatientId = patientId;
            ScheduledTime = scheduledTime;
            Status = AppointmentStatus.Pending;
            Price = price;
        }

        public void Accept()
        {
            if (Status != AppointmentStatus.Pending)
                throw new InvalidOperationException("Only pending appointments can be accepted.");

            Status = AppointmentStatus.Accepted;
        }

        public void Reject()
        {
            if (Status != AppointmentStatus.Pending)
                throw new InvalidOperationException("Only pending appointments can be rejected.");

            Status = AppointmentStatus.Rejected;
        }

        public void Cancel(string reason)
        {
            if (Status != AppointmentStatus.Accepted)
                throw new InvalidOperationException("Only accepted appointments can be canceled.");

            Status = AppointmentStatus.Canceled;
            CancellationReason = reason;
        }
    }
}