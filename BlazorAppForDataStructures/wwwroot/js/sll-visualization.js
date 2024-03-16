
(function () {
    let svg, nodes;
    let margin = { top: 20, right: 30, bottom: 40, left: 50 };

    // Helper function to adjust line endpoints to fit the arrowhead
    function adjustLineEndpoints(x1, y1, x2, y2, radius, strokeWidth) {
        const angle = Math.atan2(y2 - y1, x2 - x1);
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
        svg.append('line')
            .attr('class', 'link')
            .attr('x1', adjustedPoints.startX)
            .attr('y1', adjustedPoints.startY)
            .attr('x2', adjustedPoints.endX)
            .attr('y2', adjustedPoints.endY)
            .attr('stroke', '#000')
            .attr('marker-end', 'url(#arrowhead)')
            .attr('stroke-dasharray', '1000')
            .attr('stroke-dashoffset', '1000')
            .transition()
            .duration(1000)
            .attr('stroke-dashoffset', 0);
    }

    window.drawLinkedList = function (singlyLinkedList) {
        // Setup SVG dimensions
        margin, width = 700 - margin.left - margin.right, height = 400 - margin.top - margin.bottom;

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
    };

    function highlightNodes(condition) {
        return new Promise((resolve) => {
            let timeouts = []; // Store timeout IDs for potential clearing

            nodes.forEach((node, index) => {
                let timeoutId = setTimeout(() => {
                    // Highlight the current node
                    svg.select(`#node-${node.id}`).transition().duration(500).style('fill', 'orange');

                    // Highlight the link from the previous node
                    if (index > 0) {
                        svg.select(`#link-${nodes[index - 1].id}-${node.id}`).transition().duration(500).style('stroke', 'orange');
                    }

                    // Check the stopping condition
                    if (condition(index, node)) {
                        clearTimeouts(timeouts);
                        resolve(); // Resolve the promise once the condition is met
                    }
                }, 1000 * index);

                timeouts.push(timeoutId);
            });
        });
    }

    function clearTimeouts(timeouts) {
        timeouts.forEach((timeoutId) => clearTimeout(timeoutId));
    }

    function createNewNode(value, position) {
        let targetX = nodes[position].x;
        let targetY = nodes[position].y - 50;

        // Create the new node circle
        let newNode = svg.append("circle")
            .attr("cx", targetX)
            .attr("cy", targetY)
            .attr("r", 20)
            .style("fill", "green")
            .attr("id", `node-${value}`)
            .style("stroke", "black")
            .style("stroke-width", 2);

        // Add the value text
        svg.append("text")
            .attr("x", targetX)
            .attr("y", targetY + 5)
            .text(value)
            .attr("text-anchor", "middle")
            .style("fill", "black");

        // Return the new node object with necessary properties
        return {
            id: `node-${value}`, // Adjust ID generation as needed
            x: targetX,
            y: targetY,
            value: value
        };
    }


    function adjustLinks(newNode, position) {
        if (position < 0 || position >= nodes.length) {
            console.error("Position out of bounds");
            return;
        }

        let originalNode = nodes[position];

        // Insert the new node in the nodes array at the correct position
        nodes.splice(position, 0, newNode);

        let prevNode = position > 0 ? nodes[position - 1] : null;

        // Redirect the link from the previous node to the new node, if there's a previous node
        if (prevNode) {
            // Remove the old link if it exists
            svg.select(`#link-${prevNode.id}-${originalNode.id}`).remove();

            // Draw a new link with an arrow from the previous node to the new node
            drawLineWithArrow(prevNode.x, prevNode.y, newNode.x, newNode.y, 20, 2);
        }

        // Draw a new link with an arrow from the new node to the original node
        if (newNode && originalNode) {
            drawLineWithArrow(newNode.x, newNode.y, originalNode.x, originalNode.y, 20, 2);
        }
    }



    async function insertNode(value, position) {
        await highlightNodes((index) => index === position); // Highlight nodes up to the position

        // Delay the creation of the new node after the target position is found
        setTimeout(() => {
            let newNode = createNewNode(value, position);
            adjustLinks(newNode, position); 
        }, 1000);
    }

    window.searchValueInSLL = function (value, selectedIndex) {
        insertNode(value, selectedIndex);
    };
})();
