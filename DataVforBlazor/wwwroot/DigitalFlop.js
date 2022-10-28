export function Test(id,config,shape) {
    const { CRender, GRAPHS } = window.CRender

    const canvas = document.getElementById(id)
    const render = new CRender(canvas)
    const [w, h] = render.area
    const position = [w / 2, h / 2]
    if (config.textAlign === 'left') position[0] = 0
    if (config.textAlign === 'right') position[0] = w

    const graph = new GRAPHS.Text({
        animationCurve: config.animationCurve,
        animationFrame: config.animationFrame,
        //shape: {
        //    content: config.content,
        //    position: [w / 2, h / 2],
        //},
        shape: config,
        style: {
            fontSize: config.fontSize,
            fill: config.fill,
            opacity: 0,
        },
    })
    graph.shape.position = position
    console.log(graph)
    render.add(graph)

    graph.animation('style', {
        opacity: 1,
        fontSize: config.fontSize,
    })
}

