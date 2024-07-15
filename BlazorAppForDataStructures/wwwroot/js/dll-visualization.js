(function () {
    let svg, nodes;
    let margin = { top: 20, right: 30, bottom: 40, left: 50 };

    function adjustLineEndpoints(x1, y1, x2, y2, radius, strokeWidth, offsetX, offsetY) {
        const angle = Math.atan2(y2 - y1, x2 - x1);
        const effectiveRadius = radius + strokeWidth;
        return {
            startX: x1 + Math.cos(angle) * effectiveRadius + offsetX,
            startY: y1 + Math.sin(angle) * effectiveRadius + offsetY,
            endX: x2 - Math.cos(angle) * effectiveRadius + offsetX,
            endY: y2 - Math.sin(angle) * effectiveRadius + offsetY
        };
    }

    function drawLineWithArrow(startX, startY, endX, endY, radius, strokeWidth, id, direction) {
        let offsetX = 0, offsetY = 0;

        if (direction === 'right') {
            offsetY = -10; // Move the line up for the right direction
        } else if (direction === 'left') {
            offsetY = 10; // Move the line down for the left direction
        }

        const adjustedPoints = adjustLineEndpoints(startX, startY, endX, endY, radius, strokeWidth, offsetX, offsetY);

        // Calculate the total length of the line
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
            .duration(1000)
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
                drawLineWithArrow(node.x, node.y, nextNode.x, nextNode.y, 20, 2, `link-${node.id}-${nextNode.id}`, 'right');
                drawLineWithArrow(nextNode.x, nextNode.y, node.x, node.y, 20, 2, `link-${nextNode.id}-${node.id}`, 'left');
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
                            .style('fill', 'red');
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
        highlightNodes(value, delay);
    };

    window.drawDoublyLinkedList = drawDoublyLinkedList;
})();
