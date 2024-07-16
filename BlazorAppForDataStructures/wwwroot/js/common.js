
window.resetNodeColors = function (svg, nodes) {
    nodes.forEach(node => {
        svg.select(`#node-${node.id}`)
            .transition()
            .duration(500)
            .style('fill', 'skyblue');
    });
}

window.resetLinkColors = function (svg) {
    svg.selectAll(".link")
        .transition().duration(500)
        .style('stroke', '#000')
        .attr('marker-end', 'url(#arrowhead)');
}
