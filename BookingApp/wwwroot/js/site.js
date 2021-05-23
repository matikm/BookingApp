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

jQueryAjaxPostModal = form => {
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
                    $.notify(res.message, res.style);
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                $.notify("Nie powiodło się", "error");
                console.log(err)
            }
        })
       
        return false;
    } catch (ex) {
        console.log(ex)
    }
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
                $('#view-all').html(res.html);
                $.notify(res.message, res.style);
                clearCustpmerFields();
                clearObiectFields();
            },
            error: function (err) {
                $.notify('Nie powiodło się', "error");
                console.log(err)
            }
        })

        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = form => {
    if (confirm('Jesteś pewny/pewna, że chcesz usunąć ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                    $.notify('Usunięto', "success");

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

                if (res.message < 5) 
                    $.notify('Wyszukano: ' + res.message + ' rezerwacje', "success");
                else
                    $.notify('Wyszukano: ' +res.message+ ' rezerwacji' , "success");

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

// ---------- Customers -------------//

$(document).on('click', '.customerEdit', function () {
    var customerId = $.trim($(this).closest('tr').find('.customerId').text());
    var customerFirstName = $.trim($(this).closest('tr').find('.customerFirstName').text());
    var customerLastName = $.trim($(this).closest('tr').find('.customerLastName').text());
    var customerTelephone = $.trim($(this).closest('tr').find('.customerTelephone').text());
    var customerEmail = $.trim($(this).closest('tr').find('.customerEmail').text());

    $("#Customer_CustomerId").val(customerId);
    $("#Customer_FirstName").val(customerFirstName);
    $("#Customer_LastName").val(customerLastName);
    $("#Customer_Telephone").val(customerTelephone);
    $("#Customer_Email").val(customerEmail);

    $('.btn-edit').removeClass('d-none');
    $('#addSubmit').addClass('d-none');
});

$("#customerClearBtn").click(function () {
    clearCustpmerFields();
});


function clearCustpmerFields() {
    $("#Customer_CustomerId").val('');
    $("#Customer_FirstName").val('');
    $("#Customer_LastName").val('');
    $("#Customer_Telephone").val('');
    $("#Customer_Email").val('');

    $('#addSubmit').removeClass('d-none');
    $('.btn-edit').addClass('d-none');
}

// ---------- Objects -------------//

$(document).on('click', '.objectEdit', function () {
    var objectForRentId = $.trim($(this).closest('tr').find('.objectForRentId').text());
    var objectForRentName = $.trim($(this).closest('tr').find('.objectForRentName').text());
    var objectForRentDescription = $.trim($(this).closest('tr').find('.objectForRentDescription').text());
    var objectForRentPhoto = $.trim($(this).closest('tr').find('.objectForRentPhoto').text());

    $("#ObjectForRent_ObjectForRentId").val(objectForRentId);
    $("#ObjectForRent_Name").val(objectForRentName);
    $("#ObjectForRent_Description").val(objectForRentDescription);
    $("#ObjectForRent_Photo").val(objectForRentPhoto);

    $('.btn-edit').removeClass('d-none');
    $('#addSubmit').addClass('d-none');
});

$("#objectClearBtn").click(function () {
    clearObiectFields();
});

function clearObiectFields() {
    $("#ObjectForRent_ObjectForRentId").val('');
    $("#ObjectForRent_Name").val('');
    $("#ObjectForRent_Description").val('');
    $("#ObjectForRent_Photo").val('');

    $('#addSubmit').removeClass('d-none');
    $('.btn-edit').addClass('d-none');
}

// ---------- PriceList -------------//

$(document).on('click', '.pricePerPeopleEdit', function () {
    var pricePerPeopleId = $.trim($(this).closest('tr').find('.pricePerPeopleId').text());
    var pricePerPeoplePeople = $.trim($(this).closest('tr').find('.pricePerPeoplePeople').text());
    var pricePerPeoplePrice = $.trim($(this).closest('tr').find('.pricePerPeoplePrice').text());

    $("#PricePerPeople_PricePerPeopleId").val(pricePerPeopleId);
    $("#PricePerPeople_People").val(pricePerPeoplePeople);
    $("#PricePerPeople_Price").val(pricePerPeoplePrice);

    $('.btn-edit').removeClass('d-none');
    $('#addSubmit').addClass('d-none');
});

$("#priceListClearBtn").click(function () {
    clearPriceListFields();
});

function clearPriceListFields() {
    $("#PricePerPeople_PricePerPeopleId").val('');
    $("#PricePerPeople_People").val('');
    $("#PricePerPeople_Price").val('');

    $('#addSubmit').removeClass('d-none');
    $('.btn-edit').addClass('d-none');
}