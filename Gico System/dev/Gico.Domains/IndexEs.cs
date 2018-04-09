using System;
using Gico.Common;
using Gico.Config;

namespace Gico.Domains
{
    public class IndexEs<T>
    {
        public IndexEs(EnumDefine.EsIndexName indexName, EnumDefine.EsIndexType indexType, object id, T indexObject)
        {
            IndexName = indexName;
            IndexType = indexType;
            IndexObject = indexObject;
            Id = id;
        }
        public IndexEs(EnumDefine.EsIndexName indexName, EnumDefine.EsIndexType indexType, object id, object parentId, T indexObject)
        {
            IndexName = indexName;
            IndexType = indexType;
            IndexObject = indexObject;
            Id = id;
            ParentId = parentId;
            HasParent = true;
        }
        public EnumDefine.EsIndexName IndexName { get; private set; }
        public EnumDefine.EsIndexType IndexType { get; private set; }
        public object Id { get; private set; }
        public object ParentId { get; private set; }
        public T IndexObject { get; private set; }
        public bool HasParent { get; private set; }

        public string IndexAddUrl => $"{IndexName}/{IndexType}/{Id}".ToLower();
        public string IndexAddScript => Serialize.JsonSerializeObject(IndexObject);

        public string IndexBulkScript
        {
            get
            {
                string parentScript = string.Empty;
                if (HasParent)
                {
                    if (ParentId == null)
                    {
                        parentScript = ",\"parent\":\"null\"";
                    }
                    else
                    {
                        parentScript = ",\"parent\":\"" + ParentId + "\"";
                    }

                }
                string script = $"{{ \"index\" : {{ \"_index\" : \"{IndexName}\", \"_type\" : \"{IndexType}\", \"_id\" : \"{Id}\" {parentScript} }} }}".ToLower();
                script += Environment.NewLine;
                script += IndexAddScript;
                script += Environment.NewLine;
                return script;
            }
        }
    }
    public class EsAddResult
    {
        public int took { get; set; }
        public bool errors { get; set; }
        public EsAddResultItem[] items { get; set; }
    }

    public class EsAddResultItem
    {
        public EsAddResultDetail index { get; set; }
        public EsAddResultDetail update { get; set; }
    }
    public class EsAddResultDetail
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public int _version { get; set; }
        public string result { get; set; }
        public _Shards _shards { get; set; }
        public bool created { get; set; }
    }

    public class _Shards
    {
        public int total { get; set; }
        public int successful { get; set; }
        public int failed { get; set; }
    }

    public class EsSearchResult<T>
    {
        public ObjectSearchResult<T> hits { get; set; }
    }
    public class EsSearchAggsResult<T>
    {
        public aggregations<T> aggregations { get; set; }
    }

    public class ObjectSearchResult<T>
    {
        public int total { get; set; }
        public ObjectSearchData<T>[] hits { get; set; }
    }

    public class ObjectSearchData<T>
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public double? _score { get; set; }
        public T _source { get; set; }
        public double?[] sort { get; set; }

    }

    public class aggregations<T>
    {
        public aggs_groups<T> aggs_groups { get; set; }
    }

    public class aggs_groups<T>
    {
        public int doc_count_error_upper_bound { get; set; }
        public int sum_other_doc_count { get; set; }
        public buckets<T>[] buckets { get; set; }
    }

    public class buckets<T>
    {
        public T key { get; set; }
        public int doc_count { get; set; }
    }

    public class EsDeleteByQueryResult
    {
        public int took { get; set; }
        public bool timed_out { get; set; }
        public int deleted { get; set; }
        public int batches { get; set; }
        public int version_conflicts { get; set; }
        public int noops { get; set; }
        public Retries retries { get; set; }
        public int throttled_millis { get; set; }
        public float requests_per_second { get; set; }
        public int throttled_until_millis { get; set; }
        public int total { get; set; }
        public object[] failures { get; set; }

        public class Retries
        {
            public int bulk { get; set; }
            public int search { get; set; }
        }

    }
}