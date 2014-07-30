function CreateNotify(type, title, content) {
    /*
    type = [alert-danger, alert-warning, alert-success]
    */
    $("#thongbao").remove();
    var options = {};
    options = { percent: 100 };
    str = "<div id='thongbao' style='position: fixed; bottom: 0px'>";
    str += "<div class='alert alert-dismissible " + type + "' role='alert'>";
    str += "<button type='button' class='close' data-dismiss='alert'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button>";
    str += "<strong>" + title + "</strong> " + content + "</div></div>";
    $("#mainContent").append(str);
    $("#thongbao").show("clip", options, 500, callbackShowNotify);
}
function callbackShowNotify() {
    setTimeout(function () {
        $("#thongbao:visible").fadeOut().remove();
    }, 10000);
}