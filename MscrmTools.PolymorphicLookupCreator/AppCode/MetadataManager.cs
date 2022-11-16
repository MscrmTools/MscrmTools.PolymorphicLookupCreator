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

    public class RelationshipInfo
    {
        private readonly OneToManyRelationshipMetadata _cached;
        private readonly OneToManyRelationshipMetadata _omr;

        public RelationshipInfo(EntityMetadata referencedEntity, EntityMetadata referencingEntity, OneToManyRelationshipMetadata omr, string prefix)
        {
            _omr = omr;
            _cached = new OneToManyRelationshipMetadata();

            //if (IsNew)
            //{
            //    _omr.SchemaName = $"{prefix}_{referencingEntity.LogicalName}_{referencedEntity.LogicalName}_{polymorphicLookupName}";
            //}

            CacheInitialValues();
        }

        public bool IsNew { get; set; }

        public bool IsUpdated
        {
            get
            {
                return _cached.SchemaName != _omr.SchemaName
                  || _cached.AssociatedMenuConfiguration.Behavior != _omr.AssociatedMenuConfiguration.Behavior
                  || _cached.AssociatedMenuConfiguration.Group != _omr.AssociatedMenuConfiguration.Group
                  || (_cached.AssociatedMenuConfiguration.Order ?? 10000) != (_omr.AssociatedMenuConfiguration.Order ?? 10000)
                  || (_cached.CascadeConfiguration.Assign ?? CascadeType.NoCascade) != (_omr.CascadeConfiguration.Assign ?? CascadeType.NoCascade)
                  || (_cached.CascadeConfiguration.Delete ?? CascadeType.NoCascade) != (_omr.CascadeConfiguration.Delete ?? CascadeType.NoCascade)
                  || (_cached.CascadeConfiguration.Merge ?? CascadeType.NoCascade) != (_omr.CascadeConfiguration.Merge ?? CascadeType.NoCascade)
                  || (_cached.CascadeConfiguration.Reparent ?? CascadeType.NoCascade) != (_omr.CascadeConfiguration.Reparent ?? CascadeType.NoCascade)
                  || (_cached.CascadeConfiguration.Share ?? CascadeType.NoCascade) != (_omr.CascadeConfiguration.Share ?? CascadeType.NoCascade)
                  || (_cached.CascadeConfiguration.Unshare ?? CascadeType.NoCascade) != (_omr.CascadeConfiguration.Unshare ?? CascadeType.NoCascade)
                  || (_cached.IsValidForAdvancedFind ?? false) != (_omr.IsValidForAdvancedFind ?? false)
                  || _cached.AssociatedMenuConfiguration?.Label?.UserLocalizedLabel?.Label != _omr.AssociatedMenuConfiguration?.Label?.UserLocalizedLabel?.Label
                  || AreLabelCollectionsDifferent(_cached.AssociatedMenuConfiguration?.Label?.LocalizedLabels, _omr.AssociatedMenuConfiguration?.Label?.LocalizedLabels);
            }
        }

        public OneToManyRelationshipMetadata Relation
        {
            get
            {
                return _omr;
            }
        }

        private bool AreLabelCollectionsDifferent(LocalizedLabelCollection c1, LocalizedLabelCollection c2)
        {
            if (c1 == null || c2 == null) return false;

            return c1.Any(c => c2.FirstOrDefault(x => x.LanguageCode == c.LanguageCode && x.Label != c.Label) != null)
                || c2.Any(c => c1.FirstOrDefault(x => x.LanguageCode == c.LanguageCode && x.Label != c.Label) != null)
                || !c1.All(c => c2.FirstOrDefault(x => x.LanguageCode == c.LanguageCode) != null)
                || !c2.All(c => c1.FirstOrDefault(x => x.LanguageCode == c.LanguageCode) != null);
        }

        private void CacheInitialValues()
        {
            _cached.SchemaName = _omr.SchemaName;
            _cached.AssociatedMenuConfiguration = new AssociatedMenuConfiguration();
            _cached.AssociatedMenuConfiguration.Behavior = _omr.AssociatedMenuConfiguration?.Behavior ?? new AssociatedMenuBehavior();
            _cached.AssociatedMenuConfiguration.Group = _omr.AssociatedMenuConfiguration?.Group ?? AssociatedMenuGroup.Details;
            _cached.AssociatedMenuConfiguration.Order = _omr.AssociatedMenuConfiguration?.Order ?? 10000;
            _cached.CascadeConfiguration = new CascadeConfiguration();
            _cached.CascadeConfiguration.Assign = _omr.CascadeConfiguration?.Assign ?? CascadeType.NoCascade;
            _cached.CascadeConfiguration.Delete = _omr.CascadeConfiguration?.Delete ?? CascadeType.RemoveLink;
            _cached.CascadeConfiguration.Merge = _omr.CascadeConfiguration?.Merge ?? CascadeType.NoCascade;
            _cached.CascadeConfiguration.Reparent = _omr.CascadeConfiguration?.Reparent ?? CascadeType.NoCascade;
            _cached.CascadeConfiguration.Share = _omr.CascadeConfiguration?.Share ?? CascadeType.NoCascade;
            _cached.CascadeConfiguration.Unshare = _omr.CascadeConfiguration?.Unshare ?? CascadeType.NoCascade;
            _cached.IsValidForAdvancedFind = _omr.IsValidForAdvancedFind;

            _cached.AssociatedMenuConfiguration.Label = new Label();

            if (_omr.AssociatedMenuConfiguration?.Label?.UserLocalizedLabel != null)
            {
                _cached.AssociatedMenuConfiguration.Label.UserLocalizedLabel = new LocalizedLabel(_omr.AssociatedMenuConfiguration.Label.UserLocalizedLabel.Label, _omr.AssociatedMenuConfiguration.Label.UserLocalizedLabel.LanguageCode);
            }

            if (_omr.AssociatedMenuConfiguration?.Label?.LocalizedLabels != null)
            {
                foreach (var lbl in _omr.AssociatedMenuConfiguration?.Label?.LocalizedLabels)
                {
                    _cached.AssociatedMenuConfiguration.Label.LocalizedLabels.Add(new LocalizedLabel(lbl.Label, lbl.LanguageCode));
                }
            }
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

        public int LanguageCode => languageCode;

        public void AddRelationship(string prefix, string polymorphicLookupName, EntityMetadata emd, EntityMetadata emdReferenced, OneToManyRelationshipMetadata omd, string solutionUniqueName)
        {
            try
            {
                omd.ReferencedEntity = emdReferenced.LogicalName;
                omd.ReferencedAttribute = emdReferenced.PrimaryIdAttribute;
                omd.ReferencingEntity = emd.LogicalName;
                omd.ReferencingAttribute = polymorphicLookupName;

                _service.Execute(new CreateOneToManyRequest
                {
                    Lookup = (LookupAttributeMetadata)emd.Attributes.First(a => a.LogicalName == polymorphicLookupName.ToLower()),
                    OneToManyRelationship = omd,
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

        public Guid CreatePolymorphicLookup(string prefix, string displayName, string schemaName, string referencingEntity, string[] referencedEntities, RelationshipInfo[] rels, string solutionUniqueName)
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

                relations[i] = rels.FirstOrDefault(r => r?.Relation?.ReferencedEntity == referencedEntity)?.Relation;
                if (relations[i] == null)
                {
                    relations[i] = new OneToManyRelationshipMetadata
                    {
                        SchemaName = $"{prefix}{referencingEntity}_{referencedEntity}_{schemaName}",
                        ReferencedEntity = referencedEntity
                    };
                }

                relations[i].ReferencingEntity = referencingEntity;

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
                            // You can create extra relations on managed lookups
                            //new MetadataConditionExpression("IsManaged", MetadataConditionOperator.Equals, false),
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

        internal void UpdateRelationship(OneToManyRelationshipMetadata toUpdate)
        {
            _service.Execute(new UpdateRelationshipRequest
            {
                Relationship = toUpdate,
                MergeLabels = true
            });
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
