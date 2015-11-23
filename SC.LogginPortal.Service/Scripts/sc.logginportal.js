var page = 0;
var xhr = null;

function request() {
    if (xhr) {
        xhr.abort();
    }

    xhr = $.ajax({
        url: '/Home/JSON?page=' + page,
        dataType: 'json',
        success: function (data) {
            $('.results').append(data.HTML);
        }
    });
    return false;
}

$(function () {
    page = $('.results').data('pageid');
    $('body').on('click', '.results .page-next', function () {
        page++;
        $('.results').empty();
        request();
    });
    $('body').on('click', '.results .page-prev', function () {
        if (page > 1) {
            page--;
            $('.results').empty();
            request();
        }
    });

    $('body').on('change', '.results .facets input[type="checkbox"]', function () {
        $.ajax({

        });
    });
});