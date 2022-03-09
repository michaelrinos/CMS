/*
 Note that, jQuery validation registers its rules before the DOM is loaded.
 If you try to register your adapter after the DOM is loaded,
 your rules will not be processed. So wrap it in a self-executing function.
 */

jQuery.validator.unobtrusive.adapters.add('maxfilesize', ['maxfilesize'], function (options) {
    options.messages['maxfilesize'] = options.message;
    options.rules['maxfilesize'] = options.params;
});
jQuery.validator.addMethod("maxfilesize", function (value, element, param) {
    if (value === "") {
        // no file
        return true;
    }

    var maxBytes = parseInt(param.maxfilesize);

    if (element.files != undefined && element.files[0] != undefined && element.files[0].size != undefined) {
        var filesize = parseInt(element.files[0].size);

        return filesize <= maxBytes;
    }

    return true;
});

jQuery.validator.unobtrusive.adapters.add('minfilesize', ['minfilesize'], function (options) {
    options.messages['minfilesize'] = options.message;
    options.rules['minfilesize'] = options.params;
});
jQuery.validator.addMethod("minfilesize", function (value, element, param) {
    if (value === "") {
        // no file
        return true;
    }

    var minBytes = parseInt(param.minfilesize);

    if (element.files != undefined && element.files[0] != undefined && element.files[0].size != undefined) {
        var filesize = parseInt(element.files[0].size);

        return filesize >= minBytes;
    }

    return true;
});
