window.drawLinkedList = function (singlyLinkedList) {
    let margin = { top: 20, right: 30, bottom: 40, left: 50 },
        width = 700 - margin.left - margin.right,
        height = 400 - margin.top - margin.bottom;

    d3.select("#sll-display").select("svg").remove();

    let svg = d3.select("#sll-display").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    
    let nodes = singlyLinkedList.map((d, i) => ({ ...d, x: i * 100 + 50, y: height / 2 }));

    // Draw lines for links
    svg.selectAll(".link")
        .data(nodes.slice(1)) // Exclude the first element to draw links correctly
        .enter().append("line")
        .attr("class", "link")
        .attr("x1", (d, i) => nodes[i].x) // Use previous node's x
        .attr("y1", d => d.y)
        .attr("x2", d => d.x)
        .attr("y2", d => d.y)
        .attr("stroke", "black");

    // Draw nodes
    svg.selectAll(".node")
        .data(nodes)
        .enter().append("circle")
        .attr("class", "node")
        .attr("cx", d => d.x)
        .attr("cy", d => d.y)
        .attr("r", 20)
        .style("fill", "skyblue");

    // Draw node values
    svg.selectAll(".text")
        .data(nodes)
        .enter().append("text")
        .attr("x", d => d.x)
        .attr("y", d => d.y + 5) // Adjusted for better visual alignment
        .text(d => d.value)
        .attr("text-anchor", "middle")
        .style("fill", "black");
}
