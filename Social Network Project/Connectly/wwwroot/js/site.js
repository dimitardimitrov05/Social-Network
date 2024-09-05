// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#showModal').on('click', function () {
    $('#invite').modal('show');
})

$('#closeModal').on('click', function () {
    $('#invite').modal('toggle');
})