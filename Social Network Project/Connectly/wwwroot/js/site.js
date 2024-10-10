// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#showModal').on('click', function () {
    $('#invite').modal('show');
})

$('#showModal1').on('click', function () {
    $('#invite').modal('show');
})


$('#closeModal').on('click', function () {
    $('#invite').modal('toggle');
})

$('#show').on('click', function () {
    $('#logout').modal('show');
})

$('#show1').on('click', function () {
    $('#logout').modal('show');
})

$('#close').on('click', function () {
    $('#logout').modal('toggle');
})
