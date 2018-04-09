PUT addressesbase
{
    "settings": {
        "analysis": {
            "filter": {
                "ngram_filter": {
                    "type": "ngram",
                        "min_gram": 1,
                        "max_gram": 10
                }
            },
            "analyzer": {
                "index_analyzer": {
                    "type": "custom",
                        "tokenizer": "standard",
                        "filter": [
                        "standard",
                        "lowercase",
                        "asciifolding",
                        "ngram_filter"
                    ]
                },
                "search_analyzer": {
                    "type": "custom",
                        "tokenizer": "standard",
                        "filter": [
                        "standard",
                        "lowercase",
                        "asciifolding"
                    ]
                }
            }
        }
    },
    "mappings": {
        "addressbase": {
            "properties": {
                "FullAddress": {
                    "type": "text",
                        "analyzer": "index_analyzer",
                        "search_analyzer": "search_analyzer"
                }
            }
        }
    }
}