function CreateNotify(type, title, content) {
    /*
    type = [alert-danger, alert-warning, alert-success]
    */
    $("#thongbao").remove();
    var options = {};
    options = { percent: 100 };
    str = "<div id='thongbao' class='col-sm-9 col-md-9' style='position: fixed; bottom: 0px;margin:0px;padding:0px;'>";
    str+= "<div class='alert alert-dismissible " + type + "' role='alert'  >";
    str += "<button type='button' class='close' data-dismiss='alert'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button>";
    str += "<strong>" + title + "</strong> " + content + "</div>";
    str += "</div>";
    $("#mainContent").append(str);
    $("#thongbao").show("clip", options, 200, null).delay(3000,null).hide(3000);
}