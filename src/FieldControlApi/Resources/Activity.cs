﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace FieldControlApi.Resources
{
    public enum ActivityStatus
    {
        Scheduled = 0,
        InProgress = 1,
        Done = 2,
        Canceled = 3,
        Reported = 4
    }

    public class Activity : Resource
    {
        public Activity()
        {
            Status = ActivityStatus.Scheduled;
        }

        public Activity(Customer customer)
            : this()
        {
            CustomerId = customer.Id;
            ZipCode = customer.ZipCode;
            Street = customer.Street;
            Number = customer.Number;
            City = customer.City;
            State = customer.State;
            Latitude = customer.Latitude;
            Longitude = customer.Longitude;
        }

        public bool Archived { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string StatusString { get; private set; }

        [JsonIgnore]
        public ActivityStatus Status
        {
            get { return (ActivityStatus)(Convert.ToInt32(StatusString)); }
            set { StatusString = ((int)value).ToString(); }
        }

        public int? EmployeeId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int Duration { get; set; }

        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public DateTime ScheduledTo { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public string ProblemDescription { get; set; }
        public string CanceledDescription { get; set; }
        public string DoneDescription { get; set; }

        public DateTime? SharedLocationAt { get; set; }

        public string FixedStartTime { get; set; }
        public bool TimeFixed { get { return !string.IsNullOrEmpty(FixedStartTime); } }
    }
}
