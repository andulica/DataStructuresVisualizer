(function () {
    window.cancelVisuals = function () {
        clearAllTimeouts(timeouts)
    }

    // Helper function to clear all timeouts
    function clearAllTimeouts(timeouts) {
        timeouts.forEach(timeoutId => clearTimeout(timeoutId));
    }

    let svg, nodes;
    let margin = { top: 20, right: 30, bottom: 40, left: 50 };
    const delayDrawLinks = 1000;
    const gapBetweenNodeAndFirstElement = 100;

    let timeouts = [];

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
    function drawLineWithArrow(startX, startY, endX, endY, radius, strokeWidth, id, delay) {
        const adjustedPoints = adjustLineEndpoints(startX, startY, endX, endY, radius, strokeWidth);

        // Calculate the total length of the line
        const lineLength = Math.sqrt(Math.pow(adjustedPoints.endX - adjustedPoints.startX, 2) + Math.pow(adjustedPoints.endY - adjustedPoints.startY, 2));
        const line = svg.append('line')
            .attr('class', 'link')
            .attr('x1', adjustedPoints.startX)
            .attr('y1', adjustedPoints.startY)
            .attr('x2', adjustedPoints.startX)
            .attr('y2', adjustedPoints.startY)
            .attr('stroke', '#000')
            .attr('marker-end', 'url(#arrowhead)')
            .attr('stroke-width', strokeWidth)
            .attr('stroke-dasharray', lineLength)
            .attr('stroke-dashoffset', lineLength)
            .transition()
            .duration(delay)
            .attr('x2', adjustedPoints.endX)
            .attr('y2', adjustedPoints.endY)
            .attr('stroke-dashoffset', 0);

        if (id) {
            line.attr('id', id);
        }

        return line;
    }

    window.drawLinkedList = function (singlyLinkedListOrStack, isStack = false) {

        margin = { top: 20, right: 20, bottom: 20, left: 20 };
        width = (isStack ? 200 : 700) - margin.left - margin.right; // Adjust width for stack or list
        height = (isStack ? 500 : 250) - margin.top - margin.bottom; // Adjust height for stack or list

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

        // Prepare node data with positions based on isStack
        let nodeSpacing = isStack ? 60 : 100; // Adjust spacing between nodes for stack or list
        nodes = singlyLinkedListOrStack.map((d, i) => ({
            ...d,
            x: isStack ? width / 2 : i * nodeSpacing + 50,  // Center for stack, horizontal for list
            y: isStack ? (i === 0 ? gapBetweenNodeAndFirstElement : gapBetweenNodeAndFirstElement + i * nodeSpacing) : height / 2  // Vertical for stack with gap, centered for list
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
                drawLineWithArrow(node.x, node.y, nextNode.x, nextNode.y, 20, 2, `link-${node.id}-${nextNode.id}`, delayDrawLinks);
            }
        });
    };

    async function highlightNodesForInsertion(position, delay) {
        return new Promise((resolve) => {
            let found = false;

            // Iterate over nodes and schedule highlighting
            nodes.forEach((node, index) => {
                setCheckedTimeout(() => {
                    if (found) return; // Stop further highlighting once the condition is met

                    // Highlight the current node
                    svg.select(`#node-${node.id}`)
                        .transition()
                        .duration(delay)
                        .style('fill', 'orange');

                    // Highlight the link and the arrowhead from the previous node
                    if (index > 0) {
                        highlightLinkAndArrowhead(nodes[index - 1].id, node.id, delay);
                    }

                    // Check the stopping condition
                    if (index === position) {
                        d3.select(`#node-${node.id}`)
                            .transition()
                            .duration(delay)
                            .style('fill', '#2ebbd1');

                        found = true;
                        resolve(); // Resolve the promise once the condition is met
                    }
                }, delay * index);
            });

            // Ensure promise is resolved even if no node matches
            setCheckedTimeout(() => {
                if (!found) resolve();
            }, delay * nodes.length);
        });
    }


    function highlightLinkAndArrowhead(sourceNodeId, targetNodeId, delay) {
        let linkId = `#link-${sourceNodeId}-${targetNodeId}`;
        return new Promise((resolve) => {
            svg.select(linkId)
                .transition().duration(delay)
                .style('stroke', 'orange')
                .attr('marker-end', 'url(#highlighted-arrowhead)')
                .on('end', resolve); // Resolve the promise once the transition is complete
        });
    }

    function highlightNodes(valueID, delay) {
        return new Promise((resolve) => {
            let found = false;

            nodes.forEach((node, index) => {
                let timeout = setTimeout(() => {

                    if (found) {
                        clearTimeout(timeout); // Prevent this timeout's actions if already found
                        return;
                    }

                    // Highlight the current node
                    const nodeSelection = svg.select(`#node-${node.id}`);
                    nodeSelection.transition().duration(delay).style('fill', 'orange');

                    if (index > 0) {

                        highlightLinkAndArrowhead(nodes[index - 1].id, node.id);
                    }

                    // Check the node's value and highlight accordingly
                    if (node.value === valueID) {
                        nodeSelection.transition().duration(delay).style('fill', 'green');
                        found = true;
                        resolve(); // Resolve when the target node is found
                    } else if (node.id === valueID) {

                        nodeSelection.transition().duration(delay).style('fill', 'red');
                        found = true;
                        resolve(); // Resolve when the target node is found
                    }
                }, delay * index);

                timeouts.push(timeout);
            });
        });
    }

    function highlightTailNode() {
        return new Promise((resolve) => {
            if (nodes.length === 0) {
                resolve(); // No nodes to highlight, just resolve
                return;
            }

            const tailNode = nodes[nodes.length - 1]; // Get the last node (tail node)

            svg.select(`#node-${tailNode.id}`)
                .transition().duration(1000)
                .style('fill', 'orange');

            setTimeout(() => {
                resolve(); // Resolve after the highlight completes, without changing color to green
            }, 1000);
        });
    }

    function highlightHeadNode() {
        return new Promise((resolve) => {
            if (nodes.length === 0) {
                resolve(); // No nodes to highlight, just resolve
                return;
            }

            const headNode = nodes[0];

            // Highlight only the head node in orange
            svg.select(`#node-${headNode.id}`)
                .transition().duration(1000)
                .style('fill', 'orange');

            setTimeout(() => {
                resolve(); // Resolve after the highlight completes, without changing color to green
            }, 1000);
        });
    }

    function createNewNode(node, position) {
        let targetX = nodes[position].x;
        let targetY = nodes[position].y - 65;

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

    function createTailNode(node) {
        let lastNode = nodes[nodes.length - 1];
        let targetX = lastNode.x + 100;
        let targetY = lastNode.y;

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

    function onPurposeDelay(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    function setCheckedTimeout(callback, delay) {
        const timeoutId = setTimeout(() => {
            if (!timeouts.includes(timeoutId)) {
                return; // Exit if this timeout was cleared
            }

            // Remove the timeout ID from tracking and execute the callback
            timeouts = timeouts.filter(id => id !== timeoutId);
            callback();
        }, delay);

        // Track the timeout ID
        timeouts.push(timeoutId);
        return timeoutId;
    }

    async function insertNode(value, position, delay, isStack) {

        // Highlight nodes for insertion
        await highlightNodesForInsertion(position, delay * 2);

        // Core insertion logic
        await new Promise((resolve) => {
            setCheckedTimeout(async () => {
                let newNode;

                // Determine where to insert the node
                if (position === nodes.length) {
                    newNode = createTailNode(value);
                    nodes.push(newNode);
                } else {
                    await onPurposeDelay(delay);
                    newNode = createNewNode(value, position);
                    nodes.splice(position, 0, newNode);
                }

                let prevNode = null, nextNode = null, link1Id = null, link2Id = null;

                if (position > 0) {
                    prevNode = nodes[position - 1];
                    link1Id = `link-${prevNode.id}-${newNode.id}`;
                }

                if (position < nodes.length - 1) {
                    nextNode = nodes[position + 1];
                    link2Id = `link-${newNode.id}-${nextNode.id}`;
                }

                // Update links with additional timeouts
                setCheckedTimeout(() => {
                    if (nextNode) {
                        drawLineWithArrow(newNode.x, newNode.y, nextNode.x, nextNode.y, 20, 2, link2Id, delay);
                    }

                    setCheckedTimeout(() => {
                        if (prevNode) {
                            drawLineWithArrow(prevNode.x, prevNode.y, newNode.x, newNode.y, 20, 2, link1Id, delay);
                        }

                        if (prevNode && nextNode) {
                            const existingLinkId = `link-${prevNode.id}-${nextNode.id}`;
                            svg.select(`#${existingLinkId}`).remove();
                        }

                        setCheckedTimeout(() => {
                            refreshSinglyLinkedList(isStack);

                            setCheckedTimeout(() => {
                                resetNodeColors();
                                resolve();
                            }, delay);                     
                        }, delay);
                    }, delay);
                }, delay);
            }, delay);
        });
    }

    function refreshSinglyLinkedList(isStack) {
        updateNodePositions(isStack);
        redrawLinks();
        repositionText();
    }

    async function insertNodeAtHead(value, timing, isStack) {
        return new Promise((resolve, reject) => {
            const newNode = createNewNode(value, 0);
            nodes.unshift(newNode); // Add the new node to the beginning of the list

            // Highlight delay
            setCheckedTimeout(() => {
                if (nodes.length > 1) {
                    const nextNode = nodes[1];
                    drawLineWithArrow(
                        newNode.x,
                        newNode.y,
                        nextNode.x,
                        nextNode.y,
                        20,
                        2,
                        `link-${newNode.id}-${nextNode.id}`,
                        timing.javaScriptDelay
                    );
                }

                // Color the node green
                setCheckedTimeout(() => {
                    setNodeColor('green', newNode);

                    // Refresh the list after the node is inserted
                    setCheckedTimeout(() => {
                        refreshSinglyLinkedList(isStack);

                        // Reset colors and resolve the operation
                        setCheckedTimeout(() => {
                            resetNodeColors();
                            resolve();
                        }, timing.javaScriptDelay);
                    }, timing.nodeMovementDelay);
                }, timing.highlightDelay);
            }, timing.highlightDelay);
        });
    }


    function setNodeColor(color, node) {
        svg.select(`#node-${node.id}`).
            transition().duration(500).
            style('fill', color);
    }

    async function insertNodeAtTail(value, timing) {
        return new Promise((resolve) => {
            const newNode = createTailNode(value);
            nodes.push(newNode);

            const prevNode = nodes[nodes.length - 2];

            // Draw the connecting line if a previous node exists
            if (prevNode) {
                setCheckedTimeout(() => {
                    drawLineWithArrow(
                        prevNode.x,
                        prevNode.y,
                        newNode.x,
                        newNode.y,
                        20,
                        2,
                        `link-${prevNode.id}-${newNode.id}`,
                        timing.javaScriptDelay
                    );

                    setCheckedTimeout(() => {
                        setNodeColor('green', newNode);

                        setCheckedTimeout(() => {
                            resetNodeColors();
                            resolve();
                        }, timing.javaScriptDelay);
                    }, timing.javaScriptDelay);
                }, timing.javaScriptDelay);
            } else {
                // If there is no previous node, directly color and reset
                setCheckedTimeout(() => {
                    setNodeColor('green', newNode);

                    setCheckedTimeout(() => {
                        resetNodeColors();
                        resolve();
                    }, timing.javaScriptDelay);
                }, timing.javaScriptDelay);
            }
        });
    }

    function updateNodePositions(isStack) {
        const nodeSpacing = 60;

        if (isStack) {
            nodes.forEach((node, index) => {
                node.x = width / 2;
                node.y = index * nodeSpacing + 50;
                svg.select(`#node-${node.id}`)
                    .transition()
                    .duration(500)
                    .attr("cx", node.x)
                    .attr("cy", node.y);
            });
        }
        else {
            nodes.forEach((node, index) => {
                node.x = Math.round(index * nodeSpacing + 100);
                node.y = Math.round(height / 2);
                svg.select(`#node-${node.id}`)
                    .transition()
                    .duration(500)
                    .attr("cx", node.x)
                    .attr("cy", node.y);
            });
        }
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

    async function removeNodeInSll(nodeToBeRemoved, timing, isStack) {

        await highlightNodes(nodeToBeRemoved.id, timing.highlightDelay * 2); // Double the delay for highlighting

        // Transition and then remove the node's visual elements
        svg.select(`#node-${nodeToBeRemoved.id}`)
            .transition().duration(timing.highlightDelay)
            .style('opacity', 0) // Fade out effect
        svg.select(`#node-${nodeToBeRemoved.id}`).remove();
        // Proceed with text removal after node is removed
        svg.select(`#textId-${nodeToBeRemoved.id}`)
            .transition().duration(timing.highlightDelay)
            .style('opacity', 0) // Fade out effect for text
            .on('end', () => {
                svg.select(`#textId-${nodeToBeRemoved.id}`).remove();
                // Update links if necessary and resolve when complete
                updateLinksAfterRemoval(nodeToBeRemoved);

                refreshSinglyLinkedList(isStack);
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

    function highlightLine(lineNumber) {
        const lines = document.querySelectorAll('.code-line');
        lines.forEach((line, index) => {
            line.classList.remove('highlight');
            if (index === lineNumber) {
                line.classList.add('highlight');
            }
        });
    }

    function resetHighlight() {
        const lines = document.querySelectorAll('.code-line');
        lines.forEach((line) => {
            line.classList.remove('highlight'); // Remove the highlight class to reset color
        });
    }

    window.resetCodeLineHighlight = function () {
        resetHighlight();
    };

    window.highlightLine = function (lineNumber) {
        highlightLine(lineNumber);
    };

    window.searchValueInSLL = function (value, timing) {
        resetNodeColors();
        resetLinkColors();
        highlightNodes(value, timing.highlightDelay * 2); // Double the delay for highlighting
    };

    window.insertAtInSLL = function (value, selectedIndex, timing, isStack = false) {
        insertNode(value, selectedIndex, timing, isStack);
    };

    window.removeValueInSll = function (nodeToBeRemoved, timing, isStack) {
        resetNodeColors();
        resetLinkColors();
        return removeNodeInSll(nodeToBeRemoved, timing, isStack); // Return the promise
    };

    window.highlightTail = function () {
        resetNodeColors();
        resetLinkColors();
        highlightTailNode();
    }

    window.highlightHead = function () {
        resetNodeColors();
        resetLinkColors();
        highlightHeadNode();
    }

    window.insertTailInSll = function (value, timing) {
        resetNodeColors();
        resetLinkColors();
        insertNodeAtTail(value, timing);
    }

    window.insertHeadInSll = function (value, timing, isStack) {
        insertNodeAtHead(value, timing, isStack);
    }

    window.resetSllColours = function () {
        resetNodeColors();
        resetLinkColors();
    }
})();