$(document).ready(function () {
$(".carousel").carousel({ interval: 5000 });
$(".nav > li.dropdown > a").click(function (e) {
var $target = $(e.target);
var activeNav = $(this).siblings();
if ($target.is("b")) {
$(this).siblings().toggle("fast");
$(".nav > li.dropdown > ul.dropdown-menu:visible").not($(this).siblings()).hide("fast");
return false;
}
});
});

$('#ddlnewUserRegistrationType').on('change', function() {
    if (this.value === "number:3") {
        $("#dnn_FluidPane").removeClass("d-none");
    } else {
        $("#dnn_FluidPane").addClass("d-none");
    }
});
