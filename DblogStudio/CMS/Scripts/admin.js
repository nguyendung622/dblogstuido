function GetListRole() {
    $.post("/Admin/GetListRole", { null: null },
                                       function (data) {
                                           $("#roleTable").html(data);
                                       });
}