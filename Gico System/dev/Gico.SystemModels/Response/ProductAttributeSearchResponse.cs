using System;
using System.Linq;
using Gico.Config;
using Gico.Models.Response;

namespace Gico.SystemModels.Response
{
    #region ProductAttribute

    public class ProductAttributeSearchResponse : BaseResponse
    {
        public ProductAttributeSearchResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.StatusEnum));
        }

        public ProductAttributeViewModel[] ProductAttributes { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ProductAttributeGetResponse : BaseResponse
    {
        public ProductAttributeGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.StatusEnum));
        }

        public ProductAttributeViewModel ProductAttribute { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ProductAttributeViewModel
    {
        public string AttributeId { get; set; }
        public string AttributeName { get; set; }
        public EnumDefine.StatusEnum AttributeStatus { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; }
        public string StatusName => AttributeStatus.ToString();
        public ProductAttributeValueViewModel[] AttributeValues { get; set; }

        public string[] AttributeValueIds
        {
            get { return AttributeValues?.Select(p => p.AttributeValueId).ToArray(); }
        }

        public string Id => AttributeId;

        public string Name => AttributeName;
    }

    #endregion

    #region ProductAttributeValue

    public class ProductAttributeValueSearchResponse : BaseResponse
    {
        public ProductAttributeValueSearchResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.StatusEnum));
        }

        public ProductAttributeValueViewModel[] ProductAttributeValues { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ProductAttributeValueGetResponse : BaseResponse
    {
        public ProductAttributeValueGetResponse()
        {
            Statuses = KeyValueTypeIntModel.FromEnum(typeof(EnumDefine.StatusEnum));
        }

        public ProductAttributeValueViewModel ProductAttributeValue { get; set; }
        public KeyValueTypeIntModel[] Statuses { get; set; }
        public int TotalRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ProductAttributeValueViewModel
    {
        public string AttributeValueId { get; set; }
        public string AttributeId { get; set; }
        public string Value { get; set; }
        public int UnitId { get; set; }
        public EnumDefine.StatusEnum AttributeValueStatus { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; }
        public int DisplayOrder { get; set; }
        public string StatusName => AttributeValueStatus.ToString();
    }

    #endregion
}