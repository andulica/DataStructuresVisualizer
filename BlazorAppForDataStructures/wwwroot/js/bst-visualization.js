window.drawBST = function (data) {

    // Set the dimensions and margins of the diagram
    const margin = { top: 20, right: 90, bottom: 30, left: 90 },
        width = 960 - margin.left - margin.right,
        height = 500 - margin.top - margin.bottom;

    // Remove any existing SVG to avoid overlaps
    d3.select("#bst-display").select("svg").remove();

    // Append the svg object to the body of the page
    // Appends a 'group' element to 'svg'
    // Moves the 'group' element to the top left margin
    const svg = d3.select("#bst-display").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", "translate("
            + margin.left + "," + margin.top + ")");

    // Create the tree layout
    const treemap = d3.tree().size([width, height]);
    const root = d3.hierarchy(data);
    treemap(root);

    // Add nodes and links to the SVG
    svg.selectAll(".node")
        .data(root.descendants())
        .enter().append("g")
        .attr("class", d => "node" + (d.children ? " node--internal" : " node--leaf"))
        .attr("transform", d => "translate(" + d.x + "," + d.y + ")")
        .append("circle")
        .attr("r", 10);

    svg.selectAll(".link")
        .data(root.links())
        .enter().append("path")
        .attr("class", "link")
        .attr("d", d3.linkVertical()
            .x(d => d.x)
            .y(d => d.y));

    // Add labels to nodes
    svg.selectAll(".node")
        .append("text")
        .attr("dy", ".35em")
        .attr("x", d => d.children ? -13 : 13)
        .style("text-anchor", d => d.children ? "end" : "start")
        .text(d => d.data.name);
}