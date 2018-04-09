using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Gico.Config;
using Gico.DataObject;
using Gico.CQRS.Service.Interfaces;
using Gico.Domains;

namespace Gico.SystemDataObject
{
    public class SqlBaseDao : BaseDao
    {
        public override string ConnectionString => ConfigSettingEnum.DbBackendConnectionString.GetConfig();
        
        public class ProcName
        {
            
            #region Category
            public const string Category_GetByLanguageId = "Category_GetByLanguageId";
            public const string Category_GetById = "Category_GetById";
            public const string Category_Add = "Category_Add";
            public const string Category_Change = "Category_Change";
            public const string Category_RemoveById = "Category_RemoveById";
            public const string Category_GetListAttr = "Category_GetListAttr";
            public const string Category_GetListManufacturer = "Category_GetListManufacturer";

            #endregion

            #region ManufacturerCategoryMapping
            public const string ManufacturerCategoryMapping_Delete = "Manufacturer_Category_Mapping_Delete";
            public const string ManufacturerCategoryMapping_GetListManufacturer = "Manufacturer_Gets";
            public const string ManufacturerCategoryMapping_Add = "ManufacturerMapping_Add";

            #endregion

            #region AttributeCategoryMapping
            public const string AttributeCategoryMapping_Add = "AttributeCategoryMapping_Add";
            public const string AttributeCategoryMapping_Change = "AttributeCategoryMapping_Change";
            public const string AttributeCategoryMapping_Get = "AttributeCategoryMapping_Get";
            public const string AttributeCategoryMapping_Delete = "AttributeCategoryMapping_Delete";
            public const string AttributeCategoryMapping_GetsProductAttr = "AttributeCategory_GetsProductAttr";


            #endregion

            #region ProductAttribute
            public const string ProductAttribute_Gets = "ProductAttribute_GetList";

            #endregion

            #region VariationTheme
            public const string VariationTheme_Get = "VariationTheme_Get";
            public const string VariationTheme_GetAttributes = "VariationTheme_GetAttributes";
            public const string Category_VariationTheme_Mapping_Add = "Category_VariationTheme_Mapping_Add";
            public const string Category_VariationTheme_Mapping_Insert_List = "Category_VariationTheme_Mapping_Insert_List";
            public const string Category_VariationTheme_Mapping_Remove = "Category_VariationTheme_Mapping_Remove";
            public const string Category_VariationTheme_Mapping_Get = "Category_VariationTheme_Mapping_Get";
            public const string VariationTheme_GetById = "VariationTheme_GetById";
            #endregion

            #region Language

            public const string Language_Get = "Language_Get";
            public const string Language_GetById = "Language_GetById";
            public const string Language_Search = "Language_Search";
            public const string Language_Add = "Language_Add";
            public const string Language_Change = "Language_Change";

            #endregion

            #region Customer

            public const string Customer_Add = "Customer_Add";
            public const string Customer_Change = "Customer_Change";
            public const string Customer_ChangeAndChangeCode = "Customer_ChangeAndChangeCode";
            public const string Customer_GetById = "Customer_GetById";
            public const string Customer_GetByEmailOrMobile = "Customer_GetByEmailOrMobile";
            public const string Customer_Search = "Customer_Search";
            public const string Customer_ChangeVersion = "Customer_ChangeVersion";
            #endregion

            #region CustomerExternalLogin

            public const string CustomerExternalLogin_Add = "CustomerExternalLogin_Add";
            public const string CustomerExternalLogin_GetByCustomerId = "CustomerExternalLogin_GetByCustomerId";

            #endregion

            #region ActionDefine

            public const string ActionDefine_GetById = "ActionDefine_GetById";
            public const string ActionDefine_GetByIds = "ActionDefine_GetByIds";
            public const string ActionDefine_Search = "ActionDefine_Search";
            public const string ActionDefine_Add = "ActionDefine_Add";

            #endregion

            #region Department

            public const string Department_Search = "Department_Search";
            public const string Department_Add = "Department_Add";
            public const string Department_Change = "Department_Change";
            public const string Department_Get = "Department_Get";
            public const string Department_GetByIds = "Department_GetByIds";

