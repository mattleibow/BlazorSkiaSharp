
window.SumMethod = function (a, b) {
    if (!window._progSum) {
        window._progSum = Module.mono_bind_static_method("[ConsoleApp1] ConsoleApp1.Program:SumMethod");
    }

    return window._progSum(a, b);
}

window.refreshCanvas = function (data, canvasId, width, height) {
    if (!window._InvalidateCanvasMethod) {
        window._InvalidateCanvasMethod = Module.mono_bind_static_method("[ConsoleApp1] ConsoleApp1.Program:InvalidateCanvas");
    }

    window._InvalidateCanvasMethod(data, canvasId, width, height);
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
