if (typeof (sc) === "undefined") {
    var sc = {};
}
if (typeof (sc.loggingportal) === "undefined") {
    sc.loggingportal = {};
}

sc.loggingportal.namespace = (function () {
})();

sc.loggingportal.SignalR = {
    Hub: null,

    init:function() {
        var ctx = this;

        ctx.Hub = $.connection.loggingHub;
        ctx.Hub.client.LogMessage = function (message) {
            $(".container").prepend("<div class='record'><div class='col-md-1'>" + message.LogLevel + "</div><div class='col-md-3'>" + message.ApplicationName + "</div><div class='col md-8 " + message.LogLevel.toLowerCase() + "'>" + message.LoggerMessage + "</div></div>");
            $('.container .record').slice(100).remove();
        }

        $.connection.hub.start().done();
    }
};

$(function () {
    sc.loggingportal.SignalR.init();
});