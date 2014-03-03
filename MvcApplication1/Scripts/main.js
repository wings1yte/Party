$(document).ready(function () {
    $('a').map(function () {
        $(this).click(function (e) {
            
            e.preventDefault();
            $.get(
                $(this).attr("href"),
                function (data) {
                    $('#home').html(data)
                },
            'html');
        })
    }, this);

    var goForm = function (data) {
        $('#home').html(data);
    }
})