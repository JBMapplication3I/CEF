$(document).ready(function(){
$(function(){
$('#navdttg li:has(ul)').doubleTapToGo();
});
// Mobile Menu - Go to top-level nav items
$('.navbar-mobile a.dropdown-toggle').on('click',function(e){
e.stopPropagation();
var link=$(this).attr('href');
location.href=link;
});
// Mobile Menu - Dropdown toggle 
$('.navbar-mobile li.dropdown > a.dropdown-toggle').each(function(){$(this).after('<div class="mobileBtn"><span class="caret"></span></div>')});
$('.navbar-mobile .mobileBtn').on('click',function(e){
e.stopPropagation();
$(this).parent().toggleClass('open');
});
});
