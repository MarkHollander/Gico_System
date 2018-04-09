using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gico.Config;
using Gico.Domains;
using Gico.ReadSystemModels;
using Gico.SystemCommands;
using Gico.SystemEvents.Cache;

namespace Gico.SystemDomains
{
    public class ActionDefine : BaseDomain
    {
        public ActionDefine()
        {

        }
        public void Init(ActionDefineAddCommand command)
        {
            Id = command.Id;
            Group = command.Group;
            Name = command.Name;
            CreateAddEvent();
        }
        public void CreateAddEvent()
        {
            var @event = this.ToAddOrChangeEvent();
            AddEvent(@event);
        }

        public string Name { get; private set; }
        public string Group { get; private set; }

        #region Convert

        public ActionDefineAddEvent ToAddOrChangeEvent()
        {
            return new ActionDefineAddEvent()
            {
                Id = this.Id,
                Name = this.Name,
                Group = this.Group
            };
        }

        #endregion
    }

    public class RoleActionMapping : BaseDomain
    {
        public RoleActionMapping()
        {

        }
        public RoleActionMapping(RRoleActionMapping roleActionMapping)
        {
            RoleId = roleActionMapping.RoleId;
            ActionId = roleActionMapping.ActionId;
            Attributes = roleActionMapping.Attributes;
        }
        public RoleActionMapping(string roleId, string actionId, string attributes)
        {
            RoleId = roleId;
            ActionId = actionId;
            Attributes = attributes ?? string.Empty;
        }
        public string RoleId { get; private set; }
        public string ActionId { get; private set; }
        public string Attributes { get; private set; }


        public DataRow AddToDataTable(DataTable dataTable)
        {
            DataRow row = dataTable.NewRow();
            row["RoleId"] = RoleId;
            row["ActionId"] = ActionId;
            row["Attributes"] = Attributes;
            dataTable.Rows.Add(row);
            return row;
        }

        public static DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable("Role_Action_Mapping");
            dataTable.Columns.Add("RoleId", typeof(string));
            dataTable.Columns.Add("ActionId", typeof(string));
            dataTable.Columns.Add("Attributes", typeof(string));
            return dataTable;
        }
    }

    public class Role : BaseDomain
    {
        public Role()
        {
            RoleActionMappings = new List<RoleActionMapping>();
        }

        public Role(RRole role, RRoleActionMapping[] roleActionMappings)
        {
            Id = role.Id;
            Name = role.Name;
            DepartmentId = role.DepartmentId;
            Status = role.Status;
            RoleActionMappings = roleActionMappings.Select(p => new RoleActionMapping(p)).ToList();
        }
        public string Name { get; private set; }
        public string DepartmentId { get; private set; }
        public new EnumDefine.RoleStatusEnum Status { get; private set; }
        public IList<RoleActionMapping> RoleActionMappings { get; private set; }
        public void Init(RoleAddCommand mesage)
        {
            Id = mesage.Id;
            Name = mesage.Name;
            Status = mesage.Status;
            DepartmentId = mesage.DepartmentId;
        }
        public void Init(RoleChangeCommand mesage)
        {
            Id = mesage.Id;
            Name = mesage.Name;
            Status = mesage.Status;
            DepartmentId = mesage.DepartmentId;
        }

        public void ChangePermissionByRole(RoleActionMappingChangeByRoleCommand command, out IList<RoleActionMapping> roleActionMappingsAdd, out IList<string> actionIdsRemove)
        {
            roleActionMappingsAdd = new List<RoleActionMapping>();
            actionIdsRemove = new List<string>();
            if (command.ActionIdsRemove?.Length > 0)
            {
                foreach (var s in command.ActionIdsRemove)
                {
                    RoleActionMapping roleActionMapping = Remove(command.RoleId, s);
                    if (roleActionMapping != null)
                    {
                        actionIdsRemove.Add(s);
                    }
                }
            }
            if (command.ActionIdsAdd?.Length > 0)
            {
                foreach (var s in command.ActionIdsAdd)
                {
                    RoleActionMapping roleActionMapping = Add(command.RoleId, s, string.Empty);
                    if (roleActionMapping != null)
                    {
                        roleActionMappingsAdd.Add(roleActionMapping);
                    }
                }
            }
        }

        public RoleActionMapping Add(string roleId, string actionId, string attributes)
        {
            if (RoleActionMappings.Any(p => p.RoleId == roleId && p.ActionId == actionId))
            {
                return null;
            }
            RoleActionMapping roleActionMapping = new RoleActionMapping(roleId, actionId, attributes);
            RoleActionMappings.Add(roleActionMapping);
            return roleActionMapping;
        }
        public RoleActionMapping Remove(string roleId, string actionId)
        {
            RoleActionMapping roleActionMapping =
                RoleActionMappings.FirstOrDefault(p => p.RoleId == roleId && p.ActionId == actionId);
            if (roleActionMapping == null)
            {
                return null;
            }
            RoleActionMappings.Remove(roleActionMapping);
            return roleActionMapping;
        }
    }

    public class Department : BaseDomain
    {
        public string Name { get; private set; }
        public new EnumDefine.DepartmentStatusEnum Status { get; private set; }

        public void Init(DepartmentAddCommand mesage)
        {
            Id = mesage.Id;
            Name = mesage.Name;
            Status = mesage.Status;
        }
        public void Init(DepartmentChangeCommand mesage)
        {
            Id = mesage.Id;
            Name = mesage.Name;
            Status = mesage.Status;
        }
    }

    public class CustomerRoleMapping : BaseDomain
    {
        public string CustomerId { get; private set; }
        public string RoleId { get; private set; }

        public DataRow AddToDataTable(DataTable dataTable)
        {
            DataRow row = dataTable.NewRow();
            row["CustomerId"] = CustomerId;
            row["RoleId"] = RoleId;
            dataTable.Rows.Add(row);
            return row;
        }

        public static DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable("Customer_Role_Mapping");
            dataTable.Columns.Add("CustomerId", typeof(string));
            dataTable.Columns.Add("RoleId", typeof(string));
            return dataTable;
        }
    }

    public class ProductAttribute : BaseDomain
    {
        public string Name { get; set; }
        public new EnumDefine.StatusEnum Status { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; }

        public void Init(ProductAttributeCommand mesage)
        {
            Id = mesage.Id;
            Name = mesage.Name;
            Status = mesage.Status;
            CreatedOnUtc = mesage.CreatedDateUtc;
            UpdatedOnUtc = mesage.UpdatedOnUtc;
            CreatedUserId = mesage.CreatedUserId;
            UpdatedUserId = mesage.UpdatedUserId;
        }
    }

    public class ProductAttributeValue : BaseDomain
    {
        public string AttributeId { get; set; }
        public string Value { get; set; }
        public int UnitId { get; set; }
        public EnumDefine.StatusEnum AttributeValueStatus { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; }
        public int DisplayOrder { get; set; }

        public void Init(ProductAttributeValueCommand mesage)
        {
            Id = mesage.AttributeValueId;
            AttributeId = mesage.AttributeId;
            Value = mesage.Value;
            UnitId = mesage.UnitId;
            AttributeValueStatus = mesage.AttributeValueStatus;
            CreatedOnUtc = mesage.CreatedDateUtc;
            UpdatedOnUtc = mesage.UpdatedOnUtc;
            CreatedUserId = mesage.CreatedUserId;
            UpdatedUserId = mesage.UpdatedUserId;
            DisplayOrder = mesage.DisplayOrder;
        }
    }
}