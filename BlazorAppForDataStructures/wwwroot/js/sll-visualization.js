(function () { // Wrap in IIFE to prevent global exposure
    let svg, nodes;
    let margin = { top: 20, right: 30, bottom: 40, left: 50 }
    window.drawLinkedList = function (singlyLinkedList) {
        // Setup SVG dimensions
        margin,
            width = 700 - margin.left - margin.right,
            height = 400 - margin.top - margin.bottom;

        // Clear previous SVG and create a new one
        d3.select("#sll-display").select("svg").remove();
        svg = d3.select("#sll-display").append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", `translate(${margin.left}, ${margin.top})`);

        // Create arrowhead marker
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
        nodes = singlyLinkedList.map((d, i) => ({
            ...d,
            x: i * 100 + 50, // Calculate x based on index
            y: height / 2 // Center y in the SVG
        }));

        // Draw nodes and node values
        nodes.forEach(node => {
            svg.append("circle")
                .attr("id", `node-${node.id}`)
                .attr("class", "node")
                .attr("cx", node.x)
                .attr("cy", node.y)
                .attr("r", 20)
                .style("fill", "skyblue")
                .style("stroke", "black")
                .style("stroke-width", 2);

            svg.append("text")
                .attr("x", node.x)
                .attr("y", node.y + 5) // Adjust for alignment
                .text(node.value)
                .attr("text-anchor", "middle")
                .style("fill", "black");
        });

        // Draw lines between nodes
        nodes.forEach((node, i) => {
            if (i < nodes.length - 1) {
                let nextNode = nodes[i + 1];
                drawLineWithArrow(node.x, node.y, nextNode.x, nextNode.y, 20, 2);

                // Assign an ID to the link based on the current node and the next node's IDs
                svg.select(`line:last-child`).attr('id', `link-${node.id}-${nextNode.id}`);
            }
        });

        // Helper function to adjust line endpoints to fit the arrowhead
        function adjustLineEndpoints(x1, y1, x2, y2, radius, strokeWidth) {
            const angle = Math.atan2(y2 - y1, x2 - x1);
            // Increase the effective radius by the stroke width
            const effectiveRadius = radius + strokeWidth;

            return {
                startX: x1 + Math.cos(angle) * effectiveRadius,
                startY: y1 + Math.sin(angle) * effectiveRadius,
                endX: x2 - Math.cos(angle) * effectiveRadius,
                endY: y2 - Math.sin(angle) * effectiveRadius
            };
        }

        // Draw line with arrowhead
        function drawLineWithArrow(startX, startY, endX, endY, radius, strokeWidth) {
            const adjustedPoints = adjustLineEndpoints(startX, startY, endX, endY, radius, strokeWidth);
            let line = svg.append('line')
                .attr('class', 'link')
                .attr('x1', adjustedPoints.startX)
                .attr('y1', adjustedPoints.startY)
                .attr('x2', adjustedPoints.endX)
                .attr('y2', adjustedPoints.endY)
                .attr('stroke', '#000')
                .attr('marker-end', 'url(#arrowhead)')
                .attr('stroke-dasharray', '1000')
                .attr('stroke-dashoffset', '1000');

            // Transition to "draw" the line
            line.transition()
                .duration(1000)  // Adjust to control "drawing" speed
                .attr('stroke-dashoffset', 0);
        }
    };

    function searchValue(value) {
        
        svg.selectAll('.node').style('fill', 'skyblue');
        svg.selectAll('.link').style('stroke', '#000');

        let found = false;
        let timeouts = []; // Array to store timeout IDs

        nodes.forEach((node, index) => {
            let timeoutId = setTimeout(() => {
                // Skip this part if the node has already been found
                if (!found) {
                    // Highlight the current node
                    svg.select(`#node-${node.id}`)
                        .transition()
                        .duration(500) // Slow down the highlighting effect
                        .style('fill', 'orange');

                    // Highlight the link leading to the current node
                    if (index > 0) {
                        let prevNode = nodes[index - 1];
                        svg.select(`#link-${prevNode.id}-${node.id}`)
                            .transition()
                            .duration(500) // Slow down the highlighting effect
                            .style('stroke', 'orange');
                    }
                }

                // If the node is found, update its color to green
                if (node.value === value && !found) {
                    svg.select(`#node-${node.id}`)
                        .transition()
                        .duration(500)
                        .style('fill', 'green');
                    found = true;

                    // Clear all scheduled timeouts to stop further color changes
                    timeouts.forEach(id => clearTimeout(id));
                }
            }, 1000 * index); // Delay based on index to visualize the search step by step

            // Store the timeout ID
            timeouts.push(timeoutId);
        });
    }

    window.searchValueInSLL = function (value) {
        console.log("searchValue called with value: " + value);
        searchValue(value);
    };
})();