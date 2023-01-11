﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

 

// Write your Javascript code.


function ClearAll() {
    $("#error").addClass('hidden');
    $("#success").addClass('hidden');
    $("#processor").addClass("hidden");
}

function Logout() {
    localStorage.clear();
    location.href = "/account";
}

var addagents = function () {

    $("#error").addClass('hidden');
    $("#success").addClass('hidden');

    if (document.getElementById("firstname").value == "") {
        $("#msg").html("Please enter first name");
        $("#error").removeClass('hidden');
        $("#firstname").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    if (document.getElementById("lastname").value == "") {
        $("#msg").html("Please enter last name");
        $("#error").removeClass('hidden');
        $("#lastname").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    if (document.getElementById("login_name").value == "") {
        $("#msg").html("Please enter the login name");
        $("#error").removeClass('hidden');
        $("#login_name").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    if (document.getElementById("password").value == "") {
        $("#msg").html("Please enter the password");
        $("#error").removeClass('hidden');
        $("#password").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    if (document.getElementById("repassword").value == "") {
        $("#msg").html("Please enter re-enter the password");
        $("#error").removeClass('hidden');
        $("#repassword").focus();
        setInterval(function () { ClearAll(); }, 10000);
    }
    if (document.getElementById("password").value != document.getElementById("repassword").value) {
        $("#msg").html("The Password and Confirm Password does not match");
        $("#error").removeClass('hidden');
        $("#repassword").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    if (document.getElementById("email").value == "") {
        $("#msg").html("Please enter email address");
        $("#error").removeClass('hidden');
        $("#phone").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    if (!(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(document.getElementById("email").value))) {
        $("#msg").html("Please enter a valid email address");
        $("#error").removeClass('hidden');
        $("#phone").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

     


    $("#processor").removeClass("hidden");
    $("#btnRegisterUser").addClass("hidden");

    var formdata = {
        "FirstName": document.getElementById("firstname").value,
        "LastName": document.getElementById("lastname").value,
        "LoginName": document.getElementById("login_name").value,
        "Password": document.getElementById("password").value,
        "UserType": document.getElementById("usertype").value,
        "Phone": document.getElementById("email").value,

    };

    $("#btnRegisterUser").prop("disabled", true);
    $.ajax({
        type: "POST",
        url: "/Account/AddUser",
        data: formdata,
        success: function (data) {
            setInterval(function () { ClearAll(); }, 10000);
            if (!data) {
                $("#msg").html("User name already in use. Please try again");
                $("#error").removeClass('hidden');
                $("#processor").addClass("hidden");
                $("#btnRegisterUser").removeClass("hidden");
                $("#btnRegisterUser").prop("disabled", false);
            } else {

                $("#msgs").html("User successfully registered!");
                $("#success").removeClass('hidden');
                $("#processor").addClass("hidden");
                $("#btnRegisterUser").removeClass("hidden");
                $("#btnRegisterUser").prop("disabled", false);
                //location.href = "/admin";
            }
        },
        error: function (req, status, error) {
            setInterval(function () { ClearAll(); }, 10000);
            $("#msg").html("User name may already be in use. Please try again");
            $("#error").removeClass('hidden');
            $("#success").addClass('hidden');
            $("#processor").addClass("hidden");
            $("#btnRegisterUser").removeClass("hidden");
            $("#btnRegisterUser").prop("disabled", false);
        }
    });
}

var Login = function () {
    if (document.getElementById("login_name").value == "") {
        $("#msg").html("Please enter the phone number");
        $("#error").removeClass('hidden');
        $("#login_name").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    if (document.getElementById("password").value == "") {
        $("#msg").html("Please enter the password");
        $("#error").removeClass('hidden');
        $("#password").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    $("#processor").removeClass("hidden");
    $("#btnLoginUser").addClass("hidden");
    var _data = {
        Username: document.getElementById("login_name").value,
        Password: document.getElementById("password").value
    };

    $("#btnLoginUser").prop("disabled", true);
    $.ajax({
        type: "GET",
        url: "/Account/login",
        data: _data,
        success: function (data) {
            console.log(data);

            data.forEach(item => {

                if (item.username == "No Login") {
                    $("#msg").html("Invalid username or password. Please try again");
                    $("#error").removeClass('hidden');
                    $("#processor").addClass("hidden");
                    $("#btnLoginUser").removeClass("hidden");
                    $("#btnLoginUser").prop("disabled", false);
                    setInterval(function () { ClearAll(); }, 10000);
                } else {
                    if (item.userType == "super") {
                        localStorage.setItem("super", item.usertype);
                        localStorage.setItem("username", item.username);
                        localStorage.setItem("phone", item.username);
                        localStorage.setItem("procby", item.recoveryCode);

                        location.href = "/";
                    } else {
                        if (item.userType == "admin") {
                            localStorage.setItem("admin", item.usertype);
                            localStorage.setItem("username", item.username);
                            localStorage.setItem("phone", item.username);
                            localStorage.setItem("procby", item.recoveryCode);
                            location.href = "/";
                        } else {
                            $("#msg").html("invalid username or password. please try again");
                            $("#error").removeClass('hidden');
                            $("#processor").addClass("hidden");
                            $("#btnloginuser").removeClass("hidden");
                            $("#btnloginuser").prop("disabled", false);
                        }
                    }
                }
            });

        },
        error: function (req, status, error) {
            setInterval(function () { ClearAll(); }, 10000);
            $("#msg").html("Invalid username or password. Please try again");
            $("#error").removeClass('hidden');
            $("#processor").addClass("hidden");
            $("#btnLoginUser").removeClass("hidden");
            $("#btnLoginUser").prop("disabled", false);
            setInterval(function () { ClearAll(); }, 10000);
        }
    });
}

var logout = function () {
    localStorage.clear();
    location.href = "/";
}

if ($("#inprogress").length > 0) {

    $("#processor").removeClass("hidden");
    $.ajax({
        type: "GET",
        url: "/Home/GetTransaction",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (data) {
            console.log(data);
            if (!Object.keys(data).length) {
                $("#processor").addClass("hidden");
            } else {
                doPrint(data);
                var htm = "";
                var i = 1;
                data.forEach(item => {
                    if ((item.processed == false || item.processed == true) && item.completed == false && item.deleted == false) {
                        var buyacctnames = "-";
                        if (item.buyerAccountName != null) { buyacctnames = item.buyerAccountName; }
                        var buybanknames = "-";
                        if (item.buyerBankName != null) { buybanknames = item.buyerBankName; }
                        var buybanknos = "-";
                        if (item.buyerAccountNumber != null) { buybanknos = item.buyerAccountNumber; }
                        htm += "<tr>"
                        htm += "<td class='text-center text-muted'>" + i + "</td>"
                        htm += "<td class='text-center>"
                        htm += "<div class='dropdown d-inline-block'>"
                        htm += "<button type='button' aria-haspopup='true' aria-expanded='false' data-toggle='dropdown' class='mb-2 mr-2 dropdown-toggle' > " + item.transactionCode + "</button ></div>"
                        htm += "<div tabindex='-1' role='menu' aria-hidden='true' class='dropdown-menu'>"
                        htm += "<button type='button' tabindex= '0' class='dropdown-item'> seller Mobile Number: " + item.sellerMobileNumber + "</button>"
                        htm += "<button type='button' tabindex= '0' class='dropdown-item'> buyer Mobile Number: " + item.buyerMobileNumber + "</button>"
                        htm += "<button type='button' tabindex= '0' class='dropdown-item'> Agreed Price: " + formatMoney(parseFloat(item.agreedPrice)) + "</button>"
                        htm += "<button type='button' tabindex='0' class='dropdown-item'>Shipping Cost: " + formatMoney(parseFloat(item.shippingCost)) + "</button>"
                        htm += "<button type='button' tabindex='0' class='dropdown-item'>Commision: " + formatMoney(parseFloat(item.ahhtCommission)) + "</button>"
                        htm += "<h6 tabindex='-1' class='dropdown-header'>Total Cost: " + formatMoney(parseFloat(item.totalCost)) + "</h6>"
                        htm += "</div>"
                        htm += "</td > "
                        htm += "<td class='text-center'>" + item.transactionDescription + "</td>"
                        htm += "<td class='text-center'>" + item.transactionDate + "</td>"
                        htm += "<td class='text-center'>" + item.paymentDate + "</td>"
                        htm += "<td class='text-center'>Name: " + item.sellerAccountName + "<br/>Bank: " + item.sellerBankName + "<br /> A/C No.: " + item.sellerAccountNumber + "</td>"

                        htm += "<td class='text-center'>Name: " + buyacctnames + "<br/>Bank: " + buybanknames + "<br /> A/C No.: " + buybanknos + "</td>"

                        htm += "<td class='text-center " + item.transactionStatus + " '>" + item.reasons + "</td>"
                        if (item.processed == true) {

                            if (item.reasons.includes('Buyer declined the transaction')) {
                                htm += "<td class='text-center'>Transaction declined</td>"
                            } else {
                                if (item.reasons.includes('returned')) {
                                    htm += "<td class='text-center'>Item returned (Seller & Buyer paid)</td>"
                                } else {
                                    if (item.reasons.startsWith("Completed")) {
                                        htm += "<td class='text-center'>Item collected (Seller paid)</td>"
                                    }
                                }
                            }
                        } else {
                            htm += "<td class='text-center'> - </td>"
                        }
                        // htm += "<td class='text-center'>" + formatMoney(parseFloat(item.totalCost)) + "</td>"
                        if (item.reasons.startsWith("Completed")) {

                            var cost = item.agreedPrice;
                            var ship = parseInt(item.shippingCost);

                            var total = parseInt(cost) + parseInt(ship);
                            var _total = formatMoney(parseFloat(total));

                            htm += "<td class='text-center'>"
                            htm += "     Seller: " + _total;
                            htm += "</td>"
                        } else {
                            if (item.reasons.startsWith("Declined") && item.paid == true) {

                                var cost = item.totalCost;
                                var commission = item.ahhtCommission;

                                var ship = parseInt((0.5 * item.shippingCost)) + parseInt(item.shippingCost);
                                var _ship = formatMoney(parseFloat(ship));

                                var total = parseInt(cost) - parseInt(commission) - parseInt(ship);
                                var _total = formatMoney(parseFloat(total));

                                htm += "<td class='text-center'>"
                                htm += "    Buyer: " + _total + " <br/> Seller: " + _ship
                                htm += "</td>"
                            } else {
                                if (item.reasons.startsWith("Pending") && (item.paid == false || item.paid == true)) {
                                    htm += "<td class='text-center'>"
                                    htm += " - "
                                    htm += "</td>"
                                } else {
                                    if (item.reasons.includes("declined") && item.paid == false) {
                                        htm += "<td class='text-center'>"
                                        htm += " - "
                                        htm += "</td>"
                                    }
                                }
                            }
                        }
                        var refund = item.refundDate == null ? "-" : item.refundDate
                        htm += "<td class='text-center'> " + refund + " </td>"
                        var procby = item.processedBy == null ? "-" : item.processedBy
                        htm += "<td class='text-center'> " + procby + " </td>"
                        if (item.reasons.startsWith("Pending") && item.processed == false && item.paid == false && AddDate(item.transactionDate, "1", "M") == true) {
                            htm += "<td class='text-center'><button onclick='javascript:Delete(" + item.id + ");' class='btn-sm btn btn-danger'></button></td>"
                        }
                        else {
                            if (item.reasons.startsWith("Pending")) {
                                htm += "<td class='text-center'> - </td>"
                            } else {
                                if (item.processed == true) {
                                    if (item.completed == false) {
                                        htm += "<td class='text-center'><button onclick='javascript:Completed(" + item.id + ");' class='btn-sm btn btn-success'></button></td>"
                                    }
                                } else {
                                    htm += "<td class='text-center'><button onclick='javascript:Processed(" + item.id + ");' class='btn-sm btn btn-warning'></button></td>"
                                }
                            }
                        }
                        htm += "</tr> "
                        i++;
                    }
                });
                $("#tbody").append(htm);
                $("#processor").addClass("hidden");
            }
        },
        error: function (req, status, error) {
            $("#processor").addClass("hidden");
            $("#btnAddVehicle").removeClass("hidden");
            $("#btnAddVehicle").prop("disabled", false);
        }
    });
}

function formatMoney(number, decPlaces, decSep, thouSep) {
    return number.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
}

function Processed(id) {
    var z = confirm("Are you sure you want to PROCESS this transaction?");
    if (z == true) {

        $.ajax({

            type: "get",
            url: "/home/process/",
            data: { id: id, processby: localStorage.getItem("procby") },
            contenttype: "application/json; charset=utf-8",
            datatype: "json",
            success: function (data) {
                location.reload();
            },
            error: function (req, status, error) {
                location.reload();
            }
        });
    }
}

function Completed(id) {
    var z = confirm("Are you sure you want to mark this transaction as COMPLETED?");
    if (z == true) {
        $.ajax({

            type: "GET",
            url: "/Home/Completed/",
            data: { Id: id, processby: localStorage.getItem("procby") },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                location.reload();
            },
            error: function (req, status, error) {
                location.reload();
            }
        });
    }
}
function Delete(id) {
    var z = confirm("Are you sure you want to DELETE this transaction?");
    if (z == true) {
        $.ajax({

            type: "GET",
            url: "/Home/Delete/",
            data: { Id: id, processby: localStorage.getItem("procby") },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                location.reload();
            },
            error: function (req, status, error) {
                location.reload();
            }
        });
    }
}
var i = 0;
function AddDate(oldDateoff, offset, offsetType) {

    var db_date = new Date(oldDateoff);
    var full_year = db_date.getFullYear();
    var time_stamp = db_date.getHours() + ":" + db_date.getMinutes() + ":" + db_date.getSeconds();

    if (db_date.getMonth() + 2 > 12) {

        var new_year = db_date.getFullYear() + 1;

        var new_date = new Date("Jan " + db_date.getDate() + ", " + new_year + " " + time_stamp);

        var today = new Date();

        return today > new_date ? true : false;

    } else {

        var new_month = db_date.getMonth() + 2;

        var new_date = new Date(new_month + " " + db_date.getDate() + ", " + full_year + " " + time_stamp);

        var today = new Date();

        return today > new_date ? true : false;
    }
}

Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}

//var date = new Date();
//console.log(date.addDays(5));

if ($("#complete").length > 0) {

    $("#processor").removeClass("hidden");
    $.ajax({
        type: "GET",
        url: "/Home/GetTransaction/",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (data) {
            console.log(data);
            if (!Object.keys(data).length) {
                $("#processor").addClass("hidden");
            } else {
                var htm = "";
                var i = 1;
                data.forEach(item => {
                    if (item.processed == true && item.completed == true && item.reasons.includes('collected')) {
                        htm += "<tr>"
                        htm += "<td class='text-center text-muted'>" + i + "</td>"
                        htm += "<td class='text-center>"
                        htm += "<div class='dropdown d-inline-block'>"
                        htm += "<button type='button' aria-haspopup='true' aria-expanded='false' data-toggle='dropdown' class='mb-2 mr-2 dropdown-toggle' > " + item.transactionCode + "</button ></div>"
                        htm += "<div tabindex='-1' role='menu' aria-hidden='true' class='dropdown-menu'>"
                        htm += "<button type='button' tabindex= '0' class='dropdown-item'> seller Mobile Number: " + item.sellerMobileNumber + "</button>"
                        htm += "<button type='button' tabindex= '0' class='dropdown-item'> buyer Mobile Number: " + item.buyerMobileNumber + "</button>"
                        htm += "<button type='button' tabindex= '0' class='dropdown-item'> Agreed Price: " + formatMoney(parseFloat(item.agreedPrice)) + "</button>"
                        htm += "<button type='button' tabindex='0' class='dropdown-item'>Shipping Cost: " + formatMoney(parseFloat(item.shippingCost)) + "</button>"
                        htm += "<button type='button' tabindex='0' class='dropdown-item'>Commision: " + formatMoney(parseFloat(item.ahhtCommission)) + "</button>"
                        htm += "<h6 tabindex='-1' class='dropdown-header'>Total Cost: " + formatMoney(parseFloat(item.totalCost)) + "</h6>"
                        htm += "</div>"
                        htm += "</td > "
                        htm += "<td class='text-center'>" + item.transactionDescription + "</td>"
                        htm += "<td class='text-center'>" + item.transactionDate + "</td>"
                        htm += "<td class='text-center'>" + item.paymentDate + "</td>"

                        htm += "<td class='text-center'>Name: " + item.sellerAccountName + "<br/>Bank: " + item.sellerBankName + "<br /> A/C No.: " + item.sellerAccountNumber + "</td>"

                        htm += "<td class='text-center'>Name: " + item.buyerAccountName + "<br/>Bank: " + item.buyerBankName + "<br /> A/C No.: " + item.buyerAccountNumber + "</td>"
                        htm += "<td class='text-center " + item.transactionStatus + " '>" + item.reasons + "</td>"

                        htm += "<td class='text-center'>Item/good collected (Seller paid)</td>"
                        // htm += "<td class='text-center'>" + formatMoney(parseFloat(item.totalCost)) + "</td>"
                        if (item.reasons.startsWith("Completed")) {
                            var cost = item.agreedPrice;
                            var ship = parseInt(item.shippingCost);

                            var total = parseInt(cost) + parseInt(ship);
                            var _total = formatMoney(parseFloat(total));

                            htm += "<td class='text-center'>"
                            htm += "     Seller: " + _total;
                            htm += "</td>"
                        } else {
                            if (item.reasons.startsWith("Declined") && item.paid == true) {
                                var cost = item.totalCost;
                                var commission = item.ahhtCommission;

                                var ship = parseInt((0.5 * item.shippingCost)) + parseInt(item.shippingCost);
                                var _ship = formatMoney(parseFloat(ship));

                                var total = parseInt(cost) - parseInt(commission) - parseInt(ship);
                                var _total = formatMoney(parseFloat(total));

                                htm += "<td class='text-center'>"
                                htm += "    Buyer: " + _total + " <br/> Seller: " + _ship
                                htm += "</td>"
                            } else {
                                if (item.reasons.startsWith("Pending") && item.paid == false) {
                                    htm += "<td class='text-center'>"
                                    htm += " - "
                                    htm += "</td>"
                                } else {
                                    if (item.reasons.includes("declined") && item.paid == false) {
                                        htm += "<td class='text-center'>"
                                        htm += " - "
                                        htm += "</td>"
                                    }
                                }
                            }
                        }
                        var refund = item.refundDate == null ? "-" : item.refundDate
                        htm += "<td class='text-center'> " + refund + " </td>"
                        var procby = item.processedBy == null ? "-" : item.processedBy
                        htm += "<td class='text-center'> " + procby + " </td>"
                        htm += "</tr> "
                        i++;
                    }
                });
                $("#tcbody").append(htm);
                $("#processor").addClass("hidden");
                getTotalCash();
            }
        },
        error: function (req, status, error) {
            $("#processor").addClass("hidden");
            $("#btnAddVehicle").removeClass("hidden");
            $("#btnAddVehicle").prop("disabled", false);
        }
    });

    
}

var getTotalCash = () => {

    
    $.ajax({
        type: "GET",
        url: "/Home/GetTotalCash/",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (data) {
            $("#price").html(formatMoney(parseFloat(data)));
        }
    });
}

if ($("#returned").length > 0) {

    $("#processor").removeClass("hidden");
    $.ajax({
        type: "GET",
        url: "/Home/GetTransaction/",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (data) {
            console.log(data);
            if (!Object.keys(data).length) {
                $("#processor").addClass("hidden");
            } else {
                var htm = "";
                var i = 1;
                data.forEach(item => {
                    if (item.processed == true && item.paid == true && item.reasons.includes('returned')) {
                        htm += "<tr>"
                        htm += "<td class='text-center text-muted'>" + i + "</td>"
                        htm += "<td class='text-center>"
                        htm += "<div class='dropdown d-inline-block'>"
                        htm += "<button type='button' aria-haspopup='true' aria-expanded='false' data-toggle='dropdown' class='mb-2 mr-2 dropdown-toggle' > " + item.transactionCode + "</button ></div>"
                        htm += "<div tabindex='-1' role='menu' aria-hidden='true' class='dropdown-menu'>"
                        htm += "<button type='button' tabindex= '0' class='dropdown-item'> seller Mobile Number: " + item.sellerMobileNumber + "</button>"
                        htm += "<button type='button' tabindex= '0' class='dropdown-item'> buyer Mobile Number: " + item.buyerMobileNumber + "</button>"
                        htm += "<button type='button' tabindex= '0' class='dropdown-item'> Agreed Price: " + formatMoney(parseFloat(item.agreedPrice)) + "</button>"
                        htm += "<button type='button' tabindex='0' class='dropdown-item'>Shipping Cost: " + formatMoney(parseFloat(item.shippingCost)) + "</button>"
                        htm += "<button type='button' tabindex='0' class='dropdown-item'>Commision: " + formatMoney(parseFloat(item.ahhtCommission)) + "</button>"
                        htm += "<h6 tabindex='-1' class='dropdown-header'>Total Cost: " + formatMoney(parseFloat(item.totalCost)) + "</h6>"
                        htm += "</div>"
                        htm += "</td > "
                        htm += "<td class='text-center'>" + item.transactionDescription + "</td>"
                        htm += "<td class='text-center'>" + item.transactionDate + "</td>"
                        htm += "<td class='text-center'>" + item.paymentDate + "</td>"
                        htm += "<td class='text-center'>Name: " + item.sellerAccountName + "<br/>Bank: " + item.sellerBankName + "<br /> A/C No.: " + item.sellerAccountNumber + "</td>"
                        //htm += "<td class='text-center'>" + item.buyerMobileNumber + "</td>"
                        htm += "<td class='text-center'>Name: " + item.buyerAccountName + "<br/>Bank: " + item.buyerBankName + "<br /> A/C No.: " + item.buyerAccountNumber + "</td>"
                        htm += "<td class='text-center " + item.transactionStatus + " '>" + item.reasons + "</td>"

                        htm += "<td class='text-center'>Item returned (Seller & Buyer paid)</td>"
                        //htm += "<td class='text-center'>" + formatMoney(parseFloat(item.totalCost)) + "</td>"
                        if (item.reasons.startsWith("Completed")) {
                            var cost = item.agreedPrice;
                            var ship = parseInt(item.shippingCost);

                            var total = parseInt(cost) + parseInt(ship);
                            var _total = formatMoney(parseFloat(total));

                            htm += "<td class='text-center'>"
                            htm += "     Seller: " + _total;
                            htm += "</td>"
                        } else {
                            if (item.reasons.startsWith("Declined") && item.paid == true) {
                                var cost = item.totalCost;
                                var commission = item.ahhtCommission;

                                var ship = parseInt((0.5 * item.shippingCost)) + parseInt(item.shippingCost);
                                var _ship = formatMoney(parseFloat(ship));

                                var total = parseInt(cost) - parseInt(commission) - parseInt(ship);
                                var _total = formatMoney(parseFloat(total));

                                htm += "<td class='text-center'>"
                                htm += "    Buyer: " + _total + " <br/> Seller: " + _ship
                                htm += "</td>"
                            } else {
                                if (item.reasons.startsWith("Pending") && item.paid == false) {
                                    htm += "<td class='text-center'>"
                                    htm += " - "
                                    htm += "</td>"
                                } else {
                                    if (item.reasons.includes("declined") && item.paid == false) {
                                        htm += "<td class='text-center'>"
                                        htm += " - "
                                        htm += "</td>"
                                    }
                                }
                            }
                        }
                        var refund = item.refundDate == null ? "-" : item.refundDate
                        htm += "<td class='text-center'> " + refund + " </td>"
                        var procby = item.processedBy == null ? "-" : item.processedBy
                        htm += "<td class='text-center'> " + procby + " </td>"
                        htm += "</tr> "
                        i++;
                    }
                });
                $("#trbody").append(htm);
                $("#processor").addClass("hidden");
            }
        },
        error: function (req, status, error) {
            $("#processor").addClass("hidden");
            $("#btnAddVehicle").removeClass("hidden");
            $("#btnAddVehicle").prop("disabled", false);
        }
    });
}


if ($("#cancelled").length > 0) {

    $("#processor").removeClass("hidden");
    $.ajax({
        type: "GET",
        url: "/Home/GetTransaction/",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (data) {
            console.log(data);
            if (!Object.keys(data).length) {
                $("#processor").addClass("hidden");
            } else {
                var htm = "";
                var i = 1;
                data.forEach(item => {
                    if (item.processed == true && item.reasons.includes('decline')) {
                        htm += "<tr>"
                        htm += "<td class='text-center text-muted'>" + i + "</td>"
                        htm += "<td class='text-center'>" + item.transactionCode + "</td>"
                        htm += "<td class='text-center'>" + item.transactionDescription + "</td>"
                        htm += "<td class='text-center'>" + item.sellerMobileNumber + "</td>"
                        htm += "<td class='text-center'>Name: " + item.sellerAccountName + "<br/>Bank: " + item.sellerBankName + "<br /> A/C No.: " + item.sellerAccountNumber + "</td>"
                        htm += "<td class='text-center'>" + item.buyerMobileNumber + "</td>"
                        htm += "<td class='text-center'>Name: " + item.buyerAccountName + "<br/>Bank: " + item.buyerBankName + "<br /> A/C No.: " + item.buyerAccountNumber + "</td>"
                        htm += "<td class='text-center " + item.transactionStatus + " '>" + item.reasons + "</td>"

                        htm += "<td class='text-center'>Transaction declined</td>"
                        htm += "<td class='text-center'>" + item.processedBy + "</td>"
                        htm += "</tr> "
                        i++;
                    }
                });
                $("#tcanbody").append(htm);
                $("#processor").addClass("hidden");
            }
        },
        error: function (req, status, error) {
            $("#processor").addClass("hidden");
            $("#btnAddVehicle").removeClass("hidden");
            $("#btnAddVehicle").prop("disabled", false);
        }
    });
}

if ($("#deleted").length > 0) {

    $("#processor").removeClass("hidden");
    $.ajax({
        type: "GET",
        url: "/Home/GetTransaction/",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (data) {
            console.log(data);
            if (!Object.keys(data).length) {
                $("#processor").addClass("hidden");
            } else {
                var htm = "";
                var i = 1;
                data.forEach(item => {
                    if (item.deleted == true && item.reasons.includes('yet')) {
                        htm += "<tr>"
                        htm += "<td class='text-center text-muted'>" + i + "</td>"
                        htm += "<td class='text-center'>" + item.transactionCode + "</td>"
                        htm += "<td class='text-center'>" + item.transactionDescription + "</td>"
                        htm += "<td class='text-center'>" + item.transactionDate + "</td>"
                        htm += "<td class='text-center'>" + item.sellerMobileNumber + "</td>"
                        htm += "<td class='text-center'>Name: " + item.sellerAccountName + "<br/>Bank: " + item.sellerBankName + "<br /> A/C No.: " + item.sellerAccountNumber + "</td>"
                        htm += "<td class='text-center'>" + item.buyerMobileNumber + "</td>"
                        htm += "<td class='text-center'>Name: " + item.buyerAccountName + "<br/>Bank: " + item.buyerBankName + "<br /> A/C No.: " + item.buyerAccountNumber + "</td>"
                        htm += "<td class='text-center " + item.transactionStatus + " '>" + item.reasons + "</td>"

                        htm += "<td class='text-center'>Transaction deleted</td>"
                        htm += "<td class='text-center'>" + item.processedBy + "</td>"
                        htm += "</tr> "
                        i++;
                    }
                });
                $("#tcanbody").append(htm);
                $("#processor").addClass("hidden");
            }
        },
        error: function (req, status, error) {
            $("#processor").addClass("hidden");
            $("#btnAddVehicle").removeClass("hidden");
            $("#btnAddVehicle").prop("disabled", false);
        }
    });
}



if ($("#board").length > 0) {

    var agentid = localStorage.getItem("agentid");
    displayAgentClients(agentid);
    $.ajax({
        type: "GET",
        url: "/Users/GetDashBoardInfo/",
        contentType: "application/json; charset=utf-8",
        data: { id: agentid },
        dataType: "json",
        success: function (data) {
            console.log(data);
            data.forEach(item => {
                $("#totalIncome").html(item.accrued);
                $("#totalActive").html(item.activeCustomers);
                $("#totalClients").html(item.customers);
            });
        }
    });
}

function remove(id) {
    $.ajax({
        type: "GET",
        url: "/Users/RemoveUsers/",
        contentType: "application/json; charset=utf-8",
        data: { id: id },
        dataType: "json",
        success: function (data) {
            location.reload();
        },
        error: function (req, status, error) {
            location.reload();
        }
    });
}

function doPrint(data) {
    var htm = "";
    var i = 1;
    data.forEach(item => {

        if (item.processed == false && item.completed == false && item.deleted == false && (item.reasons.includes('returned') || item.reasons.includes("collected"))) {
            var buyacctname = "-";
            if (item.buyerAccountName != null) { buyacctname = item.buyerAccountName; }
            var buybankname = "-";
            if (item.buyerBankName != null) { buybankname = item.buyerBankName; }
            var buybankno = "-";
            if (item.buyerAccountNumber != null) { buybankno = item.buyerAccountNumber; }
            htm += "<tr>"
            htm += "<td class='text-center text-muted'>" + i + "</td>"
            htm += "<td class='text-center'>" + item.transactionCode + "</td>"
            htm += "<td class='text-center'>" + item.transactionDescription + "</td>"
            htm += "<td class='text-center'>" + item.sellerMobileNumber + "</td>"
            htm += "<td class='text-center'>Name: " + item.sellerAccountName + "<br/>Bank: " + item.sellerBankName + "<br /> A/C No.: " + item.sellerAccountNumber + "</td>"
            htm += "<td class='text-center'>" + item.buyerMobileNumber + "</td>"
            htm += "<td class='text-center'>Name: " + buyacctname + "<br/>Bank: " + buybankname + "<br /> A/C No.: " + buybankno + "</td>"
            htm += "<td class='text-center " + item.transactionStatus + " '>" + item.reasons + "</td>"
            htm += "<td class='text-center'>" + formatMoney(parseFloat(item.totalCost)) + "</td>"
            if (item.reasons.startsWith("Completed")) {

                var cost = item.agreedPrice;
                var ship = parseInt(item.shippingCost);

                var total = parseInt(cost) + parseInt(ship);
                var _total = formatMoney(parseFloat(total));

                htm += "<td class='text-center'>"
                htm += "     Seller: " + _total;
                htm += "</td>"
            } else {
                if (item.reasons.startsWith("Declined") && item.paid == true) {

                    var cost = item.totalCost;
                    var commission = item.ahhtCommission;

                    var ship = parseInt((0.5 * item.shippingCost)) + parseInt(item.shippingCost);
                    var _ship = formatMoney(parseFloat(ship));

                    var total = parseInt(cost) - parseInt(commission) - parseInt(ship);
                    var _total = formatMoney(parseFloat(total));

                    htm += "<td class='text-center'>"
                    htm += "    Buyer: " + _total + " <br/> Seller: " + _ship
                    htm += "</td>"
                }
            }

            htm += "</tr> "
            i++;
        }
    });
    $("#tbodyPrint").append(htm);
}

var printPage = function () {
    var contents = $("#dvContents").html();
    var frame1 = $('<iframe />');
    frame1[0].name = "frame1";
    frame1.css({ "position": "absolute", "top": "-1000000px" });
    $("body").append(frame1);
    var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
    frameDoc.document.open();
    //Create a new HTML document.
    frameDoc.document.write('<html><head><title>Brigho Pending Payments</title>');
    frameDoc.document.write('</head><body>');
    //Append the external CSS file.
    frameDoc.document.write('<link href="/main.css" rel="stylesheet">');
    //Append the DIV contents.
    frameDoc.document.write(contents);
    frameDoc.document.write('</body></html>');
    frameDoc.document.close();
    setTimeout(function () {
        window.frames["frame1"].focus();
        window.frames["frame1"].print();
        frame1.remove();
    }, 500);
}



var ReconcilePayment = function () {
    if (document.getElementById("trans_code").value == "") {
        $("#msg").html("Please enter the transaction code");
        $("#error").removeClass('hidden');
        $("#trans_code").focus();
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }


    $("#processor").removeClass("hidden");
    $("#btnReconcilePayment").addClass("hidden");
    var _data = document.getElementById("trans_code").value;



    $.ajax({
        type: "GET",
        url: "/Resolve/ProcessPay?trans=" + _data,
        success: function (data) {
            console.log(data);
            if (data == true) {
                $("#msgs").html("Payment was successfuly processed!");
                $("#success").removeClass('hidden');
                $("#trans_code").text('');
                $("#processor").addClass("hidden");
                $("#btnReconcilePayment").removeClass("hidden");
                setInterval(function () { ClearAll(); }, 10000);
            } else {
                $("#msg").html("Failed to process payment. Try again!");
                $("#error").removeClass('hidden');
                $("#trans_code").focus();
                $("#processor").addClass("hidden");
                $("#btnReconcilePayment").removeClass("hidden");
                setInterval(function () { ClearAll(); }, 10000);
            }

        },
        error: function (req, status, error) {
            setInterval(function () { ClearAll(); }, 10000);
            $("#msg").html("Invalid transaction code. Please try again");
            $("#error").removeClass('hidden');
            $("#processor").addClass("hidden");
            $("#btnReconcilePayment").removeClass("hidden");
            setInterval(function () { ClearAll(); }, 10000);
        }
    });
}


var forgotLogin = function () {

    $("#error").addClass('hidden');
    $("#success").addClass('hidden');

    if (document.getElementById("login_name").value == "") {
        $("#msg").html("please enter your email address");
        $("#error").removeClass('hidden');
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    $("#processor").removeClass("hidden");
    $("#btnForgot").addClass("hidden");
    var _data = {
        Phone: document.getElementById("login_name").value
    };

    $("#btnForgot").prop("disabled", true);

    var emailVal =document.getElementById("login_name").value.trim();
    
    var requestOptions = {
        method: 'GET',
        redirect: 'follow'
    };

     

    fetch("/api/remember/recoverpassword/"+ emailVal , {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        } 
    })
        .then(response => response.json())
        .then(data => {
            if (data != "") {

                SendMail(data);

            } else {

                setInterval(function () { ClearAll(); }, 10000);
                $("#msg").html("Something went wrong. Please try again");
                $("#error").removeClass('hidden');
                $("#processor").addClass("hidden");
                $("#btnForgot").removeClass("hidden");
                $("#btnForgot").prop("disabled", false);

            }
        }).catch(error => {
            setInterval(function () { ClearAll(); }, 10000);
            $("#msg").html("Something went wrong. Please try again.");
            $("#error").removeClass('hidden');
            $("#processor").addClass("hidden");
            $("#btnForgot").removeClass("hidden");
            $("#btnForgot").prop("disabled", false);
        })
     
}


var SendMail = (val) => {
     console.log("Val: "+val)
    $.ajax({
        type: "POST",
        url: "http://mobile.brigho.com/utility/SendAdminCodeMessage/",
        data: {
            'MessageContent': "Your Admin Password Recovery Code: " + val,
            'Title': "Password Recovery",
            'Name': "Admin Dashboard",
            'EmailAddress': $("#login_name").val()
        },
        dataType: "json",
        success: function (data) {
            console.log("returned data: " + data);
            if (!data) {
                console.log("falsy: " + data);
            }
            else {

                $("#msgs").html("Password reset details has been sent to your email");
                $("#success").removeClass('hidden');
                $("#processor").addClass("hidden");
                $("#btnForgot").removeClass("hidden");
                $("#btnForgot").prop("disabled", false);

                location.href = "/account/confirmcode?email=" + $("#login_name").val();
            }
        },
        error: function (err) {
            console.log("error func: " + err);
            setInterval(function () { ClearAll(); }, 10000);
        }
    });

}

var ConfirmCode = function () {

    var _query = location.search.split('=');
    var _email = _query[1];
    var code = document.getElementById("code_name").value;

    if (document.getElementById("code_name").value == "") {
        $("#msg").html("please enter the recovery code sent to your email");
        $("#error").removeClass('hidden');
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }
    
    $("#processor").removeClass("hidden");
    $("#btnForgot").addClass("hidden");
    var _data = {
        Phone: _email,
        RecoveryCode: document.getElementById("code_name").value
    }; 
    $("#btnForgot").prop("disabled", true);
    $.ajax({
        type: "GET",
        url: "/api/remember/confirmation/"+code+"/"+_email,        
        success: function (data) {
            setInterval(function () { ClearAll(); }, 10000);
            if (data == true) {
                location.href = "/account/changepassword?email=" + _email;
            } else {
                $("#msg").html("Invalid Code. Please supply valid Code");
                $("#error").removeClass('hidden');
                $("#processor").addClass("hidden");
                $("#btnForgot").removeClass("hidden");
                $("#btnForgot").prop("disabled", false);
            }
        },
        error: function (req, status, error) {
            setInterval(function () { ClearAll(); }, 10000);
            $("#msg").html("Something went wrong. Please try again");
            $("#error").removeClass('hidden');
            $("#processor").addClass("hidden");
            $("#btnForgot").removeClass("hidden");
            $("#btnForgot").prop("disabled", false);
        }
    });
}


var ChangePassword = function () {
     
    if (document.getElementById("pass").value == "") {
        $("#msg").html("please enter your new password");
        $("#error").removeClass('hidden');
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }
    if (document.getElementById("pass_c").value == "") {
        $("#msg").html("please confirm your new password");
        $("#error").removeClass('hidden');
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }
    var pass = document.getElementById("pass").value;
    var pass_c = document.getElementById("pass_c").value;

    if (pass != pass_c) {
        $("#msg").html("Your new password and it's confirmation do not match");
        $("#error").removeClass('hidden');
        setInterval(function () { ClearAll(); }, 10000);
        return false;
    }

    var _query = location.search.split('=');
    var _email = _query[1];

    $("#processor").removeClass("hidden");
    $("#btnForgot").addClass("hidden");
    
    $("#btnForgot").prop("disabled", true);
    $.ajax({
        type: "POST",
        url: "/api/remember/changepassword/" + pass_c + "/" + _email,       
        success: function (data) {
            setInterval(function () { ClearAll(); }, 10000);
            if (data == true) {
                location.href = "/account";
            } else {
                $("#msg").html("Invalid Credentials. Please supply valid credentials");
                $("#error").removeClass('hidden');
                $("#processor").addClass("hidden");
                $("#btnForgot").removeClass("hidden");
                $("#btnForgot").prop("disabled", false);
            }
        },
        error: function (req, status, error) {
            setInterval(function () { ClearAll(); }, 10000);
            $("#msg").html("Something went wrong. Please try again");
            $("#error").removeClass('hidden');
            $("#processor").addClass("hidden");
            $("#btnForgot").removeClass("hidden");
            $("#btnForgot").prop("disabled", false);
        }
    });
}
