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

$.fn.serializeObject = function () {
    var obj = {};
    var values = this.serializeArray();

    $.each(values, function () {
        if (obj[this.name]) {
            if (!obj[this.name].push) {
                obj[this.name] = [obj[this.name]];
            }
            obj[this.name].push(this.value || '');
        } else {
            obj[this.name] = this.value || '';
        }
    });
    return obj;
};

$.fn.ajaxifyForm = function () {
    this.validate({
        submitHandler: function (form) {
            $.valcon.clearErrors();
            $.valcon.showLoadingDialog();

            var $form = $(form);
            $.ajax({
                url: $form.attr('action'),
                dataType: 'json',
                type: 'post',
                data: { Body: JSON.stringify($form.serializeObject()) },
                success: function (response) {
                    $.valcon.jsonResponseHandler(response);
                }
            });
            $.valcon.closeLoadingDialog();
        }
    });
};