            #endregion

            #region Role

            public const string Role_GetByDepartmentId = "Role_GetByDepartmentId";
            public const string Role_Search = "Role_Search";
            public const string Role_GetById = "Role_GetById";
            public const string Role_Add = "Role_Add";
            public const string Role_Change = "Role_Change";
            public const string Role_Action_Mapping_GetByCustomerId = "Role_Action_Mapping_GetByCustomerId";
            public const string Role_Action_Mapping_GetByRoleId = "Role_Action_Mapping_GetByRoleId";
            public const string Role_Action_Mapping_RemoveByRoleIdAndActionIds = "Role_Action_Mapping_RemoveByRoleIdAndActionIds";
            #endregion

            #region Role

            public const string Customer_Role_Mapping_DeleteByCustomerId = "Customer_Role_Mapping_DeleteByCustomerId";
            public const string Customer_Role_Mapping_DeleteByRoleId = "Customer_Role_Mapping_DeleteByRoleId";

            #endregion

            #region LocaleStringResource

            public const string Locale_String_Resource_GetById = "Locale_String_Resource_GetById";
            public const string Locale_String_Resource_GetByLanguageId = "Locale_String_Resource_GetByLanguageId";
            public const string Locale_String_Resource_Search = "Locale_String_Resource_Search";
            public const string Locale_String_Resource_Add = "Locale_String_Resource_Add";
            public const string Locale_String_Resource_Change = "Locale_String_Resource_Change";

            #endregion

            #region MeasureUnit

            public const string Measure_Unit_Search = "Measure_Unit_Search";
            public const string Measure_Unit_Add = "Measure_Unit_Add";
            public const string Measure_Unit_Change = "Measure_Unit_Change";
            public const string Measure_Unit_GetById = "Measure_Unit_GetById";


            #endregion

            #region Vendor

            public const string Vendor_Add = "Vendor_Add";
            public const string Vendor_Change = "Vendor_Change";
            public const string Vendor_ChangeAndChangeCode = "Vendor_ChangeAndChangeCode";
            public const string Vendor_GetById = "Vendor_GetById";
            public const string Vendor_GetByIds = "Vendor_GetByIds";
            public const string Vendor_Search = "Vendor_Search";
            public const string Vendor_SearchByKeyword = "Vendor_SearchByKeyword";

            #endregion

            #region ProductAttribute

            public const string ProductAttribute_Search = "ProductAttribute_Search";
            public const string ProductAttribute_Get = "ProductAttribute_Get";
            public const string ProductAttribute_GetByIds = "ProductAttribute_GetByIds";
            public const string ProductAttribute_Add = "ProductAttribute_Add";
            public const string ProductAttribute_Update = "ProductAttribute_Update";

            public const string ProductAttributeValue_Search = "ProductAttributeValue_Search";
            public const string ProductAttributeValue_Get = "ProductAttributeValue_Get";
            public const string ProductAttributeValue_GetByIds = "ProductAttributeValue_GetByIds";
            public const string ProductAttributeValue_Add = "ProductAttributeValue_Add";
            public const string ProductAttributeValue_Update = "ProductAttributeValue_Update";

            #endregion

            #region Manufacturer
            public const string Manufacturer_GetAll = "Manufacturer_GetAll";
            public const string Manufacturer_SearchByName = "Manufacturer_Search";
            public const string Manufacturer_GetById = "Manufacturer_GetById";
            public const string Manufacturer_GetByIds = "Manufacturer_GetByIds";
            public const string Manufacturer_Add = "Manufacturer_Add";            
            public const string Manufacturer_Change = "ManufacturerManagement_Change";
            
            #endregion


            #region Warehouse

            public const string Warehouse_Search = "Warehouse_Search";
            public const string Warehouse_GetByIds = "Warehouse_GetByIds";
            public const string Warehouse_SearchByKeyword = "Warehouse_SearchByKeyword";
            public const string Warehouse_GetById = "Warehouse_GetById";
            #endregion

            #region Product

            public const string Product_SearchByCodeOrName = "Product_SearchByCodeOrName";
            public const string Product_GetByIds = "Product_GetByIds";

            #endregion
        }
    }
}
