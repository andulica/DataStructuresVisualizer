//why I can't import "d3" from "d3"?"
// setting breakpoints in the d3 .js file is not working
//import * as d3 from "d3";

let root, svg, treemap, width, height;
let margin = { top: 20, right: 90, bottom: 30, left: 90 };
window.drawBST = function (data) {

    // Set the dimensions and margins of the diagram
    margin,
        width = 700 - margin.left - margin.right,
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
    updateTree(root);      // This will compute the positions and draw the nodes#
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

    let i = 0;
    let node = svg.selectAll('g.node')
        .data(nodes, d => d.id || (d.id = ++i)); // Assigns a unique id to each node

    // Enter any new modes at the parent's previous position
    let nodeEnter = node.enter().append('g')
        .attr('transform', `translate(${source.x0},${source.y0})`) // source is the parent node

    // Add Circle for the nodes
    nodeEnter.append('circle')
        .attr('class', 'node')
        .attr('r', 20) // Adjust radius as needed
        .style('fill', d => d.data.visited ? 'red' : '#00F1D4') // Conditional fill based on visited
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

// preffered way is to have this methods in the backend and call them from the frontend. Look into it on how you may achive this.
function findParent(node, newValue) {

    if (newValue < node.data.name) {
        // Go left
        if (!node.children || node.children.length === 0 || !node.children[0]) {
            return node; // Found parent
        } else {
            return findParent(node.children[0], newValue);
        }
    } else {
        // Go right
        if (!node.children || node.children.length < 2 || !node.children[1]) {
            return node; // Found parent
        } else {
            return findParent(node.children[1], newValue);
        }
    }
}


//Need to highlight the nodes and edges that are being traversed
// Need to add a button to start the traversal preorder, inorder, postorder