
var gicoSystem = (function () {

    var gicoInstance;

    function create(language) {
        function getUrl(url) {
            return "/" + language  + url;
        }

        function changeCart(productId, quantity,action) {
            var model= {
                ProductId: productId,
                Quantity: quantity,
                Action: action
            }
            $.ajax({
                method: "POST",
                dataType: "json",
                url: getUrl("/cart/add"),
                data: model
            })
                .done(function () {
                    alert("success");
                })
                .fail(function () {
                    alert("error");
                })
                .always(function () {
                    alert("complete");
                });
        }

        function locationSearch() {
            $('.locationSearch').select2({
                ajax: {
                    delay: 250,
                    url: function (params) {
                        return getUrl("/checkout/locationsearch/" + params.term);
                    },
                    dataType: 'json',
                    data: function (params) {
                       return null;
                    },
                    processResults: function (data) {
                        return {
                            results: data
                        };
                    },
                    cache: true
                },
                placeholder: 'Search for a repository',
                minimumInputLength: 2,
                escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
                templateResult: formatRepo,
                templateSelection: formatRepoSelection
            });
        }

        function formatRepo(repo) {
            if (repo.loading) {
                return repo.text;
            }

            var markup = "<div class='select2-result-repository clearfix'>"+ repo.fullAddress + "</div>";
            

            return markup;
        }

        function formatRepoSelection(repo) {
            return repo.fullAddress;
        }

        return {
            // public + private states and behaviors
            changeCart,
            locationSearch: locationSearch
        };
    }

    return {
        getInstance: function (language) {
            if (!gicoInstance) {
                gicoInstance = create(language);
            }
            return gicoInstance;
        }
    };

    //function Singleton() {
    //    if (!gicoInstance) {
    //        gicoInstance = intialize();
    //    }
    //};

})();

