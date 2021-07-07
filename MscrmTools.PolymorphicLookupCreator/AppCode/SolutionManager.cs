using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MscrmTools.PolymorphicLookupCreator.AppCode
{
    public class SolutionInfo
    {
        private readonly Entity _solution;

        public SolutionInfo(Entity solution)
        {
            _solution = solution;
        }

        public Entity Solution => _solution;

        public override string ToString()
        {
            return $"{_solution.GetAttributeValue<string>("friendlyname")} ({_solution.GetAttributeValue<string>("version")})";
        }
    }

    internal class SolutionManager
    {
        private readonly IOrganizationService _service;

        public SolutionManager(IOrganizationService service)
        {
            _service = service;
        }

        public List<SolutionInfo> Load()
        {
            return _service.RetrieveMultiple(new QueryExpression("solution")
            {
                NoLock = true,
                ColumnSet = new ColumnSet("uniquename", "friendlyname", "version"),
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = "solution",
                        LinkFromAttributeName = "publisherid",
                        LinkToAttributeName = "publisherid",
                        LinkToEntityName = "publisher",
                        EntityAlias = "pub",
                        Columns = new ColumnSet("customizationprefix")
                    }
                },
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("ismanaged", ConditionOperator.Equal, false)
                    }
                },
                Orders =
                {
                    new OrderExpression("friendlyname", OrderType.Ascending)
                }
            }).Entities.Select(e => new SolutionInfo(e)).ToList();
        }

        internal void AddLookupToSolution(Guid attributeId, string solutionUniqueName)
        {
            var request = new AddSolutionComponentRequest
            {
                AddRequiredComponents = false,
                ComponentId = attributeId,
                ComponentType = 2,
                SolutionUniqueName = solutionUniqueName,
            };

            _service.Execute(request);
        }
    }
}