(function () {
    let svg, nodes;
    let margin = { top: 20, right: 30, bottom: 40, left: 50 };
    const delayDrawLinks = 1000;
    const delayForVisual = 1000;
    let timeouts = [];

    window.DllCancelVisuals = function () {
        clearAllTimeouts(timeouts)
    }

    // Helper function to clear all timeouts
    function clearAllTimeouts(timeouts) {
        timeouts.forEach(timeoutId => clearTimeout(timeoutId));
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

    function adjustLineEndpoints(x1, y1, x2, y2, radius, strokeWidth, direction) {
        const gap = 10;
        const effectiveRadius = radius + strokeWidth / 2;

        let startX, startY, endX, endY;

        const angle = Math.atan2(y2 - y1, x2 - x1);
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
                drawLineWithArrow(node.x, node.y, nextNode.x, nextNode.y, 20, 2, `link-${node.id}-${nextNode.id}`, 'right', delayDrawLinks);
                drawLineWithArrow(nextNode.x, nextNode.y, node.x, node.y, 20, 2, `link-${nextNode.id}-${node.id}`, 'left', delayDrawLinks);
            }
        });
    };

    async function highlightNodes(valueID, delay) {
        return new Promise((resolve) => {
            let found = false;

            nodes.forEach((node, index) => {
                setCheckedTimeout(() => {
                    if (found) return; // Skip further processing if the target node is already found

                    svg.select(`#node-${node.id}`)
                        .transition()
                        .duration(delay)
                        .style('fill', 'orange');

                    if (index > 0) {
                        highlightLinkAndArrowhead(nodes[index - 1].id, node.id, 'right', delay);
                    }

                    if (node.value === valueID) {
                        svg.select(`#node-${node.id}`)
                            .transition()
                            .duration(delay)
                            .style('fill', 'green');
                        found = true;
                        resolve(); // Resolve when the target node is found
                    }

                    if (node.id === valueID) {
                        svg.select(`#node-${node.id}`)
                            .transition()
                            .duration(delay)
                            .style('fill', 'green');
                        found = true;
                        resolve(); // Resolve when the target node is found
                    }
                }, delay * index);
            });

            setCheckedTimeout(() => {
                if (!found) resolve(); // Resolve even if no matching node is found
            }, delay * nodes.length);
        });
    }

    function onPurposeDelay(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    async function insertNode(value, position, delay) {

        await highlightNodesForInsertion(position, delay * 2);

        await onPurposeDelay(delay);

        await new Promise((resolve) => {
            setCheckedTimeout(async () => {
                let newNode;

                if (position === nodes.length) {
                    // Handle insertion at the tail
                    newNode = createTailNode(value);
                    nodes.push(newNode);

                } else {
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

                // Draw new links with delays using setCheckedTimeout
                await new Promise((innerResolve) => {
                    setCheckedTimeout(() => {

                        if (nextNode) {
                            drawLineWithArrow(newNode.x, newNode.y, nextNode.x, nextNode.y, 20, 2, link3Id, 'right', delay);
                            drawLineWithArrow(nextNode.x, nextNode.y, newNode.x, newNode.y, 20, 2, link4Id, 'left', delay);
                        }

                        setCheckedTimeout(() => {
                            if (prevNode) {
                                drawLineWithArrow(prevNode.x, prevNode.y, newNode.x, newNode.y, 20, 2, link1Id, 'right', delay);
                                drawLineWithArrow(newNode.x, newNode.y, prevNode.x, prevNode.y, 20, 2, link2Id, 'left', delay);
                            }

                            if (prevNode && nextNode) {
                                const existingLinkId1 = `link-${prevNode.id}-${nextNode.id}`;
                                const existingLinkId2 = `link-${nextNode.id}-${prevNode.id}`;
                                // Remove existing links
                                svg.select(`#${existingLinkId1}`).remove();
                                svg.select(`#${existingLinkId2}`).remove();
                            }

                            setCheckedTimeout(() => {
                                refreshDoublyLinkedList();
                                setCheckedTimeout(() => {
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
            let found = false;

            nodes.forEach((node, index) => {
                setCheckedTimeout(() => {

                    if (found) return;

                    svg.select(`#node-${node.id}`).transition().duration(delay).style('fill', 'orange');

                    if (index > 0) {
                        highlightLinkAndArrowhead(nodes[index - 1].id, node.id, 'right', delay);
                    }

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

            // Final fallback to resolve the promise if no match is found
            setCheckedTimeout(() => {
                if (!found) resolve();
            }, delay * nodes.length);
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

        svg.append("circle")
            .attr("id", `node-${node.id}`)
            .attr("class", "node")
            .attr("cx", targetX)
            .attr("cy", targetY)
            .attr("r", 20)
            .style("fill", "green")
            .style("stroke", "black")
            .style("stroke-width", 2);

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
        return new Promise((resolve) => {
            const newNode = createTailNode(value);
            nodes.push(newNode);

            const prevNode = nodes[nodes.length - 2];

            if (prevNode) {
                setCheckedTimeout(() => {
                    drawLineWithArrow(prevNode.x, prevNode.y, newNode.x, newNode.y, 20, 2, `link-${prevNode.id}-${newNode.id}`, 'right', delay);

                    setCheckedTimeout(() => {
                        drawLineWithArrow(newNode.x,newNode.y,prevNode.x,prevNode.y,20,2,`link-${newNode.id}-${prevNode.id}`,'left',delay);

                        setCheckedTimeout(() => {
                            resetNodeColors();
                            resolve();
                        }, delay);
                    }, delay);
                }, delay);
            } else {
                // If no previous node, directly reset colors and resolve
                setCheckedTimeout(() => {
                    resetNodeColors();
                    resolve();
                }, delay);
            }
        });
    }

    async function removeNode(nodeToBeRemoved, delay) {
        return new Promise((resolve) => {
            highlightNodes(nodeToBeRemoved.id, delay * 2).then(() => {
                setCheckedTimeout(() => {
                    // Transition and then remove the node's visual elements
                    svg.select(`#node-${nodeToBeRemoved.id}`)
                        .transition()
                        .duration(delay)
                        .style('opacity', 0)
                        .on('end', () => {
                            svg.select(`#node-${nodeToBeRemoved.id}`).remove();

                            setCheckedTimeout(() => {
                                svg.select(`#textId-${nodeToBeRemoved.id}`)
                                    .transition()
                                    .duration(delay)
                                    .style('opacity', 0)
                                    .on('end', () => {
                                        svg.select(`#textId-${nodeToBeRemoved.id}`).remove();

                                        // Update links and refresh the list
                                        setCheckedTimeout(() => {
                                            updateLinksAfterRemoval(nodeToBeRemoved);
                                            refreshDoublyLinkedList();

                                            // Resolve the promise after all actions are complete
                                            resolve();
                                        }, delay);
                                    });
                            }, delay);
                        });
                }, delay);
            });
        });
    }

    function updateLinksAfterRemoval(nodeToBeRemoved) {
        let nodeIndex = nodes.findIndex(node => node.id === nodeToBeRemoved.id);
        nodes = nodes.filter(node => node.id !== nodeToBeRemoved.id);

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

    function resetLinkColors() {
        svg.selectAll(".link")
            .transition().duration(500)
            .style('stroke', '#000')
            .attr('marker-end', 'url(#right-arrowhead)');

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

    function createHeadNode(node) {
        let headNode = nodes[0];
        let targetX = headNode.x;
        let targetY = headNode.y - 65

        // Create the new node circle
        svg.append("circle")
            .attr("id", `node-${node.id}`)
            .attr("class", "node")
            .attr("cx", targetX)
            .attr("cy", targetY)
            .attr("r", 20)
            .style("fill", "orange")
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
        }
    }

    function setColoursForNode(node, colour, delay) {
        svg.select(`#node-${node.id}`)
            .transition()
            .duration(delay)
            .style('fill', colour);
    }

    async function insertNodeAtHead(value, delay) {
        return new Promise((resolve) => {
            const newNode = createHeadNode(value);

            const headNode = nodes[0];

            if (headNode) {
                setCheckedTimeout(() => {
                    drawLineWithArrow(newNode.x, newNode.y, headNode.x, headNode.y, 20, 2, `link-${headNode.id}-${newNode.id}`, 'right', delay);

                    setCheckedTimeout(() => {
                        drawLineWithArrow(headNode.x, headNode.y, newNode.x, newNode.y, 20, 2, `link-${newNode.id}-${headNode.id}`, 'left', delay);

                        setCheckedTimeout(() => {
                            nodes.splice(0, 0, newNode);
                            setColoursForNode(newNode, 'green', delay);

                            setCheckedTimeout(() => {

                                // Ensure refresh happens after the node is fully inserted
                                refreshDoublyLinkedList();
                                setCheckedTimeout(() => {
                                    resetNodeColors();
                                    resolve(); // Explicitly resolve the Promise here
                                }, delay);
                            }, delay);
                        }, delay);
                    }, delay);
                }, delay);
            } else {
                // No headNode exists; directly update and refresh
                nodes.splice(0, 0, newNode);
                setColoursForNode(newNode, 'green', delay);

                refreshDoublyLinkedList();
                setCheckedTimeout(() => {
                    resetNodeColors();
                    resolve(); // Explicitly resolve the Promise here
                }, delay);
            }
        });
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
        resetNodeColors();
        resetLinkColors();
        highlightNodes(value, delay * 2); //double the delay for search
    };

    window.insertHeadInDll = async function (value, delay) {
        resetNodeColors();
        resetLinkColors()
        insertNodeAtHead(value, delay)
    };
    window.insertAtInDll = async function (dllData, position, delay) {
        resetNodeColors();
        resetLinkColors()
        insertNode(dllData, position, delay);
    };

    window.insertTailInDll = function (value, delay) {
        resetNodeColors();
        resetLinkColors()
        insertNodeAtTail(value, delay);
    }

    window.removeValueInDll = async function (value, delay) {
        resetNodeColors();
        resetLinkColors();
        removeNode(value, delay);
    }

    window.drawDoublyLinkedList = drawDoublyLinkedList;
})();
