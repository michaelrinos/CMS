// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    var NotFirst = false;
    $(".toggle").on("click", function () {
        $("#myTab").addClass("d-none");
        $("#codeParent").toggleClass("container");
        $("#tabs").toggleClass(["tab-content", "row"]);

        var html = $("#HTML");
        var css = $("#CSS");
        var js = $("#JavaScript");

        if (!html.hasClass("active") || NotFirst)
            html.toggleClass("active");
        html.toggleClass("col-4");

        if (!css.hasClass("active") || NotFirst)
            css.toggleClass("active");
        css.toggleClass("col-4");

        if (!js.hasClass("active") || NotFirst)
            js.toggleClass("active");
        js.toggleClass("col-4");
        NotFirst = true;
    });
    /*
    $("editor-style").on("click", function () {
        $("#HTML").addClass("col-4")
        $("#CSS").addClass("col-4")
        $("#JavaScript").addClass("col-4")
        /*
        if ($('#DivID').length) {
            alert('Found with Length');
        }
        // */
    // */
});

function preventDefault(event) {
    event.preventDefault();
};
