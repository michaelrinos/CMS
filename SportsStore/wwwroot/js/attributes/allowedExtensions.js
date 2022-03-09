/*
 Note that, jQuery validation registers its rules before the DOM is loaded.
 If you try to register your adapter after the DOM is loaded,
 your rules will not be processed. So wrap it in a self-executing function.
 */

$.validator.unobtrusive.adapters.add("allowedextensions", ['extensions'], function (options) {
    options.rules['allowedextensions'] = options.params.extensions.split("|");
    options.messages['allowedextensions'] = options.message;
});
$.validator.addMethod('allowedextensions', function (value, element, parameters) {
    if (!value) {
        return true;
    }
    var extension = getFileExtension(value);
    return $.inArray("." + extension, parameters) !== -1;
    /*
    if (!value) {
        return true;
    }
    var fileInput =
        document.getElementById('File');
    var filePath =
        fileInput.value;
    var allowedExtensions = new RegExp(parameters.extensions, 'i');
    if (!allowedExtensions.exec(filePath)) {
        //fileInput.value = '';

        debugger
        return false;
	}
	return true;*/
});
function getFileExtension(fileName) {
    if (/[.]/.exec(fileName)) {
        return /[^.]+$/.exec(fileName)[0].toLowerCase();
    }
    return null;
}
