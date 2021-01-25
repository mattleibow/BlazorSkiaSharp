
// hax

window.oldCleanupInit = App.cleanupInit;
window.onReadyCallback = undefined;
App.cleanupInit = function () {
    window.oldCleanupInit();
    App.isReady = true;
    if (window.onReadyCallback)
        window.onReadyCallback();
}

window.refreshCanvas = function (data, canvasId, width, height) {
    if (!App.isReady) {
        window.onReadyCallback = function () {
            window.refreshCanvas(data, canvasId, width, height);
        };
        return false;
    } else {
        window.onReadyCallback = undefined;
    }

    if (!window._InvalidateCanvasMethod) {
        window._InvalidateCanvasMethod = Module.mono_bind_static_method("[ConsoleApp1] ConsoleApp1.Program:InvalidateCanvas");
    }

    window._InvalidateCanvasMethod(data, canvasId, width, height);

    return true;
}

window.invalidateCanvas = function (pData, canvasId, width, height) {
    var htmlCanvas = document.getElementById(canvasId);
    htmlCanvas.width = width;
    htmlCanvas.height = height;

    var ctx = htmlCanvas.getContext('2d');
    if (!ctx)
        return false;

    var buffer = new Uint8ClampedArray(Module.HEAPU8.buffer, pData, width * height * 4);
    var imageData = new ImageData(buffer, width, height);
    ctx.putImageData(imageData, 0, 0);

    return true;
}
