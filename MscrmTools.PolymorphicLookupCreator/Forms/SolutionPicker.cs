﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.PolymorphicLookupCreator.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;

namespace MsCrmTools.PolymorphicLookupCreator.Forms
{
    public partial class SolutionPicker : Form
    {
        private readonly IOrganizationService innerService;

        public SolutionPicker(IOrganizationService service)
        {
            InitializeComponent();

            innerService = service;
        }

        public List<Entity> SelectedSolution { get; set; }

        private void btnSolutionPickerCancel_Click(object sender, EventArgs e)
        {
            SelectedSolution = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSolutionPickerValidate_Click(object sender, EventArgs e)
        {
            if (lstSolutions.SelectedItems.Count > 0)
            {
                SelectedSolution = lstSolutions.SelectedItems.Cast<ListViewItem>().Select(i => (Entity)i.Tag).ToList();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Please select at least one solution!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lstSolutions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var list = (ListView)sender;
            list.Sorting = list.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            list.ListViewItemSorter = new ListViewItemComparer(e.Column, list.Sorting);
        }

        private void lstSolutions_DoubleClick(object sender, EventArgs e)
        {
            btnSolutionPickerValidate_Click(null, null);
        }

        private EntityCollection RetrieveSolutions()
        {
            try
            {
                return innerService.RetrieveMultiple(new QueryExpression("solution")
                {
                    NoLock = true,
                    ColumnSet = new ColumnSet(true),
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
                });
            }
            catch (Exception error)
            {
                if (error.InnerException is FaultException)
                {
                    throw new Exception("Error while retrieving solutions: " + (error.InnerException).Message);
                }

                throw new Exception("Error while retrieving solutions: " + error.Message);
            }
        }

        private void SolutionPicker_Load(object sender, EventArgs e)
        {
            lstSolutions.Items.Clear();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = RetrieveSolutions();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Entity solution in ((EntityCollection)e.Result).Entities)
            {
                ListViewItem item = new ListViewItem(solution["friendlyname"].ToString());
                item.SubItems.Add(solution["version"].ToString());
                item.SubItems.Add(((EntityReference)solution["publisherid"]).Name);
                item.Tag = solution;

                lstSolutions.Items.Add(item);
            }

            lstSolutions.Enabled = true;
            btnSolutionPickerValidate.Enabled = true;
        }
    }
}