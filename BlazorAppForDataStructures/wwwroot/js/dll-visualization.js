(function () {
    let svg, nodes;
    let margin = { top: 20, right: 30, bottom: 40, left: 50 };

    function adjustLineEndpoints(x1, y1, x2, y2, radius, strokeWidth, direction) {
        const gap = 10;
        const effectiveRadius = radius + strokeWidth / 2;

        let startX, startY, endX, endY;

        // Calculate the angle
        const angle = Math.atan2(y2 - y1, x2 - x1);
        // Calculate the perpendicular angle
        const perpendicularAngle = angle + Math.PI / 2;
        const offsetX = Math.cos(perpendicularAngle) * gap / 2;
        const offsetY = Math.sin(perpendicularAngle) * gap / 2;

        // Horizontal link
        if (Math.abs(y2 - y1) < 0.1) {
            if (direction === 'right') {
                startX = x1 + effectiveRadius;
                startY = y1 + offsetY - 18;
                endX = x2 - effectiveRadius;
                endY = y2 + offsetY - 18;
            } else {
                startX = x1 - effectiveRadius;
                startY = y1 - offsetY;
                endX = x2 + effectiveRadius;
                endY = y2 - offsetY;
            }
        }
        // Vertical link
        else if (Math.abs(x2 - x1) < 0.1) {
            if (direction === 'right') {
                startX = x1 + offsetX + 18;
                startY = y1 + effectiveRadius;
                endX = x2 + offsetX + 18;
                endY = y2 - effectiveRadius;
            } else {
                startX = x1 - offsetX - 5;
                startY = y1 - effectiveRadius;
                endX = x2 - offsetX - 5;
                endY = y2 + effectiveRadius;
            }
        }
        // Diagonal link
        else {
            if (direction === 'right') {
                startX = x1 + Math.cos(angle) * effectiveRadius + offsetX + 4;
                startY = y1 + Math.sin(angle) * effectiveRadius + offsetY - 2;
                endX = x2 - Math.cos(angle) * effectiveRadius + offsetX - 5;
                endY = y2 - Math.sin(angle) * effectiveRadius + offsetY;
            } else {
                startX = x1 + Math.cos(angle) * effectiveRadius - offsetX - 5;
                startY = y1 + Math.sin(angle) * effectiveRadius - offsetY - 15;
                endX = x2 - Math.cos(angle) * effectiveRadius - offsetX - 5;
                endY = y2 - Math.sin(angle) * effectiveRadius - offsetY - 11;
            }
        }

        return { startX, startY, endX, endY };
    }

    function drawLineWithArrow(startX, startY, endX, endY, radius, strokeWidth, id, direction, delay) {
        const adjustedPoints = adjustLineEndpoints(startX, startY, endX, endY, radius, strokeWidth, direction);

        const lineLength = Math.sqrt(Math.pow(adjustedPoints.endX - adjustedPoints.startX, 2) + Math.pow(adjustedPoints.endY - adjustedPoints.startY, 2));
        const line = svg.append('path')
            .attr('class', 'link')
            .attr('d', `M${adjustedPoints.startX},${adjustedPoints.startY} L${adjustedPoints.endX},${adjustedPoints.endY}`)
            .attr('stroke', '#000')
            .attr('stroke-width', strokeWidth)
            .attr('fill', 'none')
            .attr('marker-end', direction === 'right' ? 'url(#right-arrowhead)' : 'url(#left-arrowhead)')
            .attr('stroke-dasharray', lineLength)
            .attr('stroke-dashoffset', lineLength)
            .transition()
            .duration(delay)
            .attr('stroke-dashoffset', 0);

        if (id) {
            line.attr('id', id);
        }

        return line;
    }


    window.drawDoublyLinkedList = function (doublyLinkedList) {
        margin, width = 700 - margin.left - margin.right, height = 250 - margin.top - margin.bottom;

        d3.select("#dll-display").select("svg").remove();
        svg = d3.select("#dll-display").append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", `translate(${margin.left}, ${margin.top})`);

        svg.append('defs').append('marker')
            .attr('id', 'right-arrowhead')
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
            .attr('id', 'left-arrowhead')
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
            .attr('id', 'highlighted-right-arrowhead')
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

        svg.append('defs').append('marker')
            .attr('id', 'highlighted-left-arrowhead')
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

        nodes = doublyLinkedList.map((d, i) => ({
            ...d,
            x: i * 100 + 50,
            y: height / 2
        }));

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
                .attr("y", node.y + 5)
                .text(node.value)
                .attr("text-anchor", "middle")
                .style("fill", "black");
        });

        nodes.forEach((node, i) => {
            if (i < nodes.length - 1) {
                let nextNode = nodes[i + 1];
                drawLineWithArrow(node.x, node.y, nextNode.x, nextNode.y, 20, 2, `link-${node.id}-${nextNode.id}`, 'right', 1000);
                drawLineWithArrow(nextNode.x, nextNode.y, node.x, node.y, 20, 2, `link-${nextNode.id}-${node.id}`, 'left', 1000);
            }
        });
    };

    function highlightNodes(value, delay) {
        return new Promise((resolve) => {
            let timeouts = [];
            let found = false;

            nodes.forEach((node, index) => {
                let timeout = setTimeout(() => {
                    if (found) {
                        clearTimeout(timeout);
                        return;
                    }

                    svg.select(`#node-${node.id}`)
                        .transition().duration(delay)
                        .style('fill', 'orange');

                    if (index > 0) {
                        highlightLinkAndArrowhead(nodes[index - 1].id, node.id, 'right', delay);
                    }

                    if (node.value === value) {
                        svg.select(`#node-${node.id}`)
                            .transition().duration(500)
                            .style('fill', 'green');
                        found = true;
                        clearTimeouts(timeouts);
                        resolve();
                    }
                }, delay * index);

                timeouts.push(timeout);
            });

            let finalTimeout = setTimeout(() => {
                if (!found) resolve();
            }, 1000 * nodes.length);
            timeouts.push(finalTimeout);
        });
    }
    async function insertNode(value, position, delay) {
        await highlightNodesForInsertion(position, delay); // Highlight nodes up to the position

        await new Promise((resolve) => {
            setTimeout(async () => {
                let newNode;
                if (position === nodes.length) {
                    // Handle insertion at the tail
                    newNode = createTailNode(value);
                    nodes.push(newNode);
                } else {
                    // Handle insertion at the head or specified position
                    newNode = createNewNode(value, position);
                    nodes.splice(position, 0, newNode);
                }

                let prevNode, nextNode, link1Id, link2Id, link3Id, link4Id;

                if (position > 0) {
                    prevNode = nodes[position - 1];
                    link1Id = `link-${prevNode.id}-${newNode.id}`;
                    link2Id = `link-${newNode.id}-${prevNode.id}`;
                }

                if (position < nodes.length - 1) {
                    nextNode = nodes[position + 1];
                    link3Id = `link-${newNode.id}-${nextNode.id}`;
                    link4Id = `link-${nextNode.id}-${newNode.id}`;
                }

                // Draw new links with a delay
                await new Promise((innerResolve) => {
                    setTimeout(() => {
                        const isInsertMiddle = position < nodes.length - 1;

                        if (nextNode) {
                            drawLineWithArrow(newNode.x, newNode.y, nextNode.x, nextNode.y, 20, 2, link3Id, 'right', delay);
                            drawLineWithArrow(nextNode.x, nextNode.y, newNode.x, newNode.y, 20, 2, link4Id, 'left', delay);
                        }

                        setTimeout(() => {
                            if (prevNode) {
                                drawLineWithArrow(prevNode.x, prevNode.y, newNode.x, newNode.y, 20, 2, link1Id, 'right', delay);
                                drawLineWithArrow(newNode.x, newNode.y, prevNode.x, prevNode.y, 20, 2, link2Id, 'left', delay);
                            }

                            if (prevNode && nextNode) {
                                const existingLinkId1 = `link-${prevNode.id}-${nextNode.id}`;
                                const existingLinkId2 = `link-${nextNode.id}-${prevNode.id}`;
                                // Remove the existing link before creating new links
                                svg.select(`#${existingLinkId1}`).remove();
                                svg.select(`#${existingLinkId2}`).remove();
                            }

                            setTimeout(() => {
                                // Update the positions and redraw the links
                                refreshDoublyLinkedList();

                                // Reset node colors after a delay
                                setTimeout(() => {
                                    resetNodeColors();
                                    innerResolve();
                                }, delay);
                            }, delay);
                        }, delay);
                    }, delay);
                });

                resolve();
            }, delay);
        });
    }

    async function highlightNodesForInsertion(position, delay) {
        return new Promise((resolve) => {
            let timeouts = []; // Store timeout IDs for potential clearing
            let found = false;

            nodes.forEach((node, index) => {
                let timeoutId = setTimeout(() => {
                    if (found) return; // Stop further highlighting once the condition is met

                    // Highlight the current node
                    svg.select(`#node-${node.id}`).transition().duration(delay).style('fill', 'orange');

                    // Highlight the link and the arrowhead from the previous node
                    if (index > 0) {
                        highlightLinkAndArrowhead(nodes[index - 1].id, node.id, 'right', delay);
                    }

                    // Check the stopping condition
                    if (index === position) {
                        found = true;
                        clearTimeouts(timeouts);
                        resolve(); // Resolve the promise once the condition is met
                    }
                }, delay * index);

                timeouts.push(timeoutId);
            });

            // Ensure promise is resolved even if no node matches
            let finalTimeout = setTimeout(() => {
                if (!found) resolve();
            }, delay * nodes.length);
            timeouts.push(finalTimeout);

            // Function to clear all timeouts
            function clearTimeouts(timeouts) {
                timeouts.forEach(timeoutId => clearTimeout(timeoutId));
            }
        });
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
        svg.selectAll('.link').remove();

        nodes.forEach((node, index) => {
            if (index < nodes.length - 1) {
                let nextNode = nodes[index + 1];
                drawLineWithArrow(node.x, node.y, nextNode.x, nextNode.y, 20, 2, `link-${node.id}-${nextNode.id}`, 'right', 1000);
                drawLineWithArrow(nextNode.x, nextNode.y, node.x, node.y, 20, 2, `link-${nextNode.id}-${node.id}`, 'left', 1000);
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

    function refreshDoublyLinkedList() {
        updateNodePositions();
        redrawLinks();
        repositionText();
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

    async function insertNodeAtTail(value, delay) {

        setTimeout(() => {
            const newNode = createTailNode(value);
            nodes.push(newNode);

            const prevNode = nodes[nodes.length - 2];

            if (prevNode) {
                setTimeout(() => {
                    drawLineWithArrow(prevNode.x, prevNode.y, newNode.x, newNode.y, 20, 2, `link-${prevNode.id}-${newNode.id}`, 'right', delay);
                    drawLineWithArrow(newNode.x, newNode.y, prevNode.x, prevNode.y, 20, 2, `link-${newNode.id}-${prevNode.id}`, 'left', delay);
                }, 1000);
            }
            setTimeout(() => {
                resetNodeColors();
            }, 1000);

        }, 1000);
    }

    function removeNode(nodeToBeRemoved, delay) {
        return new Promise((resolve) => {
            highlightNodes(nodeToBeRemoved.value, delay).then(() => {
                setTimeout(() => {
                    // Transition and then remove the node's visual elements
                    svg.select(`#node-${nodeToBeRemoved.id}`)
                        .transition().duration(delay)
                        .style('opacity', 0) // Fade out effect
                        .on('end', () => {
                            svg.select(`#node-${nodeToBeRemoved.id}`).remove();
                            // Proceed with text removal after node is removed
                            svg.select(`#textId-${nodeToBeRemoved.id}`)
                                .transition().duration(delay)
                                .style('opacity', 0) // Fade out effect for text
                                .on('end', () => {
                                    svg.select(`#textId-${nodeToBeRemoved.id}`).remove();

                                    // Update links if necessary and resolve when complete
                                    updateLinksAfterRemoval(nodeToBeRemoved);

                                    refreshDoublyLinkedList();

                                    resolve(); // Ensure all transitions have time to complete
                                });
                        });
                }, delay);
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
            svg.select(`#link-${nodeToBeRemoved.id}-${prevNode.id}`).remove();

            drawLineWithArrow(prevNode.x, prevNode.y, nextNode.x, nextNode.y, 20, 2, `link-${prevNode.id}-${nextNode.id}`, 'right');
            drawLineWithArrow(nextNode.x, nextNode.y, prevNode.x, prevNode.y, 20, 2, `link-${prevNode.id}-${nextNode.id}`, 'left');

        } else if (nodeIndex > 0) {
            // Removing the last node
            svg.select(`#link-${nodes[nodeIndex - 1].id}-${nodeToBeRemoved.id}`).remove();
            svg.select(`#link-${nodeToBeRemoved.id}-${nodes[nodeIndex - 1].id}`).remove();
        } else if (nodes.length) {
            // Removing the first node and there are other nodes
            svg.select(`#link-${nodeToBeRemoved.id}-${nodes[0].id}`).remove();
            svg.select(`#link-${nodes[0].id}-${nodeToBeRemoved.id}`).remove();
        }
    }

    function resetNodeColors() {
        nodes.forEach(node => {
            svg.select(`#node-${node.id}`)
                .transition()
                .duration(500)
                .style('fill', 'skyblue');
        });
    }

    function resetLinkColors(svg) {
        // Reset right arrowhead links
        svg.selectAll(".link")
            .transition().duration(500)
            .style('stroke', '#000')
            .attr('marker-end', 'url(#right-arrowhead)');

        // Reset left arrowhead links
        svg.selectAll(".link")
            .transition().duration(500)
            .style('stroke', '#000')
            .attr('marker-end', 'url(#left-arrowhead)');
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

    function highlightLinkAndArrowhead(sourceNodeId, targetNodeId, direction, delay) {
        let linkId = `#link-${sourceNodeId}-${targetNodeId}`;
        return new Promise((resolve) => {
            svg.select(linkId)
                .transition().duration(delay)
                .style('stroke', 'orange')
                .attr('marker-end', `url(#highlighted-${direction}-arrowhead)`)
                .on('end', resolve);
        });
    }

    window.highlightLine = function (lineNumber) {
        const lines = document.querySelectorAll('.code-line');
        lines.forEach((line, index) => {
            line.classList.remove('highlight');
            if (index === lineNumber) {
                line.classList.add('highlight');
            }
        });
    };

    window.searchValueInDLL = function (value, delay) {
        refreshDoublyLinkedList();
        highlightNodes(value, delay);
    };

    window.insertAtInDll = async function (dllData, position, delay) {
        refreshDoublyLinkedList();
        await insertNode(dllData, position, delay);
    };

    window.insertTailInDll = function (value, delay) {
        refreshDoublyLinkedList();
        insertNodeAtTail(value, delay);
    }

    window.removeValueInDll = function (value, delay) {
        refreshDoublyLinkedList();
        removeNode(value, delay);
    }

    window.drawDoublyLinkedList = drawDoublyLinkedList;
})();
