import './styles/style.css';

import fillCanvas from './client-logic/init-canvas';
import setCallback from './client-logic/file-input';
import handleCoordinates from './client-logic/handle-coordinates'

var canvasState = fillCanvas();
var coordinates = null;

function redraw(){
    canvasState = fillCanvas(scale.value);
    if(coordinates)
        handleCoordinates(coordinates, canvasState);
}

let scale = document.getElementById("scale-select");

scale.onchange = redraw;

window.onresize = redraw;

setCallback((data) => {
    coordinates = data;
    redraw();
});