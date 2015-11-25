
if (typeof (sc) === "undefined") {
    var sc = {};
}
if (typeof (sc.loggingportal) === "undefined") {
    sc.loggingportal  = {};
}

sc.loggingportal.namespace = (function () {
})();

sc.loggingportal.Search = {
    page: 0,
    xhr: null,

    init: function () {
        var ctx = this;

        ctx.page = $('.results').data('pageid');
        $('body').on('click', '.results .page-next', function () {
            ctx.page++;
            ctx.request();
        });
        $('body').on('click', '.results .page-prev', function () {
            if (ctx.page > 1) {
                ctx.page--;
                ctx.request();
            }
        });

        $('body').on('change', '.results .facets input[type="checkbox"]', function () {
            ctx.request();
        });
    },
    request: function () {
        var ctx = this;

        if (ctx.xhr) {
            ctx.xhr.abort();
        }

        ctx.xhr = $.ajax({
            url: '/Home/JSON',
            data: {
                page: ctx.page,
                f: ctx.getFacets(),
            },
            dataType: 'json',
            success: function (data) {
                $('.results').html(data.HTML);
            }, failure: function () {
                $('.results').html('Request failed');
            }
        });
        return false;
    },
    getFacets: function () {

    }
};

$(function () {
    sc.loggingportal.Search.init();
});