let root, svg, treemap;
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
    svg = d3.select("#bst-display").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", "translate("
            + margin.left + "," + margin.top + ")");

    // Create the tree layout
    treemap = d3.tree().size([height, width]);

    // Initialize the global root variable and update the tree
    root = d3.hierarchy(data);
    root.x0 = height / 2;  // Center the root node vertically
    root.y0 = 0;           // Start the root node at the left of the screen
    updateTree(root);      // This will compute the positions and draw the nodes
}

function updateTree(source) {

    const duration = 750;
    // Assigns the x and y position for the nodes
    let treeData = treemap(root);

    // Compute the new tree layout
    let nodes = treeData.descendants(),
        links = treeData.descendants().slice(1);

    // Normalize for fixed-depth
    nodes.forEach(d => { d.y = d.depth * 180; });

    // ****************** Nodes section ***************************
    // Update the nodes...

    let i = 0;
    let node = svg.selectAll('g.node')
        .data(nodes, d => d.id || (d.id = ++i));

    // Enter any new modes at the parent's previous position
    let nodeEnter = node.enter().append('g')
        .attr('class', 'node')
        .attr('transform', d => `translate(${source.y0},${source.x0})`) // source is the parent node

    // Add Circle for the nodes
    nodeEnter.append('circle')
        .attr('class', 'node')


    // Add labels for the nodes
    nodeEnter.append('text')
        .attr('text-anchor', 'middle')
        .text(d => d.data.name);

    // UPDATE
    let nodeUpdate = nodeEnter.merge(node);

    // Transition to the proper position for the node
    nodeUpdate.transition()
        .duration(duration)
        .attr('transform', d => `translate(${d.y},${d.x})`);

    // Update the node attributes and style
    nodeUpdate.select('circle.node')
        .attr('r', 20)
        .style('fill', '#00F1D4')

    // ****************** Links section ***************************
    // Update the links...
    let link = svg.selectAll('path.link')
        .data(links, d => d.id);

    // Enter any new links at the parent's previous position
    let linkEnter = link.enter().insert('path', 'g')
        .attr('class', 'link')
        .attr('d', d => {
            let o = { x: source.x0, y: source.y0 };
            return diagonal(o, o);
        });

    // UPDATE
    let linkUpdate = linkEnter.merge(link);

    // Transition back to the parent element position
    linkUpdate.transition()
        .duration(duration)
        .attr('stroke', 'black')
        .attr('d', d => diagonal(d, d.parent));

    // Store the old positions for transition
    nodes.forEach(d => {
        d.x0 = d.x;
        d.y0 = d.y;
    });

    // Creates a curved (diagonal) path from parent to the child nodes
    function diagonal(s, d) {
        let path = `M ${s.y} ${s.x} L ${d.y} ${d.x} Z`;            
        return path;
    }
}

function findParent(node, newValue) {
    // Ensure we're accessing the 'name' property as in your D3 format
    if (newValue < node.name) {
        // Go left
        if (!node.children || node.children.length === 0 || !node.children[0]) {
            return node; // Found parent
        } else {
            return findParent(node.children[0], newValue); // Assuming left child is first
        }
    } else {
        // Go right
        if (!node.children || node.children.length < 2 || !node.children[1]) {
            return node; // Found parent
        } else {
            return findParent(node.children[1], newValue); // Assuming right child is second
        }
    }
}

function addNode(newValue) {
    let parentNode = findParent(root.data, newValue);
    let newNode = { name: newValue, children: [] }; // Adjust based on your data structure

    if (!parentNode.children) {
        parentNode.children = [];
    }
    parentNode.children.push(newNode);

    // Update the D3 hierarchy with the new structure
    root = d3.hierarchy(root.data);
    treemap(root);
    updateTree(root);
}

window.bstVisualization = {
    addNode: function (newValue) {
        addNode(newValue);
    }
};