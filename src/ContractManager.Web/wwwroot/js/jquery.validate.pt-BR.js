$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.split(".").join("").replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}

$.validator.methods.number = function (value, element) {
    return this.optional(element) || /-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}

// Use $.getJSON instead of $.get if your server is not configured to return the
// right MIME type for .json files.
$.when(
    $.get("/dist/js/cldr/likelySubtags.json"),
    $.get("/dist/js/cldr/numbers.json"),
    $.get("/dist/js/cldr/numberingSystems.json"),
    $.get("/dist/js/cldr/ca-gregorian.json"),
    $.get("/dist/js/cldr/timeZoneNames.json"),
    $.get("/dist/js/cldr/timeData.json"),
    $.get("/dist/js/cldr/weekData.json")
).then(function () {
    // Normalize $.get results, we only need the JSON, not the request statuses.
    return [].slice.apply(arguments, [0]).map(function (result) {
        return result[0];
    });
}).then(Globalize.load).then(function () {
    Globalize.locale("pt"); //$("html").prop("lang")
});