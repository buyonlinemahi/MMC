$(".phoneMaskformat").mask("(999) 999-9999");
$(".tin").mask("99-9999999");
$(".ssn").mask("999-99-9999");
$(".time").mask("99:99");
$(".zipMaskformat").mask("99999?-9999")
              .blur(function () {
                  var lastFour = $(this).val().substr(6, 4);
                  if (lastFour != "") {
                      if (lastFour.length != 4) {
                          $(this).val("");
                      }
                  }
              });


$(".number").keydown(function (e) {
    // Allow: backspace, delete, tab, escape, enter, f5 and f6
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 117, 116]) !== -1 ||
        // Allow: Ctrl+A, Command+A
        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});

// use to block popup background
function blockPopupBackground() {
    $("#background-main-div").removeClass("hidden");    
}
// use to unblock popup background
function unblockPopupBackground() {
    $("#background-main-div").addClass("hidden");    
}

// to display loader
function showLoader()
{
    $("#loader-main-div").removeClass("hidden");
}

//to remove loader
function hideLoader()
{
    $("#loader-main-div").addClass("hidden");
}

$(".txtSearch").keypress(function (e) {
    e = e || event;
    return /[a-z 0-9]/i.test(String.fromCharCode(e.charCode || e.keyCode))
            || !!(!e.charCode && ~[8, 37, 39, 46].indexOf(e.keyCode));
});

function calculateAge(dob) {
    var now = new Date();
    var past = new Date(dob);
    var nowYear = now.getFullYear();
    var pastYear = past.getFullYear();
    var age = nowYear - pastYear;

    return age;
};

$('.datepicker').datepicker({ autoclose: true });

//$('.datepicker').on('changeDate', function (ev) {
//    $(this).datepicker('hide');
//});

$("#txtsearchPat").keypress(function (e) {
    var key = e.which;
    if (key == 13)  // the enter key code
    {
        searchPatClaim();
    }
});