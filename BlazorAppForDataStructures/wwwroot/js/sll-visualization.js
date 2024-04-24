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

    // Draw line with arrow and assign an ID immediately
    function drawLineWithArrow(startX, startY, endX, endY, radius, strokeWidth, id) {
        const adjustedPoints = adjustLineEndpoints(startX, startY, endX, endY, radius, strokeWidth);
        const line = svg.append('line')
            .attr('x1', adjustedPoints.startX)
            .attr('y1', adjustedPoints.startY)
            .attr('x2', adjustedPoints.endX)
            .attr('y2', adjustedPoints.endY)
            .attr('stroke', '#000')
            .attr('marker-end', 'url(#arrowhead)')
            .attr('stroke-width', strokeWidth);

        if (id) {
            line.attr('id', id);
        }

        return line;
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

        svg.append('defs').append('marker')
            .attr('id', 'highlighted-arrowhead')
            .attr('viewBox', '-0 -5 10 10')
            .attr('refX', 5)
            .attr('refY', 0)
            .attr('orient', 'auto')
            .attr('markerWidth', 6)
            .attr('markerHeight', 6)
            .attr('xoverflow', 'visible')
            .append('svg:path')
            .attr('d', 'M 0,-5 L 10 ,0 L 0,5')
            .attr('fill', 'orange');

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
                .attr("id", `textId-${node.id}`)
                .attr("class", "text")
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
                drawLineWithArrow(node.x, node.y, nextNode.x, nextNode.y, 20, 2, `link-${node.id}-${nextNode.id}`);
            }
        });        
    };

    function highlightNodesForInsertion(condition) {
        return new Promise((resolve) => {
            let timeouts = []; // Store timeout IDs for potential clearing

            nodes.forEach((node, index) => {
                let timeoutId = setTimeout(() => {
                    // Highlight the current node
                    svg.select(`#node-${node.id}`).transition().duration(500).style('fill', 'orange');

                    // Highlight the link and the arrowhead from the previous node
                    if (index > 0) {
                        highlightLinkAndArrowhead(nodes[index - 1].id, node.id);
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

    function highlightLinkAndArrowhead(sourceNodeId, targetNodeId) {
        let linkId = `#link-${sourceNodeId}-${targetNodeId}`;
        svg.select(linkId)
            .transition().duration(500)
            .style('stroke', 'orange')
            .attr('marker-end', 'url(#highlighted-arrowhead)');
    }


    function highlightNodes(value) {
        return new Promise((resolve) => {
            let timeouts = [];
            let nodesProcessed = 0;

            nodes.forEach((node, index) => {
                let timeout = setTimeout(() => {
                    console.log(`Processing node with id: ${node.id} and value: ${node.value}`);
                    svg.select(`#node-${node.id}`)
                        .transition().duration(500)
                        .style('fill', 'orange');

                    if (index > 0) {
                        highlightLinkAndArrowhead(nodes[index - 1].id, node.id);
                    }

                    if (node.value === value) {
                        console.log(`Value match found for node id: ${node.id}`);
                        svg.select(`#node-${node.id}`)
                            .transition().duration(500)
                            .style('fill', 'green');
                    }

                    nodesProcessed++;
                    if (nodesProcessed === nodes.length) {
                        resolve();
                    }
                }, 1000 * index);

                timeouts.push(timeout);
            });
        });
    }

    function clearTimeouts(timeouts) {
        timeouts.forEach((timeoutId) => clearTimeout(timeoutId));
    }

    function createNewNode(node, position) {
        let targetX = nodes[position].x;
        let targetY = nodes[position].y - 50;

        // Create the new node circle
        svg.append("circle")
            .attr("id", `node-${node.id}`)
            .attr("class", "node")
            .attr("cx", targetX)
            .attr("cy", targetY)
            .attr("r", 20)
            .style("fill", "green")
            .style("stroke", "black")
            .style("stroke-width", 2);

        // Add the value text
        svg.append("text")
            .attr("id", `textId-${node.id}`)
            .attr("x", targetX)
            .attr("y", targetY + 5)
            .text(node.value)
            .attr("text-anchor", "middle")
            .style("fill", "black");

        return {
            id: node.id,
            x: targetX,
            y: targetY,
            value: node.value
        };
    }

    async function insertNode(value, position) {
        await highlightNodesForInsertion((index) => index === position); // Highlight nodes up to the position

        // Delay the creation of the new node after the target position is found
        setTimeout(() => {
            let newNode = createNewNode(value, position);
            nodes.splice(position, 0, newNode);
            let prevNode = position > 0 ? nodes[position - 1] : null;
            let nextNode = position > 0 ? nodes[position + 1] : null;

            // Inside insertNode function
            let link1Id = `link-${prevNode.id}-${newNode.id}`;
            let link2Id = `link-${newNode.id}-${nextNode.id }`;

            drawLineWithArrow(prevNode.x, prevNode.y, newNode.x, newNode.y, 20, 2, link1Id);
            drawLineWithArrow(newNode.x, newNode.y, nextNode.x, nextNode.y, 20, 2, link2Id);

            setTimeout(() => {
                svg.select(`#${link1Id}`).remove();
                svg.select(`#${link2Id}`).remove();
                repositionNodes();
                repositionText();
                setTimeout(() => {
                    resetNodeColors();
                }, 1300);
            }, 1200);
        }, 1000);
    }

    function repositionNodes() {
        const nodeSpacing = 100; 

        svg.selectAll('line').remove();


        nodes.forEach((node, index) => {
            node.x = Math.round(index * nodeSpacing + 50);
            node.y = Math.round(height / 2);

            // Update the circle's position
            svg.select(`#node-${node.id}`)
                .transition()
                .duration(500)
                .attr("cx", node.x)
                .attr("cy", node.y);
        });

        // Update the links
        nodes.forEach((node, i) => {
            if (i < nodes.length - 1) {
                let nextNode = nodes[i + 1];
                drawLineWithArrow(node.x, node.y, nextNode.x, nextNode.y, 20, 2, `link-${node.id}-${nextNode.id}`);
            }
        });
    }

    function repositionText() {
        nodes.forEach(node => {
            svg.select(`#textId-${node.id}`)
                .transition()
                .duration(500)
                .attr("x", node.x)
                .attr("y", node.y + 5);
        });
    }

    function resetNodeColors() {
        nodes.forEach(node => {
            svg.select(`#node-${node.id}`)
                .transition()
                .duration(500)
                .style('fill', 'skyblue');
        });
    }

    function removeNodeInSll(nodeToBeRemoved) {
        highlightNodes(nodeToBeRemoved.value).then(() => {
            // Remove the node's visual elements
            svg.select(`#node-${nodeToBeRemoved.id}`).remove();
            svg.select(`#text-${nodeToBeRemoved.id}`).remove();

            // Update links if necessary
            updateLinksAfterRemoval(nodeToBeRemoved);

            // Optionally reset or update the entire visualization here
        });
    }

    function updateLinksAfterRemoval(nodeToBeRemoved) {
        let nodeIndex = nodes.findIndex(node => node.id === nodeToBeRemoved.id);
        nodes = nodes.filter(node => node.id !== nodeToBeRemoved.id); // Update the nodes array

        if (nodeIndex > 0 && nodeIndex < nodes.length) {
            // Update the previous link to point to the next node if not removing the last node
            let prevNode = nodes[nodeIndex - 1];
            let nextNode = nodes[nodeIndex]; // This now refers to the node after the removed node
            svg.select(`#link-${prevNode.id}-${nodeToBeRemoved.id}`).attr('x2', nextNode.x).attr('y2', nextNode.y);
        } else if (nodeIndex > 0) {
            // Removing the last node
            svg.select(`#link-${nodes[nodeIndex - 1].id}-${nodeToBeRemoved.id}`).remove();
        } else if (nodes.length) {
            // Removing the first node when there are other nodes
            svg.select(`#link-${nodeToBeRemoved.id}-${nodes[0].id}`).remove();
        }
    }


    window.searchValueInSLL = function (value) {
        // Reset node colors first
        resetNodeColors();

        // Then highlight nodes based on the value
        highlightNodes(value);
    };



    window.insertAtInSLL = function (value, selectedIndex) {
        insertNode(value, selectedIndex);
    };

    window.removeValueInSll = function (nodeToBeRemoved) {
        removeNodeInSll(nodeToBeRemoved);
    };
})();
