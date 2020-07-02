import * as d3 from 'd3';
import '../styles/canvas.css'
import '../styles/inputfile.css'

var margin = 30;

function Redraw(value){
    d3.select("#maincanvas").html(null);
    return value?PreparePlane(value):PreparePlane(45);
}

function PreparePlane(scale){
    var width = window.innerWidth - 30 - (window.innerWidth * 0.15);
    var height = window.innerHeight - (window.innerHeight * 0.08);
    

    var svg = d3.select("#maincanvas").append("svg")
            .attr("class", "axis")
            .attr("width", width)
            .attr("height", height);
    
    var xAxisLength = width - 2 * margin;     
    
    var yAxisLength = height - 2 * margin;
    
    var scaleX = d3.scaleLinear()
                .domain([0, xAxisLength/scale])
                .range([0, xAxisLength]);
    var scaleY = d3.scaleLinear()
                .domain([yAxisLength/scale, 0])
                .range([0, yAxisLength]);

    var xAxis = d3.axisBottom(scaleX).ticks(xAxisLength/scale);    
    var yAxis = d3.axisLeft(scaleY).ticks(yAxisLength/scale);
                            
    svg.append("g")       
        .attr("class", "x-axis")
        .attr("transform",
            "translate(" + margin + "," + (height - margin) + ")")
        .call(xAxis);
                
    svg.append("g")       
        .attr("class", "y-axis")
        .attr("transform",
                "translate(" + margin + "," + margin + ")")
        .call(yAxis);
    
    d3.selectAll("g.x-axis g.tick")
        .append("line")
        .classed("grid-line", true)
        .attr("x1", 0)
        .attr("y1", 0)
        .attr("x2", 0)
        .attr("y2", - (yAxisLength));
        
    d3.selectAll("g.y-axis g.tick")
        .append("line")
        .classed("grid-line", true)
        .attr("x1", 0)
        .attr("y1", 0)
        .attr("x2", xAxisLength)
        .attr("y2", 0);
    
    return {svg, scaleX, scaleY};
}

export default Redraw;