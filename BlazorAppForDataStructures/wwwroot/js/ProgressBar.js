window.initializeProgressBar = function (elementId, maxValue, currentValue, dotNetObject) {
    $(`#${elementId}`).slider({
        range: "min",
        min: 0,
        max: maxValue,
        value: currentValue,
        slide: function (event, ui) {
            dotNetObject.invokeMethodAsync('HandleValueChanged', ui.value);
        }

    });
};

window.updateProgressBar = function (elementId, maxValue, currentValue) {
    $(`#${elementId}`).slider("option", "max", maxValue);
    $(`#${elementId}`).slider("option", "value", currentValue);
};
