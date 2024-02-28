window.drawLinkedList = function (singlyLinkedList) {
    // Setup SVG dimensions and margins
    let margin = { top: 20, right: 30, bottom: 40, left: 50 },
        width = 700 - margin.left - margin.right,
        height = 400 - margin.top - margin.bottom;

    // Clear previous SVG and create a new one
    d3.select("#sll-display").select("svg").remove();
    let svg = d3.select("#sll-display").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", `translate(${margin.left}, ${margin.top})`);

    // Define arrowhead marker
    svg.append('defs').append('marker')
        .attr('id', 'arrowhead')
        .attr('viewBox', '-0 -5 10 10')
        .attr('refX', 5)
        .attr('refY', 0)
        .attr('orient', 'auto')
        .attr('markerWidth', 6)
        .attr('markerHeight', 6)
        .attr('xoverflow', 'visible')
        .append('svg:path')
        .attr('d', 'M 0,-5 L 10 ,0 L 0,5')
        .attr('fill', '#000');

    // Prepare node data with positions
    let nodes = singlyLinkedList.map((d, i) => ({
        ...d,
        x: i * 100 + 50, // Calculate x based on index
        y: height / 2 // Center y in the SVG
    }));

    // Draw nodes and node values
    nodes.forEach(node => {
        svg.append("circle")
            .attr("class", "node")
            .attr("cx", node.x)
            .attr("cy", node.y)
            .attr("r", 20)
            .style("fill", "skyblue");

        svg.append("text")
            .attr("x", node.x)
            .attr("y", node.y + 5) // Adjust for alignment
            .text(node.value)
            .attr("text-anchor", "middle")
            .style("fill", "black");
    });

    // Draw lines for links with adjusted endpoints
    nodes.forEach((node, i) => {
        if (i < nodes.length - 1) { // Don't draw for the last node
            let nextNode = nodes[i + 1];
            let adjustedPoints = adjustLineEndpoints(node.x, node.y, nextNode.x, nextNode.y, 20); // 20 is the radius of the node

            svg.append('line')
                .attr('x1', adjustedPoints.startX)
                .attr('y1', adjustedPoints.startY)
                .attr('x2', adjustedPoints.endX)
                .attr('y2', adjustedPoints.endY)
                .attr('stroke', '#000')
                .attr('marker-end', 'url(#arrowhead)');
        }
    });

    // Adjust endpoint function to prevent line overlap with nodes
    function adjustLineEndpoints(x1, y1, x2, y2, r) {
        const angle = Math.atan2(y2 - y1, x2 - x1);
        return {
            startX: x1 + Math.cos(angle) * r,
            startY: y1 + Math.sin(angle) * r,
            endX: x2 - Math.cos(angle) * r,
            endY: y2 - Math.sin(angle) * r
        };
    }
};

