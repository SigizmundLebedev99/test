import * as d3 from 'd3';

var margin = 30;

function HandleData(data, canvasState){
    PrintCoordinates(data);
    DrawPath(data, canvasState);
    DrawTransmitters(data, canvasState);
}

function PrintCoordinates(data){
    var list = d3.select("#coordinates").html(null);
    data.coordinates.forEach((data, i) => {
        if(data.success)
            list.append('p').text(`${i + 1}) X: ${data.payload.x} / Y: ${data.payload.y}`);
        else
            list.append('p').text(`${i + 1}) Невалидный ввод`);
    });
}

function DrawTransmitters(data, {svg, scaleX, scaleY}){
    var t1 = data.transmitter1;
    var t2 = data.transmitter2;
    var t3 = data.transmitter3;
    [t1,t2,t3].forEach(t=>{
        svg.append("circle")
            .attr("class", "red")
            .attr("r", 5)
            .attr("cx", scaleX(t.x)+margin)
            .attr("cy", scaleY(t.y)+margin);
    });

    ////should delete
    let dist = (x,y, t) => {
        return Math.sqrt(Math.pow(x-t.x, 2) + Math.pow(y-t.y,2))
    }
    
    svg.on('mousedown', function(){
        let xy = d3.mouse(d3.event.currentTarget);
        var x = xy[0] - 30;
        var y = xy[1];
        var width = window.innerWidth - 30 - (window.innerWidth * 0.15);
        var height = window.innerHeight - (window.innerHeight * 0.08);
        console.log()
        var xAxisLength = width - 2 * margin;  
        var yAxisLength = height - margin;
        var _scaleX = d3.scaleLinear()
                .domain([0, xAxisLength])
                .range([0, xAxisLength/45]);
        var _scaleY = d3.scaleLinear()
                .domain([0, yAxisLength])
                .range([yAxisLength/45, 0]);

        [t1,t2,t3].forEach(t=>{
            var _x = _scaleX(x);
            var _y = _scaleY(y);
            var d = dist(_x,_y,t);
            console.log(d);
        });
    });
    ///// should delete
}

function DrawPath(data, {svg, scaleX, scaleY}){
    var dotData = data.coordinates.filter(c=>c.success).map(c=>c.payload);
    var lineData = dotData.map(e=>({x: scaleX(e.x)+margin, y: scaleY(e.y) + margin}));
    var line = d3.line()
        .x(function(d){return d.x;})
        .y(function(d){return d.y;});
    var g = svg.append("g");
    g.append("path")
        .attr("d", line(lineData))
        .style("stroke", "steelblue")
        .style("stroke-width", 2);
    svg.selectAll(".dot")
        .data(dotData)
        .enter().append("circle")
        .attr("class", "dot")
        .attr("r", 3.5)
        .attr("cx", function(d) { return scaleX(d.x)+margin; })
        .attr("cy", function(d) { return scaleY(d.y)+margin; });   
}

export default HandleData;