﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
    $(document).ready(function () {
        $('.sidebar .nav-link').on('click', function () {
            $('.sidebar .nav-link').removeClass('active'); // Remove from all
            $(this).addClass('active'); // Add to clicked one
        });
    });