﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldControlApi;
using FieldControlApi.Configuration;
using FieldControlApi.Resources;
using FieldControlApi.Requests;
using FieldControlApi.Requests.Activities;
using FieldControlApi.Requests.Customers;
using Newtonsoft.Json;

namespace FieldControlApi.Examples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = CreateAuthenticatedClient();

            CreateNewCustomer(client);
            GetCustomerById(client);
            SearchCustomersByName(client);

            CreateNewActivity(client);
            CreateNewActivityWithTimeFixed(client);
            GetActivityById(client);
            GetActivities(client);
            GetActivitiesByRangeOfDates(client);

            GetServices(client);
            CreateNewService(client);

            GetActiveEmployees(client);

            OptimizeRoutes(client);

            Console.Read();
        }

        private static void GetActivitiesByRangeOfDates(Client client)
        {
            PrintSeparator();

            var from = new DateTime(2016, 1, 1);
            var to = new DateTime(2016, 5, 15);
            PrintHeader("Getting activities from " + from.ToShortDateString() + " to " + to.ToShortDateString());

            var activities = client.Execute(new GetActivitiesRequest(from));
            PrintObject(activities);
        }

        private static void GetActivities(Client client)
        {
            PrintSeparator();
            PrintHeader("Getting activities from "+ new DateTime(2016,5,5).ToShortDateString() + ": ");

            var activities = client.Execute(new GetActivitiesRequest(new DateTime(2016, 5, 5)));
            PrintObject(activities);
        }

        private static void CreateNewService(Client client)
        {
            PrintSeparator();
            PrintHeader("Creating a new service: ");

            var service = new Service()
            {
                Description = "Cleaning",
                Duration = 60,
            };

            var savedService = client.Execute(new CreateServiceRequest(service));
            PrintObject(savedService);
        }

        private static void GetServices(Client client)
        {
            PrintSeparator();
            PrintHeader("Getting all services: ");

            var services = client.Execute(new GetServicesRequest());
            PrintObject(services);
        }

        private static void GetActiveEmployees(Client client)
        {
            PrintSeparator();
            PrintHeader("Getting active employees: ");

            var employees = client.Execute(new GetActiveEmployeesRequest(DateTime.Today));
            PrintObject(employees);
        }

        private static void CreateNewActivityWithTimeFixed(Client client)
        {
            PrintSeparator();
            PrintHeader("Creating a new activity with time fixed: ");

            var activity = new Activity()
            {
                Identifier = Guid.NewGuid().ToString(),
                ScheduledTo = DateTime.Today,
                CustomerId = 1,
                ServiceId = 1,
                EmployeeId = null,
                Duration = 60,
                ZipCode = "15015000",
                Street = "Avenida Doutor Alberto Andaló",
                Number = "4075",
                City = "São José do Rio Preto",
                State = "São Paulo",
                Description = "Activity from csharp client",
                Latitude = -20.798035m,
                Longitude = -49.359166m,
                FixedStartTime = "15:00"
            };

            var savedActivity = client.Execute(new CreateActivityRequest(activity));
            PrintObject(savedActivity);
        }

        private static void CreateNewActivity(Client client)
        {
            PrintSeparator();
            PrintHeader("Creating a new activity: ");

            var activity = new Activity()
            {
                Identifier = Guid.NewGuid().ToString(),
                ScheduledTo = new DateTime(2016, 3, 22),
                CustomerId = 1,
                ServiceId = 1,
                EmployeeId = null,
                Duration = 60,
                ZipCode = "15015000",
                Street = "Avenida Doutor Alberto Andaló",
                Number = "4075",
                City = "São José do Rio Preto",
                State = "São Paulo",
                Description = "Activity from csharp client",
                Latitude = -20.798035m,
                Longitude = -49.359166m
            };

            var savedActivity = client.Execute(new CreateActivityRequest(activity));
            PrintObject(savedActivity);
        }

        private static void GetActivityById(Client client)
        {
            PrintSeparator();
            PrintHeader("Getting a activity by id: ");

            var activity = client.Execute(new GetActivityRequest("1"));
            PrintObject(activity);
        }

        private static void CreateNewCustomer(Client client)
        {
            PrintSeparator();
            PrintHeader("Creating a new customer: ");

            var customer = new Customer()
            {
                Name = "Luiz Freneda",
                Email = "lfreneda@gmail.com",
                Phone = "11963427199",
                ZipCode = "05422010",
                Street = "Rua dos Pinheiros",
                Number = "383",
                City = "São Paulo",
                State = "São Paulo",
                Latitude = -23.566413m,
                Longitude = -46.679770m
            };

            var savedCustomer = client.Execute(new CreateCustomerRequest(customer));
            PrintObject(savedCustomer);
        }

        private static void GetCustomerById(Client client)
        {
            PrintSeparator();
            PrintHeader("Getting a customer by id: ");

            var customer = client.Execute(new GetCustomerRequest("1"));
            PrintObject(customer);
        }

        private static void SearchCustomersByName(Client client)
        {
            PrintSeparator();
            PrintHeader("Searching customers by name: ");

            var customers = client.Execute(new GetCustomersRequest("Shopping"));
            PrintObject(customers);
        }

        public static Client CreateAuthenticatedClient()
        {
            PrintSeparator();
            PrintHeader("Creating authenticated client: ");

            var client = new Client(new Configuration.Configuration
            {
                BaseUrl = "http://api.fieldcontrol.com.br/"
            });

            client.Authenticate("lfreneda@gmail.com", "password");

            PrintObject(new { Token = client.AuthenticationToken });
            return client;
        }

        private static void OptimizeRoutes(Client client)
        {
            PrintSeparator();
            PrintHeader("Optimizing activities for given date: ");

            var routeOptimization = new RouteOptimization() {
                Date = new DateTime(2016, 3, 23)
            };
            var optimizationResult = client.Execute(new RouteOptimizationRequest(routeOptimization));

            PrintObject(optimizationResult);
        }

        private static void PrintObject(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void PrintHeader(string sectionName)
        {
            Console.WriteLine(sectionName);
        }

        private static void PrintSeparator()
        {
            Console.WriteLine("################################################");
        }
    }
}
