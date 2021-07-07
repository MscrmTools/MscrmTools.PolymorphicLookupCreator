using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolymorphicLookupCreator.AppCode
{
    public class EntityInfo
    {
        private readonly EntityMetadata _emd;

        public EntityInfo(EntityMetadata emd)
        {
            _emd = emd;
        }

        public EntityMetadata Metadata => _emd;

        public override string ToString()
        {
            return _emd.SchemaName;
        }
    }

    internal class MetadataManager
    {
        private readonly IOrganizationService _service;
        private int languageCode;

        public MetadataManager(IOrganizationService service)
        {
            _service = service;

            LoadBaseLanguage();
        }

        public Guid CreatePolymorphicLookup(string prefix, string displayName, string schemaName, string referencingEntity, string[] referencedEntities)
        {
            var label = new LocalizedLabel()
            {
                Label = displayName,
                LanguageCode = languageCode
            };

            var relations = new OneToManyRelationshipMetadata[referencedEntities.Length];
            for (var i = 0; i < referencedEntities.Length; i++)
            {
                var referencedEntity = referencedEntities[i];

                relations[i] = new OneToManyRelationshipMetadata
                {
                    SchemaName = $"{prefix}{referencingEntity}_{referencedEntity}_{schemaName}",
                    ReferencedEntity = referencedEntity,
                    ReferencingEntity = referencingEntity
                };

                if (relations[i].SchemaName.Length > 100)
                {
                    relations[i].SchemaName = relations[i].SchemaName.Substring(0, 100);
                }
            }

            var request = new OrganizationRequest
            {
                RequestName = "CreatePolymorphicLookupAttribute",
                Parameters =
                {
                    {"OneToManyRelationships", relations },
                    {
                        "Lookup", new LookupAttributeMetadata()
                        {
                            DisplayName = new Label(label, new LocalizedLabel[] {label}) ,
                            SchemaName = schemaName
                        }
                    }
                }
            };

            var response = _service.Execute(request);

            return new Guid(response.Results["AttributeId"].ToString());
        }

        public List<EntityInfo> GetAvailableEntitiesForRelationship(ConnectionDetail detail = null)
        {
            //if (detail?.MetadataCache != null)
            //{
            //    return detail.MetadataCache
            //        .Where(e => e.CanBePrimaryEntityInRelationship.Value || e.CanBeRelatedEntityInRelationship.Value)
            //        .Select(emd => new EntityInfo(emd))
            //        .ToList();
            //}

            EntityQueryExpression entityQueryExpression = new EntityQueryExpression
            {
                Criteria = new MetadataFilterExpression(LogicalOperator.And)
                {
                    Filters =
                    {
                        new MetadataFilterExpression(LogicalOperator.Or){
                         Conditions =
                        {
                            new MetadataConditionExpression("CanBePrimaryEntityInRelationship", MetadataConditionOperator.Equals, true),
                            new MetadataConditionExpression("CanBeRelatedEntityInRelationship", MetadataConditionOperator.Equals, true)
                        }
                        },
                        new MetadataFilterExpression(LogicalOperator.And)
                        {
                            Conditions =
                            {
                                new MetadataConditionExpression("IsIntersect", MetadataConditionOperator.Equals, false)
                            }
                        }
                    }
                },
                Properties = new MetadataPropertiesExpression
                {
                    AllProperties = false,
                    PropertyNames = { "DisplayName", "SchemaName", "LogicalName", "CanBePrimaryEntityInRelationship", "CanBeRelatedEntityInRelationship" }
                }
            };

            RetrieveMetadataChangesRequest retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest
            {
                Query = entityQueryExpression,
                ClientVersionStamp = null
            };

            var response = (RetrieveMetadataChangesResponse)_service.Execute(retrieveMetadataChangesRequest);

            return response.EntityMetadata.Select(emd => new EntityInfo(emd)).OrderBy(e => e.Metadata.SchemaName).ToList();
        }

        private void LoadBaseLanguage()
        {
            var org = _service.RetrieveMultiple(new QueryExpression("organization")
            {
                NoLock = true,
                TopCount = 1,
                ColumnSet = new ColumnSet("languagecode")
            }).Entities.FirstOrDefault();

            if (org == null)
            {
                throw new Exception("Unable to find organization record");
            }

            languageCode = org.GetAttributeValue<int>("languagecode");
        }
    }
}