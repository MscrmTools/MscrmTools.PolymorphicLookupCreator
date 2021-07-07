using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace MscrmTools.PolymorphicLookupCreator.AppCode
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

        public void AddRelationship(string prefix, string polymorphicLookupName, EntityMetadata emd, EntityMetadata emdReferenced, string solutionUniqueName)
        {
            try
            {
                _service.Execute(new CreateOneToManyRequest
                {
                    Lookup = (LookupAttributeMetadata)emd.Attributes.First(a => a.LogicalName == polymorphicLookupName.ToLower()),
                    OneToManyRelationship = new OneToManyRelationshipMetadata
                    {
                        SchemaName = $"{prefix}_{emd.LogicalName}_{emdReferenced.LogicalName}_{polymorphicLookupName}",
                        ReferencedEntity = emdReferenced.LogicalName,
                        ReferencedAttribute = emdReferenced.PrimaryIdAttribute,
                        ReferencingEntity = emd.LogicalName,
                        ReferencingAttribute = polymorphicLookupName
                    },
                    SolutionUniqueName = solutionUniqueName
                });
            }
            catch (FaultException<OrganizationServiceFault> error)
            {
                if (error.Detail.ErrorCode == -2147192813)
                {
                    throw new Exception("This lookup is not a polymorphic lookup");
                }
                else throw;
            }
        }

        public Guid CreatePolymorphicLookup(string prefix, string displayName, string schemaName, string referencingEntity, string[] referencedEntities, string solutionUniqueName)
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
                    },
                    {
                        "SolutionUniqueName", solutionUniqueName}
                }
            };

            var response = _service.Execute(request);

            return new Guid(response.Results["AttributeId"].ToString());
        }

        public void DeleteAttribute(string polymorphicLookupName, string entity)
        {
            _service.Execute(new DeleteAttributeRequest
            {
                LogicalName = polymorphicLookupName,
                EntityLogicalName = entity
            });
        }

        public void DeleteRelationship(string polymorphicLookupName, EntityMetadata emd, string referencedEntity)
        {
            var relationship = emd.ManyToOneRelationships.FirstOrDefault(r => r.ReferencedEntity == referencedEntity && r.ReferencingAttribute == polymorphicLookupName.ToLower());
            if (relationship == null)
            {
                throw new Exception($"Unable to find relationship between entities {emd.LogicalName} and {referencedEntity} for lookup {polymorphicLookupName}");
            }

            _service.Execute(new DeleteRelationshipRequest
            {
                Name = relationship.SchemaName
            });
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
                    PropertyNames = { "DisplayName", "SchemaName", "LogicalName", "PrimaryIdAttribute", "CanBePrimaryEntityInRelationship", "CanBeRelatedEntityInRelationship", "Attributes", "OneToManyRelationships", "ManyToOneRelationships" }
                },
                AttributeQuery = new AttributeQueryExpression
                {
                    Properties = new MetadataPropertiesExpression
                    {
                        AllProperties = false,
                        PropertyNames = { "DisplayName", "SchemaName", "LogicalName", "Targets" }
                    },
                    Criteria = new MetadataFilterExpression
                    {
                        Conditions =
                        {
                            new MetadataConditionExpression("IsManaged", MetadataConditionOperator.Equals, false),
                            new MetadataConditionExpression("AttributeType", MetadataConditionOperator.Equals, AttributeTypeCode.Lookup)
                        }
                    }
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

        public List<EntityMetadata> GetPolymorphicLookups()
        {
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
                    PropertyNames = { "DisplayName", "SchemaName", "LogicalName" }
                },
                AttributeQuery = new AttributeQueryExpression
                {
                    Properties = new MetadataPropertiesExpression
                    {
                        AllProperties = false,
                        PropertyNames = { "DisplayName", "SchemaName", "Targets" }
                    },
                    Criteria = new MetadataFilterExpression(LogicalOperator.And)
                    {
                        Conditions = {
                            new MetadataConditionExpression("IsManaged", MetadataConditionOperator.Equals, false),
                            new MetadataConditionExpression("AttributeType", MetadataConditionOperator.Equals, "Lookup")
                        }
                    }
                }
            };

            RetrieveMetadataChangesRequest retrieveMetadataChangesRequest = new RetrieveMetadataChangesRequest
            {
                Query = entityQueryExpression,
                ClientVersionStamp = null
            };

            var response = (RetrieveMetadataChangesResponse)_service.Execute(retrieveMetadataChangesRequest);

            return response.EntityMetadata.Where(e => e.Attributes.Any(a => ((LookupAttributeMetadata)a).Targets.Length > 1)).OrderBy(e => e.SchemaName).ToList();
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