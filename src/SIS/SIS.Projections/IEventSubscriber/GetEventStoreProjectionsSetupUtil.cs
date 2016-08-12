using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.Projections;
using EventStore.ClientAPI.SystemData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;


namespace SIS.Projections
{
    public class GetEventStoreProjectionsSetupUtil
    {
        IPEndPoint ServerHttpEndPoint;
        UserCredentials UserCredentials;
        Assembly AssemblyThatContainsAggregates;

        public void CreateAggregateProjectionStreamsFor(Assembly assemblyThatContainsAggregates)
        {
            AssemblyThatContainsAggregates = assemblyThatContainsAggregates;
            InitializeConenctionSettings();
            Dictionary<string, Projection> domainProjections = DistillDomainProjections();
            HashSet<string> databaseProjections = ReadAggregateEventProjections();
            var projectionsToCreate = GenListOfProjectionsToCreate(domainProjections, databaseProjections);
            CreateProjections(projectionsToCreate);
            var proj = GetAllProjections();
            EnableAllProjections(proj);
        }

        private void InitializeConenctionSettings()
        {
            var dict = ParseStringIntoKeyValues(ConfigurationManager.ConnectionStrings["GetEventStoreProjectionSetupUtilConnection"].ToString());
            var credentials = dict["UserCredentials"].Split(':');
            UserCredentials = new UserCredentials(credentials[0], credentials[1]);
            var endpoint = dict["IPEndpoint"].Split(':');
            var ipAddress = endpoint[0].ToLower().Replace("localhost", "127.0.0.1");
            ServerHttpEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), Convert.ToInt32(endpoint[1]));
        }

        private static Dictionary<string, string> ParseStringIntoKeyValues(string str)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            var fields = str.Split(';');
            foreach (var field in fields)
            {
                var data = field.Split('=');
                retVal.Add(data[0], data[1]);
            }
            return retVal;
        }

        private Dictionary<string, Projection> DistillDomainProjections()
        {
            Dictionary<string, Projection> domainProjections = new Dictionary<string, Projection>();
            foreach (var type in GetAllAggregateTypes())
            {
                string name = type.Name.Replace("Aggregate", "");
                domainProjections.Add(name, Projection.FromAggregateTypeName(name));
            }
            return domainProjections;
        }

        private HashSet<string> ReadAggregateEventProjections()
        {
            dynamic projections = GetAllProjections();
            HashSet<string> databaseProjections = new HashSet<string>();
            foreach (var projection in projections)
            {
                databaseProjections.Add(projection.Name.Replace("Projection", ""));
            }
            return databaseProjections;
        }

        private static List<Projection> GenListOfProjectionsToCreate(Dictionary<string, Projection> domainProjections, HashSet<string> databaseProjections)
        {
            List<Projection> projectionsThatShoudBeCreated = new List<Projection>();
            foreach (var p in domainProjections)
            {
                if (!databaseProjections.Contains(p.Key))
                {
                    projectionsThatShoudBeCreated.Add(p.Value);
                }
            }
            return projectionsThatShoudBeCreated;
        }

        void CreateProjections(List<Projection> projections)
        {
            if (projections.Count == 0)
                return;
            ProjectionsManager projectionsManager = new ProjectionsManager(new ConsoleLogger(), ServerHttpEndPoint, TimeSpan.FromSeconds(5));
            foreach (var p in projections)
                projectionsManager.CreateContinuousAsync(p.Name, p.JSStatement, UserCredentials).Wait();
        }

        private void EnableAllProjections(dynamic projections)
        {
            var logger = new ConsoleLogger();
            ProjectionsManager projectionsManager = new ProjectionsManager(logger, ServerHttpEndPoint, TimeSpan.FromSeconds(5));
            foreach (var projection in projections)
            {
                if (projection.Status.ToLower() == "stopped")
                {
                    projectionsManager.EnableAsync(projection.Name, UserCredentials).Wait();
                }
            }
        }

        IEnumerable<Type> GetAllAggregateTypes()
        {

            var types = AssemblyThatContainsAggregates
                .GetTypes()
                .Where(m => m.IsClass && (!m.IsAbstract) && m.GetInterface("IAggregate") != null);
            return types;
        }

        private dynamic GetAllProjections()
        {
            ProjectionsManager projectionsManager = new ProjectionsManager(new ConsoleLogger(), ServerHttpEndPoint, TimeSpan.FromSeconds(5));
            return projectionsManager.ListAllAsync(UserCredentials).Result;
        }

        internal class Projection
        {
            const string ProjectionFormat = "fromCategory('{0}').whenAny(function(state, ev) {{linkTo('{1}', ev);}});   ";
            public string Name { get; set; }
            public string JSStatement { get; set; }

            public static Projection FromAggregateTypeName(string aggTypeName)
            {
                aggTypeName = aggTypeName.Replace("Aggregate", "");
                var projectionName = string.Format("{0}Projection", aggTypeName);
                var streamName = string.Format("{0}Events", aggTypeName);
                var js = string.Format(ProjectionFormat, aggTypeName, streamName);
                return new Projection() { Name = projectionName, JSStatement = js };
            }
        }
    }
}
