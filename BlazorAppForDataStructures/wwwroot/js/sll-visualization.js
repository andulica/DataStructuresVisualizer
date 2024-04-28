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
            .attr('class', 'link')
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
            let timeouts = []; // Array to store timeout IDs for potential clearing
            let found = false;

            nodes.forEach((node, index) => {
                let timeout = setTimeout(() => {
                    if (found) {
                        clearTimeout(timeout); // Prevent this timeout's actions if already found
                        return;
                    }

                    svg.select(`#node-${node.id}`)
                        .transition().duration(500)
                        .style('fill', 'orange');

                    if (index > 0) {
                        highlightLinkAndArrowhead(nodes[index - 1].id, node.id);
                    }

                    if (node.value === value) {
                        svg.select(`#node-${node.id}`)
                            .transition().duration(500)
                            .style('fill', 'green');
                        found = true;
                        clearTimeouts(timeouts); // Clear all remaining timeouts
                        resolve();
                    }
                }, 1000 * index);

                timeouts.push(timeout);
            });

            // Ensure we resolve the promise if no node matches
            let finalTimeout = setTimeout(() => {
                if (!found) resolve();
            }, 1000 * nodes.length);
            timeouts.push(finalTimeout);
        });
    }

    function clearTimeouts(timeouts) {
        timeouts.forEach(timeout => clearTimeout(timeout));
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
            nodes.splice(position, 0, newNode); // Insert the new node into the array at the specified position

            let prevNode;
            let nextNode;

            let link1Id;
            let link2Id;

            if (position === 0) {
                if (nodes.length > 1) { // Check if there is a node to link to
                    drawLineWithArrow(newNode.x, newNode.y, nodes[1].x, nodes[1].y, 20, 2, `link-${newNode.id}-${nodes[1].id}`);
                }
            } else {
                prevNode = nodes[position - 1];
                nextNode = nodes[position + 1];

                link1Id = `link-${prevNode.id}-${newNode.id}`;
                link2Id = `link-${newNode.id}-${nextNode.id}`;

                drawLineWithArrow(prevNode.x, prevNode.y, newNode.x, newNode.y, 20, 2, `link-${prevNode.id}-${newNode.id}`);
                if (nextNode) {
                    drawLineWithArrow(newNode.x, newNode.y, nextNode.x, nextNode.y, 20, 2, `link-${newNode.id}-${nextNode.id}`);
                }
            }
            setTimeout(() => {
                svg.select(`#${link1Id}`).remove();
                svg.select(`#${link2Id}`).remove();
                updateNodePositions();
                redrawLinks();
                repositionText();
                setTimeout(() => {
                    resetNodeColors();
                }, 1300);
            }, 1200);
        }, 1000);
    }

    function updateNodePositions() {
        const nodeSpacing = 100;
        nodes.forEach((node, index) => {
            node.x = Math.round(index * nodeSpacing + 50);
            node.y = Math.round(height / 2);
            svg.select(`#node-${node.id}`)
                .transition()
                .duration(500)
                .attr("cx", node.x)
                .attr("cy", node.y);
        });
    }

    function redrawLinks() {
        svg.selectAll('line').remove();
        nodes.forEach((node, index) => {
            if (index < nodes.length - 1) {
                let nextNode = nodes[index + 1];
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

    function resetLinkColors() {
        // Select all links and reset their styles
        svg.selectAll(".link")
            .transition().duration(500)
            .style('stroke', '#000') // Default stroke color
            .attr('marker-end', 'url(#arrowhead)'); // Default arrowhead, not the highlighted one
    }

    function removeNodeInSll(nodeToBeRemoved) {
        return new Promise((resolve) => {
            highlightNodes(nodeToBeRemoved.value).then(() => {
                setTimeout(() => {
                    // Transition and then remove the node's visual elements
                    svg.select(`#node-${nodeToBeRemoved.id}`)
                        .transition().duration(500)
                        .style('opacity', 0) // Fade out effect
                        .on('end', () => {
                            svg.select(`#node-${nodeToBeRemoved.id}`).remove();
                            // Proceed with text removal after node is removed
                            svg.select(`#textId-${nodeToBeRemoved.id}`)
                                .transition().duration(500)
                                .style('opacity', 0) // Fade out effect for text
                                .on('end', () => {
                                    svg.select(`#textId-${nodeToBeRemoved.id}`).remove();

                                    // Update links if necessary and resolve when complete
                                    updateLinksAfterRemoval(nodeToBeRemoved);

                                    // Once links are updated, reposition nodes, text and redraw links
                                    updateNodePositions();
                                    redrawLinks();
                                    repositionText();

                                    resolve(); // Ensure all transitions have time to complete
                                });
                        });
                }, 1000); // Delay before removing the node to allow for highlighting
            });
        });
    }

    function updateLinksAfterRemoval(nodeToBeRemoved) {
        let nodeIndex = nodes.findIndex(node => node.id === nodeToBeRemoved.id);
        nodes = nodes.filter(node => node.id !== nodeToBeRemoved.id); // Remove the node from the nodes array

        if (nodeIndex > 0 && nodeIndex < nodes.length) {
            // Node is in the middle of the list
            let prevNode = nodes[nodeIndex - 1];
            let nextNode = nodes[nodeIndex]; // Now refers to the next node after the removed one

            // Remove the old link from the previous node to the removed node
            svg.select(`#link-${prevNode.id}-${nodeToBeRemoved.id}`).remove();

            drawLineWithArrow(prevNode.x, prevNode.y, nextNode.x, nextNode.y, 20, 2, `link-${prevNode.id}-${nextNode.id}`);
        } else if (nodeIndex > 0) {
            // Removing the last node
            svg.select(`#link-${nodes[nodeIndex - 1].id}-${nodeToBeRemoved.id}`).remove();
        } else if (nodes.length) {
            // Removing the first node and there are other nodes
            svg.select(`#link-${nodeToBeRemoved.id}-${nodes[0].id}`).remove();
        }
    }

    window.searchValueInSLL = function (value) {
        resetNodeColors();
        resetLinkColors();
        highlightNodes(value);
    };

    window.insertAtInSLL = function (value, selectedIndex) {
        resetNodeColors();
        resetLinkColors();
        insertNode(value, selectedIndex);
    };

    window.removeValueInSll = function (nodeToBeRemoved) {
        resetNodeColors();
        resetLinkColors();
        removeNodeInSll(nodeToBeRemoved);
    };
})();
