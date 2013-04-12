$(document).ready(function () {
    $.ajax({
        url:  window.location.pathname+'/Home/RegexTree',
        data: { composedRegex: $("#codeTextArea").val() },
        type: 'POST',
        success: function (data) {
            //alert(data);
        }
    });
});