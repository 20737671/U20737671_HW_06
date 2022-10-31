$(document).ready(function () {
    loadData();
});
function loadData() {
    $.ajax({
        url: "/products/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.product_name + '</td>';
                html += '<td>' + item.model_year + '</td>';
                html += '<td>' + item.list_price + '</td>';
                html += '<td>' + item.brand + '</td>';
                html += '<td>' + item.category + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.product_id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.product_id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        
        Name: $('#Name').val(),
        Age: $('#Year').val(),
        State: $('#Price').val(),
        Country: $('#Brand').val(),
        Category: $('#Category').val()

    };
    $.ajax({
        url: "/products/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal1').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getbyID(Id) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Year').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
    $('#Brand').css('border-color', 'lightgrey');
    $('#Category').css('border-color', 'lightgrey');

    $.ajax({
        url: "/products/getbyID/" + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Name').val(result.Name);
            $('#Year').val(result.model_year);
            $('#Price').val(result.list_price);
            $('#Brand').val(result.brand);
            $('#Category').val(result.category);
            $('#myModal1').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        
        Name: $('#Name').val(),
        Year: $('#Year').val(),
        Price: $('#Price').val(),
        Brand: $('#Brand').val(),
        Category: $('#Category').val(),

    };
    $.ajax({
        url: "/products/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal1').modal('hide');
           
            $('#Name').val("");
            $('#Year').val("");
            $('#Price').val("");
            $('#Brand').val("");
            $('#Category').val("");

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/products/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function clearTextBox() {
    $('#Name').val("");
    $('#Year').val("");
    $('#Price').val("");
    $('#Brand').val("");
    $('#Category').val("");

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Year').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
    $('#Brand').css('border-color', 'lightgrey');
    $('#Category').css('border-color', 'lightgrey');

}
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Year').val().trim() == "") {
        $('#Year').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Year').css('border-color', 'lightgrey');
    }
    if ($('#Price').val().trim() == "") {
        $('#Price').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Price').css('border-color', 'lightgrey');
    }
    if ($('#Brand').val().trim() == "") {
        $('#Brand').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Brand').css('border-color', 'lightgrey');
    }
    if ($('#Category').val().trim() == "") {
        $('#Category').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Category').css('border-color', 'lightgrey');
    }
    return isValid;
}