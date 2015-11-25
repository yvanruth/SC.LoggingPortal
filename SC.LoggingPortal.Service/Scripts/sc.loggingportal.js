
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
    locked: false,

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
        $('body').on('click', '.results a.clearfilters', function () {
            ctx.locked = true;
            $('input.daterange').val('');
            $('.results .facets input[type="checkbox"]').removeAttr('checked');
            ctx.locked = false;
            ctx.page = 0;
            ctx.request();
        });
        $('body').on('click', '.results a.del-filter', function () {
            ctx.locked = true;
            var facet = $(this).data('facet'), value = $(this).data('filter');
            $('.results .facets input[type="checkbox"][name="' + facet + '"][value="' + value + '"]').removeAttr('checked');
            ctx.locked = false;
            ctx.page = 0;
            ctx.request();
        });
        
        $('body').on('change', '.results .facets input[type="checkbox"]', function () {
            ctx.page = 0;
            ctx.request();
        });
        ctx.resetEvents();
    },
    request: function () {
        var ctx = this;

        if (ctx.locked) {
            return;
        }

        if (ctx.xhr) {
            ctx.xhr.abort();
        }

        ctx.xhr = $.ajax({
            url: '/Home/JSON',
            data: {
                page: ctx.page,
                f: $('form#facetForm').serialize()
            },
            dataType: 'json',
            success: function (data) {
                $('.results').html(data.HTML);
                ctx.resetEvents();
            }, failure: function () {
                $('.results').html('Request failed');
                ctx.resetEvents();
            }
        });
        return false;
    },
    resetEvents: function () {
        var ctx = this;

        $('[data-toggle="popover"]').popover({ html: true, trigger: 'hover click' });
        $('input.daterange').daterangepicker({
            autoUpdateInput: false,
            showDropdowns: true,
            minDate: "01/01/2015",
            locale: {
                cancelLabel: 'Clear'
            }
        });

        $('input.daterange').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM-DD-YYYY') + ' | ' + picker.endDate.format('MM-DD-YYYY'));
            ctx.page = 0;
            ctx.request();
        });

        $('input.daterange').on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
            ctx.page = 0;
            ctx.request();
        });
    }
};

$(function () {
    sc.loggingportal.Search.init();
});