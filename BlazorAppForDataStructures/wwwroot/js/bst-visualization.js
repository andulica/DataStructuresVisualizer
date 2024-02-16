let root, svg, treemap, width, height;
let margin = { top: 20, right: 90, bottom: 30, left: 90 };
window.drawBST = function (data) {

    console.log("Received data in drawBST:", data);

    // Set the dimensions and margins of the diagram
    margin,
        width = 700 - margin.left - margin.right,
        height = 500 - margin.top - margin.bottom;

    // Remove any existing SVG to avoid overlaps
    d3.select("#bst-display").select("svg").remove();

    // Append the svg object to the body of the page
    // Append the rwerwersvg object to the body of the page
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
     root = d3.hierarchy(data, function (d) {
        let children = [];
        if (d.left) children.push(d.left);
        if (d.right) children.push(d.right);
        return children.length ? children : null;
    });
    root.x0 = height / 2;  // Center the root node vertically
    root.y0 = 0;           // Start the root node at the left of the screen
    updateTree(root);      // This will compute the positions and draw the nodes#
    window.searchValue = function (value) {

        searchRecursive(data, value);
    };
}

function updateTree(source) {

    const nodeDuration = 750;
    const linkDuration = 750;
    // Assigns the x and y position for the nodes
    let treeData = treemap(root); 

    // Compute the new tree layout
    let nodes = treeData.descendants(),
        links = treeData.descendants().slice(1);

    // Normalize for fixed-depth
    nodes.forEach(d => { d.y = d.depth * 50; });


    // ****************** Nodes section ***************************
    // Update the nodes...

    let node = svg.selectAll('g.node') 
        .data(nodes, d => d.data.id); // Use the global ID from the backend
        
    // Enter any new modes at the parent's previous position
    let nodeEnter = node.enter().append('g')
        .attr('id', d => `node-${d.data.id}`) // Set element ID using global ID
        .attr('transform', `translate(${source.x0},${source.y0})`); // source is the parent node

    // Add Circle for the nodes
    nodeEnter.append('circle')
        .attr('class', 'node')
        .attr('r', 20) // Adjust radius as needed
        .style('fill', '#00F1D4') 
        .attr('stroke', 'black')
        .attr('stroke-width', 1.5);

    // Add labels for the nodes
    nodeEnter.append('text')
        .attr('text-anchor', 'middle')
        .text(d => d.data.name); 

    // UPDATE
    let nodeUpdate = nodeEnter.merge(node);

    // Transition to the proper position for the node
    nodeUpdate.transition()
        .duration(nodeDuration)
        .attr('transform', d => `translate(${d.x},${d.y})`);

    // ****************** Links section ***************************
    // Update the links...
    let link = svg.selectAll('path.link')
        .data(links, d => d.id);

    // Enter any new links at the parent's previous position
    let linkEnter = link.enter().insert('path', 'g')
        .attr('d', d => {
            let o = { x: source.x0, y: source.y0 };
            return diagonal(o, o);
        });

    // UPDATE
    let linkUpdate = linkEnter.merge(link);

    
    // Transition back to the parent element position
    linkUpdate.transition()
        .duration(linkDuration)
        .attr('stroke', 'black')
        .attr('stroke-width', 2.5)
        .attr('d', d => diagonal(d.parent, d))

    // Creates a straight diagonal path from parent to the child nodes
    function diagonal(s, d) {
        return `M ${s.x} ${s.y} L ${d.x} ${d.y}`;
    }
}

function searchRecursive(node, value) {
    if (!node) return null;

    // Select the circle within the node group
    const circleElement = document.querySelector(`#node-${node.id} circle`);
    if (circleElement) {
        circleElement.style.transition = 'fill 0.5s';
        circleElement.style.fill = 'yellow'; // Highlight the current node as being searched
    }

    const nodeValue = parseInt(node.name, 10);
    if (nodeValue === value) {
        if (circleElement) {
            circleElement.style.fill = 'red'; // Highlight the found node
        }
        return node;
    }

    // Traverse to the left or right child based on the comparison
    if (value < nodeValue && node.left) {
        return searchRecursive(node.left, value);
    } else if (value > nodeValue && node.right) {
        return searchRecursive(node.right, value);
    }
}



