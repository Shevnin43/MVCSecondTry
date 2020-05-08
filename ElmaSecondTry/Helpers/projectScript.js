function ShowMessage(ClientMessageUrl) {
    $.ajaxSetup({ cache: false });
    $.ajax({
    type: "POST",
    url: ClientMessageUrl,
    data: {},
        success: function (data) {
            $('#dialogUser').html(data);
            $('#modUser').modal({
                keyboard: true
            }, 'show');
            return true;
        }
  });
}

