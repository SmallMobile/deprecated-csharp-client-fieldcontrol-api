﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldControlApi.Requests
{
    public class CreateActivityRequest : Request
    {
        public override string Resource { get { return "activities"; } }
        public override string Method { get { return "POST"; } }

        public string Identifier { get; set; }
        public string Description { get; set; }

        public int EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int Duration { get; set; }
        public int Status { get; set; }

        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime ScheduledTo { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }

        public string Order { get; set; }
        public bool Archived { get; set; }

        public string ProblemDescription { get; set; }
        public string CanceledDescription { get; set; }
        public bool TimeFixed { get; set; }

        public DateTime FixedStartTime { get; set; }
        public DateTime SharedLocationAt { get; set; }

        public override object GetPayload()
        {
            return this;
        }
    }
}