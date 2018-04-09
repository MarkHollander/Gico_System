using System.Collections.Generic;
using System.Linq;
using ProtoBuf;
using Gico.Config;


namespace Gico.ReadSystemModels
{
    [ProtoContract]
    public class RCategory:BaseReadModel
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string ParentId { get; set; }
        [ProtoMember(3)]
        public string Description { get; set; }
     
        [ProtoMember(4)]
        public int DisplayOrder { get; set; }

        [ProtoMember(5)]
        public string Logos { get; set; }

        [ProtoMember(16)]
        public new EnumDefine.CategoryStatus Status { get; set; }

        public static Dictionary<string, IList<RCategory>> BuildTree(RCategory[] categories)
        {
            Dictionary<string, IList<RCategory>> dictionary = new Dictionary<string, IList<RCategory>>();
            foreach (var rCategory in categories)
            {
                string key = string.IsNullOrEmpty(rCategory.ParentId) ? "#" : rCategory.ParentId;
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, new List<RCategory>());
                }
                dictionary[key].Add(rCategory);
            }
            return dictionary;
        }

        public static RCategory[] RemoveCurrentTree(RCategory[] categories, RCategory category)
        {
            Dictionary<string, IList<RCategory>> dictionary = BuildTree(categories);
            IList<RCategory> categoriesRemove = new List<RCategory>();
            if (dictionary.ContainsKey(category.Id))
            {
                var childs = dictionary[category.Id];
                foreach (var child in childs)
                {
                    //dictionary[menu.Id].Remove(child);
                    categoriesRemove.Add(child);
                    RemoveChild(dictionary, child, ref categoriesRemove);
                }
            }
            if (dictionary.ContainsKey(category.ParentId))
            {
                categoriesRemove.Add(category);
                RemoveChild(dictionary, category, ref categoriesRemove);
            }

            var rootItem = dictionary["#"].FirstOrDefault(p => p.Id == category.Id);
            if (rootItem != null)
            {
                dictionary["#"].Remove(rootItem);
            }
            if (categoriesRemove.Count > 0)
            {
                foreach (var rCategory in categoriesRemove)
                {
                    dictionary[rCategory.ParentId].Remove(rCategory);
                }
            }
            return dictionary.Values.SelectMany(p => p).ToArray();
        }

        private static void RemoveChild(Dictionary<string, IList<RCategory>> dictionary, RCategory category, ref IList<RCategory> categoriesRemove)
        {
            if (dictionary.ContainsKey(category.Id))
            {
                var childs = dictionary[category.Id];
                foreach (var child in childs)
                {
                    //dictionary[menu.Id].Remove(child);
                    categoriesRemove.Add(child);
                    RemoveChild(dictionary, child, ref categoriesRemove);
                }
            }
        }


    }

    [ProtoContract]
    public class RCategoryAttr : BaseReadModel
    {
        [ProtoMember(1)]
        public bool IsFilter { get; set; }
        [ProtoMember(2)]
        public int BaseUnitId { get; set; }
        [ProtoMember(3)]
        public int DisplayOrder { get; set; }

        [ProtoMember(4)]
        public string AttributeName { get; set; }
        [ProtoMember(5)]
        public int AttributeId { get; set; }
        [ProtoMember(6)]
        public EnumDefine.AttrCategoryType AttributeType { get; set; }


    }

}
