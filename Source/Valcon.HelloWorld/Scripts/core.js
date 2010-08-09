$(document).ready(function () {

    function init() {
        // Message Box 
        $('#MessageBox').dialog({
            bgiframe: true,
            autoOpen: false,
            height: 240,
            width: 320,
            modal: true,
            buttons: {
                OK: function () {
                    $(this).dialog('close');
                }
            }
        });

        // Confirmation Dialog
        $('#Confirmation').dialog({
            bgiframe: true,
            autoOpen: false,
            height: 240,
            width: 320,
            modal: true
        });

        $('#Loading').dialog({
            modal: true,
            bgiframe: true,
            autoOpen: false,
            width: 150,
            height: 30,
            resizable: false,
            draggable: false
        });

        $.ajaxSetup({
            cache: false
        });

        $('#Loading')
            .siblings(".ui-dialog-titlebar")
            .remove();
    }

    init();
});
$.extend({
    valcon: {
        messageBox: function (title, message) {
            var messageBox = $('#MessageBox');
            messageBox.dialog('option', 'title', title);
            messageBox.html(message);
            messageBox.dialog('open');
        },
        showLoadingDialog: function () {
            $('#Loading').dialog('open');
        },
        closeLoadingDialog: function () {
            $('#Loading').dialog('close');
        },
        jsonResponseHandler: function (response) {
            if (response.Success) {
                $('.success-container > .notice > p > span.message').html(response.Message);
                $('.success-container').fadeIn('slow', function () {
                    setTimeout(function () { $('.success-container').fadeOut(); }, 6000);
                });
            }
            else {
                // TODO -- should make an unordered list of errors
                $('.error-container > .notice > p > span.message').html(response.Errors[0]);
                $('.error-container').fadeIn('slow', function () {
                    // NOTE -- Errors should probably stay visible
                    //setTimeout(function () { $('.error').fadeOut(); }, 6000);
                });
            }
        },
        clearErrors: function () {
            $('.error-container').hide();
        }
    }
});