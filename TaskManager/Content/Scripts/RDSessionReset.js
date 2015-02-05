function resetDesktopSession(resetActionurl, resetFailureMessage) {
    $.ajax(
        {
            url: resetActionurl,
            type: "POST",
            success: function (result, status) {
                if (result == "SUCCESS") {
                    $('#dialog').attr("title", "Success");
                    $('#dialog').html("The remote session has been reset successfully.");
                    $('#dialog').dialog();
                } else {
                    $('#dialog').attr("title", "Reset Desktop");
                    $('#dialog').html(resetFailureMessage);
                    $('#dialog').dialog();
                }
            },
            error: function (error) {
                $('#dialog').attr("title", "Error");
                $('#dialog').html(resetFailureMessage);
                $('#dialog').dialog();
            }
        }
    );
}