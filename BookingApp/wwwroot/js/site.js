// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$(function () {
//    $("#loaderbody").addClass('hide');

//    $(document).bind('ajaxStart', function () {
//        $("#loaderbody").removeClass('hide');
//    }).bind('ajaxStop', function () {
//        $("#loaderbody").addClass('hide');
//    });
//});

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');

            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    $.notify('submitted successfully', "success");
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
       
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this reservation ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                    $.notify('deleted successfully', "success");

                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    return false;
}

jQueryAjaxGetForDate = form => {
    
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                $('#view-all').html(res.html);
                $.notify('Wyszukano', "success");

            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    
    return false;
}

jQuery(function ($) {

    $(document.body).on('change', '#Reservation_CheckIn', function () {
        $("#Reservation_CheckOut").prop('min', $("#Reservation_CheckIn").val());

        if ($("#Reservation_CheckOut").val() < $("#Reservation_CheckIn").val()) {
            $("#Reservation_CheckOut").val($("#Reservation_CheckIn").val());
        }
    });
   
    $(document.body).on('change', '.dateChange', function () {

        var id = $("#ObjectForRentId").val();
        var people = $("#Reservation_People").val();
        var fromDate = $("#Reservation_CheckIn").val();
        var untilDate = $("#Reservation_CheckOut").val();

        if (people > 0) {
            jQueryAjaxCalculatePrice(id, people, fromDate, untilDate);      

        } else {
            $("#Reservation_ReservationDeposit").val(0);
            $("#Reservation_ReservationPrice").val(0);
        }
        
    });
});

jQueryAjaxCalculatePrice = (id, people, fromDate, untilDate) => {

    try {
        $.ajax({
            type: "POST",
            url: "/Reservations/CalculatePrice", 
            data: {
                objectId: id,
                numberPeople: people,
                checkIn: fromDate,
                checkOut: untilDate
            },
            dataType: false,
            success: function (res) {

                if (res.message) {
                    $.notify(res.message, "error");
                }
                
                $("#Reservation_ReservationDeposit").val(res.reservationDeposit);
                $("#Reservation_ReservationPrice").val(res.reservationPrice);

            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }

    return false;
}



//$(document.body).on('change', '#ObjectForRentId', function () {
//    alert('Change Happened');
//});

