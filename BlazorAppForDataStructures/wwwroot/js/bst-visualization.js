let root, svg, treemap, width, height, currentTreeNode; // Declare global variables
let margin = { top: 20, right: 90, bottom: 30, left: 90 }; // Set the margins for the tree diagram
window.drawBST = function (treeRootNode) {
    
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

    // Assigns parent, children, height, depth
     root = d3.hierarchy(treeRootNode, function (d) {
        let children = [];
        if (d.left) children.push(d.left);
        if (d.right) children.push(d.right);
        return children.length ? children : null;
    });
    root.x0 = height / 2;  // Center the root node vertically
    root.y0 = 0;           // Start the root node at the left of the screen
    updateTree(root);      // This will compute the positions and draw the nodes#
    currentTreeNode = treeRootNode; // Store the current tree data
}

window.searchValue = function (value) {

    searchRecursive(currentTreeNode, value, null); 
};

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
        
    // Enter any new nodes at the parent's previous position
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
    let linkEnter = link.enter().insert('path', "g")
        .attr("class", "link")
        .attr("id", d => `link-${d.parent.data.id}-to-${d.data.id}`)
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

function searchRecursive(currentNode, valueToBeSearched, parentId) {
    if (!currentNode) return null;

    // Highlight the current node
    const circleElement = document.querySelector(`#node-${currentNode.id} circle`); // Get the circle element by ID
    if (circleElement) {
        circleElement.style.transition = 'fill 1s'; // Add a transition effect
        circleElement.style.fill = 'yellow'; // Highlight the current node as being searched
    }

    if (parentId !== null) {
        const linkElement = document.querySelector(`#link-${parentId}-to-${currentNode.id}`); // Get the link element by ID
        if (linkElement) {
            linkElement.style.stroke = 'yellow'; 
            circleElement.style.transition = 'fill 1s';
            linkElement.style.strokeWidth = '2'; 
        }
    }
    
    const nodeValue = parseInt(currentNode.name, 10); // Convert the node value to an integer
    if (nodeValue === valueToBeSearched) { 
        if (circleElement) { 
            circleElement.style.fill = 'red'; // Highlight the found node
        }
        return currentNode; 
    }

    // Traverse to the left or right child based on the comparison
    console.log("value to be searched is: " + valueToBeSearched + "and nodeValue is" + nodeValue + " and currentNode.left is " + currentNode.left + " and currentNode.right is" + currentNode.right);
    if (valueToBeSearched < nodeValue && currentNode.left) {
        return searchRecursive(currentNode.left, valueToBeSearched, currentNode.id);
    } else if (valueToBeSearched > nodeValue && currentNode.right) {
        return searchRecursive(currentNode.right, valueToBeSearched, currentNode.id);
    }
}